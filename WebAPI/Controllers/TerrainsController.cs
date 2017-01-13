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