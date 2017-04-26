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
    public class TerrainsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Terrains
        public IQueryable<Terrain> GetTerrains()
        {
            return db.Terrains;
        }
        //http://localhost:49421/api/terrains/10.014/12.02/
        [Route("api/Terrains/{latitude}/{longitude}")]
        [HttpGet]
        public List<Terrain> GetNearestTerrains(double longitude, double latitude )
        {

            List<Terrain> Terrains = new List<Terrain>();

            foreach (Terrain t in db.Terrains.ToList())
            {
                double rlat1 = Math.PI * t.Latitude / 180;
                double rlat2 = Math.PI * latitude / 180;
                double theta = t.Longitude - longitude;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;
               if (dist < 10)
                {
                    Terrain terrain = new Terrain();
                    terrain.Id = t.Id;
                    terrain.Nom = t.Nom;
                    terrain.Description = t.Description;
                    terrain.Latitude = t.Latitude;
                    terrain.Longitude = t.Longitude;
                    terrain.PathImage = t.PathImage;
                    terrain.EmailProp = t.EmailProp;

                   Proprietaire p= db.Proprietaires.Find(t.EmailProp);
                    if (p != null)
                    {
                        terrain.Proprietaire = new Proprietaire();
                        terrain.Proprietaire.NumTel = p.NumTel;
                        terrain.Proprietaire.Email = p.Email;
                    }
                    Terrains.Add(terrain);



                }
            }
            return Terrains;
        }


        // GET: api/Terrains/5
        [ResponseType(typeof(Terrain))]
        public IHttpActionResult GetTerrain(int id)
        {
            Terrain terrain = db.Terrains.Find(id);
            if (terrain == null)
            {
                return NotFound();
            }

            return Ok(terrain);
        }

        // PUT: api/Terrains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTerrain(int id, Terrain terrain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != terrain.Id)
            {
                return BadRequest();
            }

            db.Entry(terrain).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerrainExists(id))
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

        // POST: api/Terrains
        [ResponseType(typeof(Terrain))]
        public IHttpActionResult PostTerrain(Terrain terrain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Terrains.Add(terrain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = terrain.Id }, terrain);
        }

        // DELETE: api/Terrains/5
        [ResponseType(typeof(Terrain))]
        public IHttpActionResult DeleteTerrain(int id)
        {
            Terrain terrain = db.Terrains.Find(id);
            if (terrain == null)
            {
                return NotFound();
            }

            db.Terrains.Remove(terrain);
            db.SaveChanges();

            return Ok(terrain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerrainExists(int id)
        {
            return db.Terrains.Count(e => e.Id == id) > 0;
        }
    }
}