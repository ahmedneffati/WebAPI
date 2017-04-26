using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HorairesController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Horaires
        public IQueryable<Horaire> GetHoraires()
        {
            return db.Horaires;
        }

        public IQueryable<Horaire> GetHorairesD(int x)
        {
            var list = db.Reservations.Where(b => b.IdTerrain == x);
           //nt[] l=new int();
            int i= 0;
            foreach (var ll in list)
            {

             //   l.add
            }
            return db.Horaires.Where(a=>a.Id != db.Reservations.Where(b=>b.IdTerrain==x).First().HoraireId);
        }


        // GET: api/Horaires/5
        [ResponseType(typeof(Horaire))]
        public IHttpActionResult GetHoraire(int id)
        {
            Horaire horaire = db.Horaires.Find(id);
            if (horaire == null)
            {
                return NotFound();
            }

            return Ok(horaire);
        }

        // PUT: api/Horaires/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHoraire(int id, Horaire horaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horaire.Id)
            {
                return BadRequest();
            }

            db.Entry(horaire).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoraireExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Horaires
        [ResponseType(typeof(Horaire))]
        public IHttpActionResult PostHoraire(Horaire horaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Horaires.Add(horaire);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = horaire.Id }, horaire);
        }

        // DELETE: api/Horaires/5
        [ResponseType(typeof(Horaire))]
        public IHttpActionResult DeleteHoraire(int id)
        {
            Horaire horaire = db.Horaires.Find(id);
            if (horaire == null)
            {
                return NotFound();
            }

            db.Horaires.Remove(horaire);
            db.SaveChanges();

            return Ok(horaire);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoraireExists(int id)
        {
            return db.Horaires.Count(e => e.Id == id) > 0;
        }
    }
}