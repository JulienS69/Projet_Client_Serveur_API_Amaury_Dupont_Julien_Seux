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
    public class LivresController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Livres
        public IQueryable<Livre> GetLivre()
        {
            return db.Livre;
        }

        // GET: api/Livres/5
        [ResponseType(typeof(Livre))]
        public async Task<IHttpActionResult> GetLivre(int id)
        {
            Livre livre = await db.Livre.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            return Ok(livre);
        }

        // PUT: api/Livres/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLivre(int id, Livre livre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livre.IdLivre)
            {
                return BadRequest();
            }

            db.Entry(livre).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(id))
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

        // POST: api/Livres
        [ResponseType(typeof(Livre))]
        public async Task<IHttpActionResult> PostLivre(Livre livre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livre.Add(livre);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = livre.IdLivre }, livre);
        }

        // DELETE: api/Livres/5
        [ResponseType(typeof(Livre))]
        public async Task<IHttpActionResult> DeleteLivre(int id)
        {
            Livre livre = await db.Livre.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            db.Livre.Remove(livre);
            await db.SaveChangesAsync();

            return Ok(livre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivreExists(int id)
        {
            return db.Livre.Count(e => e.IdLivre == id) > 0;
        }
    }
}