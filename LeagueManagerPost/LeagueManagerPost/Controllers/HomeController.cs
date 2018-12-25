using LeagueManagerPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LeagueManagerPost.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Feed()
        {
            var upcomingGames = db.Games
                 .Include(g => g.HomeTeam)
                 .Include(g => g.AwayTeam)
                 .Where(g => g.Date >= DateTime.Now);
            return View(upcomingGames);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        //public ActionResult RefereeError()
        //{
        //    return View();
        //}

        public ActionResult SameDayError()
        {
            return View();
        }

        //[Authorize]
        public ActionResult Events()
        {
            return View();
        }

        //[Authorize]
        public JsonResult GetEvents()
        {
            using (db)
            {
                var events = db.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public JsonResult SaveEvent(Events e)
        {
            var status = false;

            using (db)
            {
                if (e.EventId > 0)
                {
                    //Update the event
                    var v = db.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    db.Events.Add(e);
                }

                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public JsonResult DeleteEvent(int eventId)
        {
            var status = false;

            using (db)
            {
                var v = db.Events.Where(a => a.EventId == eventId).FirstOrDefault();
                if (v != null)
                {
                    db.Events.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}