using LeagueManagerPost.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LeagueManagerPost.Controllers
{
    
        public class EventsController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: Events
            public ActionResult Index()
            {
                return View(db.Events.ToList());
            }

            // GET: Events/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Events @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }

            // GET: Events/Create
            //[Authorize(Roles = "Admin")]
            public ActionResult Create()
            {
                return View();
            }

            // POST: Events/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            //[Authorize(Roles = "Admin")]
            public ActionResult Create([Bind(Include = "EventId,Subject,Description,Start,End,ThemeColor,IsFullDay")] Events @event)
            {
                if (ModelState.IsValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(@event);
            }

            // GET: Events/Edit/5
            //[Authorize(Roles = "Admin")]
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Events @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }

            // POST: Events/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            //[Authorize(Roles = "Admin")]
            public ActionResult Edit([Bind(Include = "EventId,Subject,Description,Start,End,ThemeColor,IsFullDay")] Events @event)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(@event);
            }

            // GET: Events/Delete/5
            //[Authorize(Roles = "Admin")]
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Events @event = db.Events.Find(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }

            // POST: Events/Delete/5
            //[Authorize(Roles = "Admin")]
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Events @event = db.Events.Find(id);
                db.Events.Remove(@event);
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