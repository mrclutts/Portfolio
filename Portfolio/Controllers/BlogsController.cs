using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    [RequireHttps]
    public class BlogsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs
        public ActionResult BlogIndex(string tag, int? page, string query)
        {

            IQueryable<Blog> posts;
            if (!String.IsNullOrWhiteSpace(tag))
            {
                posts = db.Blog.Where(p => p.Tag == tag);
            }
            else {
                posts = db.Blog;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.Query = query;
            //var qposts = posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                posts = posts.Where(p => p.Title.Contains(query) || p.Body.Contains(query) || p.Comments.Any(c => c.Body.Contains(query) || c.Author.DisplayName.Contains(query)));
            }
            return View(posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View(db.Blog.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(string slug)
        {

            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.FirstOrDefault(b => b.Slug == slug);
            if (blog == null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }

        [Authorize(Roles = "Admin")]
        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        //Upload an Image
        public static class ImageUploadValidator
        {
            public static bool IsWebFriendlyImage(HttpPostedFileBase file)
            {
                //check for actual object
                if (file == null)
                    return false;
                //check size - file must be less than 2MB and greater than 1kb
                if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                    return false;
                try
                {
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                            ImageFormat.Png.Equals(img.RawFormat) ||
                            ImageFormat.Gif.Equals(img.RawFormat);
                    }
                }
                catch
                {
                    return false;
                }
            }
           
        }


        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Slug,Body,MediaURL,Published,Tag,Caption")] Blog blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/img/blog/"), fileName));
                    blog.MediaURL = "~/img/blog/" + fileName;
                }

                var Slug = StringUtilities.URLFriendly(blog.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title.");
                    return View(blog);
                }
                if (db.Blog.Any(b => b.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blog);
                }
                blog.Slug = Slug;
                blog.Created = System.DateTimeOffset.Now;
                db.Blog.Add(blog);
                db.SaveChanges();
                var recipients = db.Subscribe.Select(s=>s.SubEmail).ToList();
                EmailService es = new EmailService();
                IdentityMessage im = new IdentityMessage();
                im.Subject = $"Blog Post from Matt Clutts - {blog.Title}";
                im.Body = blog.Body;
                await es.SendAsync(im,recipients);
                return RedirectToAction("Admin");
            }

            return View(blog);
        }
        //Post: Blogs/CreateComment
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "PostId,Body")] Comment comment)
        {
            var slug = db.Blog.Find(comment.PostId).Slug;
            if (ModelState.IsValid)
            {
                comment.AuthorId = User.Identity.GetUserId();
                comment.Created = System.DateTimeOffset.Now;
                db.Comment.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { Slug = slug });
        }



        [Authorize(Roles = "Admin")]
        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        //Get: Blogs/EditComment
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published,Tag,Caption")] Blog blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                db.Blog.Attach(blog);

                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/img/blog/"), fileName));
                    blog.MediaURL = "~/img/blog/" + fileName;
                    db.Entry(blog).Property("MediaURL").IsModified = true;

                }
                //db.Entry(blog).State = EntityState.Modified;
                blog.Updated = System.DateTimeOffset.Now;
                db.Entry(blog).Property("Title").IsModified = true;
                db.Entry(blog).Property("Body").IsModified = true;
                db.Entry(blog).Property("Slug").IsModified = true;
                db.Entry(blog).Property("Caption").IsModified = true;
                db.Entry(blog).Property("Updated").IsModified = true;
                db.Entry(blog).Property("Tag").IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(blog);
        }
        //Post EditComment
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment([Bind(Include = "PostId,Body,Id")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var slug = db.Blog.Find(comment.PostId).Slug;
                db.Comment.Attach(comment);
                db.Entry(comment).Property("Body").IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Details", new { Slug = slug });
            }
            return View(comment);
        }



        [Authorize(Roles = "Admin")]
        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        //Get Blogs/DeleteComment 
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        //Post Blogs/DeleteComment
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int? id)
        {
            
            Comment comment = db.Comment.Find(id);
            var slug = db.Blog.Find(comment.PostId).Slug;
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { Slug = slug });
        }

        [Authorize(Roles = "Admin")]
        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
