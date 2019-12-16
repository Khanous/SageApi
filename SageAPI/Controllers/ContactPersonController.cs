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
using Person;

namespace SageAPI.Controllers
{
    public class ContactPersonController : ApiController
    {
        private SageAPIEntities db = new SageAPIEntities();
        int Mandant = 1;
        // GET: api/ContactPerson
        public IQueryable<KHKAnsprechpartner> GetKHKAnsprechpartner()
        {
            return db.KHKAnsprechpartner;
        }

        // GET: api/ContactPerson/5
        [ResponseType(typeof(KHKAnsprechpartner))]
        public IHttpActionResult GetKHKAnsprechpartner(int id)
        {
            KHKAnsprechpartner kHKAnsprechpartner = db.KHKAnsprechpartner.Find(id, Mandant);
            if (kHKAnsprechpartner == null)
            {
                return NotFound();
            }

            return Ok(kHKAnsprechpartner);
        }

        // PUT: api/ContactPerson/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHKAnsprechpartner(int id, KHKAnsprechpartner kHKAnsprechpartner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHKAnsprechpartner.Nummer)
            {
                return BadRequest();
            }

            db.Entry(kHKAnsprechpartner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHKAnsprechpartnerExists(id))
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

        // POST: api/ContactPerson
        [ResponseType(typeof(KHKAnsprechpartner))]
        public IHttpActionResult PostKHKAnsprechpartner(KHKAnsprechpartner kHKAnsprechpartner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KHKAnsprechpartner.Add(kHKAnsprechpartner);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHKAnsprechpartnerExists(kHKAnsprechpartner.Nummer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kHKAnsprechpartner.Nummer }, kHKAnsprechpartner);
        }

        // DELETE: api/ContactPerson/5
        [ResponseType(typeof(KHKAnsprechpartner))]
        public IHttpActionResult DeleteKHKAnsprechpartner(int id)
        {
            KHKAnsprechpartner kHKAnsprechpartner = db.KHKAnsprechpartner.Find(id);
            if (kHKAnsprechpartner == null)
            {
                return NotFound();
            }

            db.KHKAnsprechpartner.Remove(kHKAnsprechpartner);
            db.SaveChanges();

            return Ok(kHKAnsprechpartner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHKAnsprechpartnerExists(int id)
        {
            return db.KHKAnsprechpartner.Count(e => e.Nummer == id) > 0;
        }
    }
}