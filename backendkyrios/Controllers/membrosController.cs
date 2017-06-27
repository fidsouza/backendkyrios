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
using backendkyrios.Models;

namespace backendkyrios.Controllers
{
    public class membrosController : ApiController
    {
        private ContextKyriosbd db = new ContextKyriosbd();

        // GET: api/membros/5
        [ResponseType(typeof(membros))]
        public async Task<IHttpActionResult> Getmembros(string nome)
        {
            List<membros> membro = await  db.membros.Where(i => i.nomeMembro.Contains(nome)).ToListAsync();
            if (membro == null)
            {
                return NotFound();
            }

            return Ok(membro);
        }


        [ResponseType(typeof(membros))]
        public async Task<IHttpActionResult> GetmembrosID(int id)
        {
            membros membro = await db.membros.FirstOrDefaultAsync();
            if (membro == null)
            {
                return NotFound();
            }

            return Ok(membro);
        }

        // PUT: api/membros/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmembros(int id, membros membros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membros.idMembro)
            {
                return BadRequest();
            }

            db.Entry(membros).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!membrosExists(id))
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

        // POST: api/membros
        [ResponseType(typeof(membros))]
        public async Task<IHttpActionResult> Postmembros(membros membros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            int Id = db.membros.Select(i => i.idMembro).DefaultIfEmpty(0).Max();
            int maxid = Id + 1;

            membros.idMembro = maxid;

            db.membros.Add(membros);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (membrosExists(membros.idMembro))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = membros.idMembro }, membros);
        }

        // DELETE: api/membros/5
        [ResponseType(typeof(membros))]
        public async Task<IHttpActionResult> Deletemembros(int id)
        {
            membros membros = await db.membros.FindAsync(id);
            if (membros == null)
            {
                return NotFound();
            }

            db.membros.Remove(membros);
            await db.SaveChangesAsync();

            return Ok(membros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool membrosExists(int id)
        {
            return db.membros.Count(e => e.idMembro == id) > 0;
        }
    }
}