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
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.AwayTeam).Include(g => g.HomeTeam);
            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.AwayTeam)
                                .Include(g => g.HomeTeam)
                                .SingleOrDefault(x => x.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.AwayTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time,Location,HomeTeamId,AwayTeamId")] Game game)
        {
            //if (ModelState.IsValid)
            //{

            game.HomeTeam = db.Teams.Single(t => t.Id == game.HomeTeamId);
            game.AwayTeam = db.Teams.Single(t => t.Id == game.AwayTeamId);
            

            #region ErrorChecking
            // Home team can't be the same as away team
            if (game.HomeTeam == game.AwayTeam)
            {
                return RedirectToAction("Error", "Home");
            }

            // Very overly complicated if statement
            // Each team can only have one game per day
            // I'm not completely sure if this works right
            foreach (var g in db.Games)
            {
                if ((g.HomeTeam == game.HomeTeam) && (g.Date == game.Date) ||
                    (g.HomeTeam == game.AwayTeam) && (g.Date ==game.Date) ||
                    (g.AwayTeam == game.HomeTeam) && (g.Date == game.Date) ||
                    (g.AwayTeam == game.AwayTeam) && (g.Date == game.Date))
                {
                    return RedirectToAction("SameDayError", "Home");
                }
            }

            //// All of the referees must be different people
            //if (game.Referee == game.SecondReferee ||
            //    game.Referee == game.ThirdReferee ||
            //    game.ThirdReferee == game.SecondReferee)
            //{
            //    return RedirectToAction("RefereeError", "Home");
            //}
            #endregion

            #region EventCreation
            // Adds event to calendar when a game is created
            string gameTime = game.Time.ToShortTimeString();
            string awayTeamString = game.AwayTeam.Name + " (" + game.AwayTeam.Wins + "-" + game.AwayTeam.Losses + ")";
            string homeTeamString = game.HomeTeam.Name + " (" + game.HomeTeam.Wins + "-" + game.HomeTeam.Losses + ")";
            string gameDescription = "The " + awayTeamString + " are playing the " + homeTeamString
                                        + " at the " + game.Location + ". Start time is " + gameTime + ". ";


            

            var eve = new Events
            {
                Subject = game.AwayTeam.Name + " @ " + game.HomeTeam.Name,
                Start = game.Date,
                IsFullDay = true,
                Description = gameDescription,
                ThemeColor = "Blue",
                Event = game.Id


            };
            db.Events.Add(eve);
            #endregion

            db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}

            //ViewBag.AwayTeamId = new SelectList(db.Teams, "Id", "Name", game.AwayTeamId);
            //ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name", game.HomeTeamId);
            //return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.AwayTeamId = new SelectList(db.Teams, "Id", "Name", game.AwayTeamId);
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name", game.HomeTeamId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time,Location,HomeTeamId,AwayTeamId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AwayTeamId = new SelectList(db.Teams, "Id", "Name", game.AwayTeamId);
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name", game.HomeTeamId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.AwayTeam)
                                .Include(g => g.HomeTeam)
                                .SingleOrDefault(x => x.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
