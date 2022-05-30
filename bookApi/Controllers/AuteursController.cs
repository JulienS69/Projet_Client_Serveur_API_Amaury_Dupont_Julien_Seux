using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ORM;

namespace bookApi.Controllers
{
    public class AuteursController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Auteurs
        public IQueryable<Auteur> GetAuteur()
        {
            return db.Auteur;
        }

        // GET: api/Auteurs/5
        [ResponseType(typeof(Auteur))]
        public async Task<IHttpActionResult> GetAuteur(int id)
        {
            Auteur auteur = await db.Auteur.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            return Ok(auteur);
        }

        // PUT: api/Auteurs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auteur.IdAuteur)
            {
                return BadRequest();
            }

            db.Entry(auteur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
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

        // POST: api/Auteurs
        [ResponseType(typeof(Auteur))]
        public async Task<IHttpActionResult> PostAuteur(Auteur auteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Auteur.Add(auteur);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = auteur.IdAuteur }, auteur);
        }

        // DELETE: api/Auteurs/5
        [ResponseType(typeof(Auteur))]
        public async Task<IHttpActionResult> DeleteAuteur(int id)
        {
            Auteur auteur = await db.Auteur.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            db.Auteur.Remove(auteur);
            await db.SaveChangesAsync();

            return Ok(auteur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuteurExists(int id)
        {
            return db.Auteur.Count(e => e.IdAuteur == id) > 0;
        }
    }
}