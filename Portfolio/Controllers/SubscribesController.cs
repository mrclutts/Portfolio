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
    public class SubscribesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subscribes
        public ActionResult Index()
        {
            return View(db.Subscribe.ToList());
        }

        // GET: Subscribes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribe subscribe = db.Subscribe.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // GET: Subscribes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscribes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubEmail")] Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                db.Subscribe.Add(subscribe);
                db.SaveChanges();
                return RedirectToAction("BlogIndex", "Blogs");
            }

            return View(subscribe);
        }

        // GET: Subscribes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribe subscribe = db.Subscribe.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // POST: Subscribes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubEmail")] Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscribe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscribe);
        }

        // GET: Subscribes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribe subscribe = db.Subscribe.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // POST: Subscribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscribe subscribe = db.Subscribe.Find(id);
            db.Subscribe.Remove(subscribe);
            db.SaveChanges();
            return RedirectToAction("Index");
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
