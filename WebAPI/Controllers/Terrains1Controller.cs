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
    public class Terrains1Controller : Controller
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: Terrains1
        public ActionResult Index()
        {
            var terrains = db.Terrains.Include(t => t.Proprietaire);
            return View(terrains.ToList());
        }

        // GET: Terrains1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrains.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            return View(terrain);
        }

        // GET: Terrains1/Create
        public ActionResult Create()
        {
            ViewBag.EmailProp = new SelectList(db.Proprietaires, "Email", "MotDePass");
            return View();
        }

        // POST: Terrains1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Longitude,Latitude,Nom,Description,PathImage,EmailProp")] Terrain terrain)
        {
            if (ModelState.IsValid)
            {
                db.Terrains.Add(terrain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmailProp = new SelectList(db.Proprietaires, "Email", "MotDePass", terrain.EmailProp);
            return View(terrain);
        }

        // GET: Terrains1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrains.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailProp = new SelectList(db.Proprietaires, "Email", "MotDePass", terrain.EmailProp);
            return View(terrain);
        }

        // POST: Terrains1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Longitude,Latitude,Nom,Description,PathImage,EmailProp")] Terrain terrain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terrain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmailProp = new SelectList(db.Proprietaires, "Email", "MotDePass", terrain.EmailProp);
            return View(terrain);
        }

        // GET: Terrains1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrains.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            return View(terrain);
        }

        // POST: Terrains1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Terrain terrain = db.Terrains.Find(id);
            db.Terrains.Remove(terrain);
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
