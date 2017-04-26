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
    public class Horaires1Controller : Controller
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: Horaires1
        public ActionResult Index()
        {
            return View(db.Horaires.ToList());
        }
      

        // GET: Horaires1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = db.Horaires.Find(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // GET: Horaires1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horaires1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Intervalle")] Horaire horaire)
        {
            if (ModelState.IsValid)
            {
                db.Horaires.Add(horaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(horaire);
        }

        // GET: Horaires1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = db.Horaires.Find(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // POST: Horaires1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Intervalle")] Horaire horaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(horaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(horaire);
        }

        // GET: Horaires1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = db.Horaires.Find(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // POST: Horaires1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Horaire horaire = db.Horaires.Find(id);
            db.Horaires.Remove(horaire);
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
