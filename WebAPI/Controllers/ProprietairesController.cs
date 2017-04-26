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
    public class ProprietairesController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();
        [Route("api/Proprietaires/Connexion/{Email}/{pass}")]
        [ResponseType(typeof(Joueur))]

        public IHttpActionResult GetConnec(string Email, string pass)
        {
            try
            {
                Proprietaire joueur = db.Proprietaires.Where(s => s.Email.Equals(Email) &&
    s.MotDePass.Equals(pass)).ToList().First();
                return Ok(joueur);
            }
            catch (Exception ex)
            {
                return NotFound();
            }


        }
        // GET: api/Proprietaires
        public IQueryable<Proprietaire> GetProprietaires()
        {
            return db.Proprietaires;
        }

        // GET: api/Proprietaires/5
        [ResponseType(typeof(Proprietaire))]
        public IHttpActionResult GetProprietaire(string id)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            return Ok(proprietaire);
        }

        // PUT: api/Proprietaires/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProprietaire(string id, Proprietaire proprietaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proprietaire.Email)
            {
                return BadRequest();
            }

            db.Entry(proprietaire).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProprietaireExists(id))
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

        // POST: api/Proprietaires
        [ResponseType(typeof(Proprietaire))]
        public IHttpActionResult PostProprietaire(Proprietaire proprietaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Proprietaires.Add(proprietaire);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProprietaireExists(proprietaire.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = proprietaire.Email }, proprietaire);
        }

        // DELETE: api/Proprietaires/5
        [ResponseType(typeof(Proprietaire))]
        public IHttpActionResult DeleteProprietaire(string id)
        {
            Proprietaire proprietaire = db.Proprietaires.Find(id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            db.Proprietaires.Remove(proprietaire);
            db.SaveChanges();

            return Ok(proprietaire);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProprietaireExists(string id)
        {
            return db.Proprietaires.Count(e => e.Email == id) > 0;
        }
    }
}