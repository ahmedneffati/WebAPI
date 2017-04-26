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
    public class MatchJoueurs1Controller : Controller
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: MatchJoueurs1
        public ActionResult Index()
        {
            var matchJoueurs = db.MatchJoueurs.Include(m => m.Joueur).Include(m => m.Match);
            return View(matchJoueurs.ToList());
        }

        // GET: MatchJoueurs1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            if (matchJoueur == null)
            {
                return HttpNotFound();
            }
            return View(matchJoueur);
        }

        // GET: MatchJoueurs1/Create
        public ActionResult Create()
        {
            ViewBag.JoueurEmail = new SelectList(db.Joueurs, "Email", "MotDePass");
            ViewBag.MatchId = new SelectList(db.Matchs, "Id", "Description");
            return View();
        }

        // POST: MatchJoueurs1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JoueurEmail,MatchId,EtatDeConfirmation")] MatchJoueur matchJoueur)
        {
            if (ModelState.IsValid)
            {
                db.MatchJoueurs.Add(matchJoueur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JoueurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", matchJoueur.JoueurEmail);
            ViewBag.MatchId = new SelectList(db.Matchs, "Id", "Description", matchJoueur.MatchId);
            return View(matchJoueur);
        }

        // GET: MatchJoueurs1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            if (matchJoueur == null)
            {
                return HttpNotFound();
            }
            ViewBag.JoueurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", matchJoueur.JoueurEmail);
            ViewBag.MatchId = new SelectList(db.Matchs, "Id", "Description", matchJoueur.MatchId);
            return View(matchJoueur);
        }

        // POST: MatchJoueurs1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JoueurEmail,MatchId,EtatDeConfirmation")] MatchJoueur matchJoueur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchJoueur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JoueurEmail = new SelectList(db.Joueurs, "Email", "MotDePass", matchJoueur.JoueurEmail);
            ViewBag.MatchId = new SelectList(db.Matchs, "Id", "Description", matchJoueur.MatchId);
            return View(matchJoueur);
        }

        // GET: MatchJoueurs1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            if (matchJoueur == null)
            {
                return HttpNotFound();
            }
            return View(matchJoueur);
        }

        // POST: MatchJoueurs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            db.MatchJoueurs.Remove(matchJoueur);
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
