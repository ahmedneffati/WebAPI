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
    public class JoueursController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Joueurs
        public IQueryable<Joueur> GetJoueurs()
        {
            return db.Joueurs;
        }

        // GET: api/Joueurs/5
        [ResponseType(typeof(Joueur))]
        public IHttpActionResult GetJoueur(string id)
        {
            Joueur joueur = db.Joueurs.Find(id);
            if (joueur == null)
            {
                return NotFound();
            }

            return Ok(joueur);
        }

        // PUT: api/Joueurs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJoueur(string id, Joueur joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != joueur.Email)
            {
                return BadRequest();
            }

            db.Entry(joueur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoueurExists(id))
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

        // POST: api/Joueurs
        [ResponseType(typeof(Joueur))]
        public IHttpActionResult PostJoueur(Joueur joueur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Joueurs.Add(joueur);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JoueurExists(joueur.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = joueur.Email }, joueur);
        }

        // DELETE: api/Joueurs/5
        [ResponseType(typeof(Joueur))]
        public IHttpActionResult DeleteJoueur(string id)
        {
            Joueur joueur = db.Joueurs.Find(id);
            if (joueur == null)
            {
                return NotFound();
            }

            db.Joueurs.Remove(joueur);
            db.SaveChanges();

            return Ok(joueur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JoueurExists(string id)
        {
            return db.Joueurs.Count(e => e.Email == id) > 0;
        }
    }
}