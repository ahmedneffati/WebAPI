﻿using System;
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
    public class AdminsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Admins
        public IQueryable<Admin> GetAdmins()
        {
            return db.Admins;
        }


        // GET: api/Admins/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetAdmin(string id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        //http://localhost:49421/api/Admins/Connexion/zadazdf@zefze50.com/qfqzfqzfqzfd
        [Route("api/Admins/Connexion/{Email}/{pass}")]
        [ResponseType(typeof(Joueur))]

        public IHttpActionResult GetConnec(string Email, string pass)
        {
            try
            {
                Admin admin  = db.Admins.Where(s => s.Email.Equals(Email) &&
    s.MotDePass.Equals(pass)).ToList().First();
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        // PUT: api/Admins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmin(string id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.Email)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        [ResponseType(typeof(Admin))]
        public IHttpActionResult PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admins.Add(admin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdminExists(admin.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = admin.Email }, admin);
        }

        // DELETE: api/Admins/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult DeleteAdmin(string id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.Admins.Remove(admin);
            db.SaveChanges();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(string id)
        {
            return db.Admins.Count(e => e.Email == id) > 0;
        }
    }
}