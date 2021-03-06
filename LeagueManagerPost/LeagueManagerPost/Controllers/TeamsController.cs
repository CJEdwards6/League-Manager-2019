﻿using System;
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
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly Repository _repo = new Repository();

        // GET: Teams
        //public ActionResult Index()
        //{
        //    return View(db.Teams.ToList());
        //}

        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_" : "";
            ViewBag.CoachSortParm = String.IsNullOrEmpty(sortOrder) ? "coach_" : "";
            ViewBag.WinsSortParm = String.IsNullOrEmpty(sortOrder) ? "wins_" : "";
            ViewBag.LossesSortParm = String.IsNullOrEmpty(sortOrder) ? "losses_" : "";

            var teams = from s in db.Teams
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                teams = teams.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_":
                    teams = teams.OrderBy(s => s.Name);
                    break;
                case "coach_":
                    teams = teams.OrderBy(s => s.Coach);
                    break;
                case "wins_":
                    teams = teams.OrderByDescending(s => s.Wins);
                    break;
                case "losses_":
                    teams = teams.OrderByDescending(s => s.Losses);
                    break;
                default:
                    teams = teams.OrderByDescending(s => s.Wins);
                    break;
            }

            return View(teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Team team = db.Teams.Find(id);
            var team = _repo.GetTeamWithPlayers(id.Value);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Coach,Wins,Losses")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Team team = db.Teams.Find(id);
            var team = _repo.GetTeamWithPlayers(id.Value);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Coach,Wins,Losses")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
