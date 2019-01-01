using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeagueManagerPost.Models;

namespace LeagueManagerPost.Controllers
{
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        private readonly Repository _repo = new Repository();

        // GET: Players
        public ActionResult Index()
        {
            return View(db.Players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // Added new Create method that takes in team info
        public ActionResult Create(int teamId, string teamName)
        {
            ViewBag.TeamId = teamId;
            ViewBag.TeamName = teamName;
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Position,Number,Starter,TeamId")] Player player)
        {
            if (!int.TryParse(Request.Form["TeamId"], out int teamId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _repo.SaveNewPlayer(player, teamId);

            return RedirectToAction("Edit", "Teams", new { id = teamId });
            //return RedirectToAction("Index", "Teams");
        }
        ////////////////////////////////////////////////////////////


        // GET: Players/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Player player = db.Players.Find(id);
        //    if (player == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(player);
        //}

        public ActionResult Edit(int? id, int teamId, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TeamId = teamId;
            ViewBag.TeamName = name;
            var players = _repo.GetPlayersById(id.Value);
            if (players == null)
            {
                return HttpNotFound();
            }
            return View(players);
        }

        // POST: Players/Edit/5
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Position,Number,Starter")] Player player)
        {
            if (!int.TryParse(Request.Form["TeamId"], out int teamId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _repo.SaveUpdatedPlayer(player, teamId);
            return RedirectToAction("Edit", "Teams", new { id = teamId });
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index", "Teams");
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
