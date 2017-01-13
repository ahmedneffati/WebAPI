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
    public class MatchJoueursController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/MatchJoueurs
        public IQueryable<MatchJoueur> GetMatchJoueurs()
        {
            return db.MatchJoueurs;
        }

        // GET: api/MatchJoueurs/5
        [ResponseType(typeof(MatchJoueur))]
        public IHttpActionResult GetMatchJoueur(int id)
        {
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            if (matchJoueur == null)
            {
                return NotFound();
            }

            return Ok(matchJoueur);
        }

        // PUT: api/MatchJoueurs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatchJoueur(int id, MatchJoueur matchJoueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matchJoueur.Id)
            {
                return BadRequest();
            }

            db.Entry(matchJoueur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchJoueurExists(id))
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

        // POST: api/MatchJoueurs
        [ResponseType(typeof(MatchJoueur))]
        public IHttpActionResult PostMatchJoueur(MatchJoueur matchJoueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MatchJoueurs.Add(matchJoueur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = matchJoueur.Id }, matchJoueur);
        }

        // DELETE: api/MatchJoueurs/5
        [ResponseType(typeof(MatchJoueur))]
        public IHttpActionResult DeleteMatchJoueur(int id)
        {
            MatchJoueur matchJoueur = db.MatchJoueurs.Find(id);
            if (matchJoueur == null)
            {
                return NotFound();
            }

            db.MatchJoueurs.Remove(matchJoueur);
            db.SaveChanges();

            return Ok(matchJoueur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchJoueurExists(int id)
        {
            return db.MatchJoueurs.Count(e => e.Id == id) > 0;
        }
    }
}