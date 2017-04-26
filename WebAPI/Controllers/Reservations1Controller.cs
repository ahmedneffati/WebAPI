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
    public class Reservations1Controller : Controller
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: Reservations1
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Horaire).Include(r => r.Joueur).Include(r => r.Terrain);
            return View(reservations.ToList());
        }

        // GET: Reservations1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations1/Create
        public ActionResult Create()
        {
            ViewBag.HoraireId = new SelectList(db.Horaires, "Id", "Intervalle");
            ViewBag.EmailJoueur = new SelectList(db.Joueurs, "Email", "MotDePass");
            ViewBag.IdTerrain = new SelectList(db.Terrains, "Id", "Nom");
            return View();
        }

        // POST: Reservations1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,EmailJoueur,IdTerrain,HoraireId,EtatDeConfirmation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HoraireId = new SelectList(db.Horaires, "Id", "Intervalle", reservation.HoraireId);
            ViewBag.EmailJoueur = new SelectList(db.Joueurs, "Email", "MotDePass", reservation.EmailJoueur);
            ViewBag.IdTerrain = new SelectList(db.Terrains, "Id", "Nom", reservation.IdTerrain);
            return View(reservation);
        }

        // GET: Reservations1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoraireId = new SelectList(db.Horaires, "Id", "Intervalle", reservation.HoraireId);
            ViewBag.EmailJoueur = new SelectList(db.Joueurs, "Email", "MotDePass", reservation.EmailJoueur);
            ViewBag.IdTerrain = new SelectList(db.Terrains, "Id", "Nom", reservation.IdTerrain);
            return View(reservation);
        }

        // POST: Reservations1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,EmailJoueur,IdTerrain,HoraireId,EtatDeConfirmation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HoraireId = new SelectList(db.Horaires, "Id", "Intervalle", reservation.HoraireId);
            ViewBag.EmailJoueur = new SelectList(db.Joueurs, "Email", "MotDePass", reservation.EmailJoueur);
            ViewBag.IdTerrain = new SelectList(db.Terrains, "Id", "Nom", reservation.IdTerrain);
            return View(reservation);
        }

        // GET: Reservations1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
