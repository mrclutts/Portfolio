using Microsoft.AspNet.Identity;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            
            return View();
        }
        public ActionResult Blog()
        {
            return View(db.Blog.ToList());
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Music()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact ([Bind(Include ="Id,Name,SenderEmail,SendTime,Message,Phone")] Contact contact)
        {
            contact.SendTime = DateTime.Now;
            var svc = new EmailService();
            var msg = new IdentityMessage();
            msg.Subject = "Contact from my Portfolio" + contact.Name;
            msg.Body = contact.Message + "Email Address:" + contact.SenderEmail + "Phone:" + contact.Phone;
            await svc.SendAsync(msg);
            TempData["Message"] = "Success!  Thank you for your E-mail!";
            return RedirectToAction("Contact", "Home");
        }
    }
}