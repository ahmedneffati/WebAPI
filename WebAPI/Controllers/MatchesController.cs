using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class MatchesController : Controller
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: Matches
        public ActionResult Index()
        {
            var matchs = db.Matchs.Include(m => m.Organisateur);
            return View(matchs.ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matchs.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            ViewBag.OrganisateurEmail = new SelectList(db.Joueurs, "Email", "MotDePass");
            return View();
        }

        // POST: Matches/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Date,NbDeJoueur,Longitude,Latitude,OrganisateurEmail")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matchs.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganisateurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", match.OrganisateurEmail);
            return View(match);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matchs.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganisateurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", match.OrganisateurEmail);
            return View(match);
        }

        // POST: Matches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Date,NbDeJoueur,Longitude,Latitude,OrganisateurEmail")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganisateurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", match.OrganisateurEmail);
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matchs.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matchs.Find(id);
            db.Matchs.Remove(match);
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
