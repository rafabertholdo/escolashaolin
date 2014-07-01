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
using EscolaShaolin.Dominio.Academia.Entidade;
using EscolaShaolin.Web.Models;

namespace EscolaShaolin.Web.Controllers.Api
{
    public class AlunoController : ApiController
    {
        private EscolaShaolinContext db = new EscolaShaolinContext();

        // GET: api/Aluno
        public IQueryable<Aluno> GetAlunos()
        {
            return db.Alunos;
        }

        // GET: api/Aluno/5
        [ResponseType(typeof(Aluno))]
        public async Task<IHttpActionResult> GetAluno(Guid id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        // PUT: api/Aluno/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAluno(Guid id, Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aluno.Id)
            {
                return BadRequest();
            }

            db.Entry(aluno).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
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

        // POST: api/Aluno
        [ResponseType(typeof(Aluno))]
        public async Task<IHttpActionResult> PostAluno(Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alunos.Add(aluno);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlunoExists(aluno.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aluno.Id }, aluno);
        }

        // DELETE: api/Aluno/5
        [ResponseType(typeof(Aluno))]
        public async Task<IHttpActionResult> DeleteAluno(Guid id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();

            return Ok(aluno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlunoExists(Guid id)
        {
            return db.Alunos.Count(e => e.Id == id) > 0;
        }
    }
}