using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    [RequireHttps]
    public class BlogsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs
        public ActionResult BlogIndex(string tag)
        {
            List<Blog> posts= new List<Blog>();
            if (!String.IsNullOrWhiteSpace(tag))
            {
                posts = db.Blog.Where(p => p.Tag == tag).ToList();
            }
            else {
                posts = db.Blog.ToList();
            }
                return View(posts);
        }
        [Authorize(Roles ="Admin")]
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
            Blog blog = db.Blog.FirstOrDefault(b=>b.Slug==slug);
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

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Slug,Body,MediaURL,Published,Tag,Caption")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blog.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title.");
                    return View(blog);
                }
                if (db.Blog.Any(b =>b.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blog);
                }
                blog.Slug = Slug;
                blog.Created = System.DateTimeOffset.Now;
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(blog);
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

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published,Tag,Caption")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(blog).State = EntityState.Modified;
                blog.Updated = System.DateTimeOffset.Now;
                db.Blog.Attach(blog);
                db.Entry(blog).Property("Title").IsModified = true;
                db.Entry(blog).Property("Body").IsModified = true;
                db.Entry(blog).Property("Slug").IsModified = true;
                db.Entry(blog).Property("MediaURL").IsModified = true;
                db.Entry(blog).Property("Caption").IsModified = true;
                db.Entry(blog).Property("Updated").IsModified = true;
                db.Entry(blog).Property("Tag").IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(blog);
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
