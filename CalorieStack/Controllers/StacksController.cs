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

using CalorieStack.Models;

namespace CalorieStack.Controllers
{
    public class StacksController : ApiController
    {
        private CalorieStackContext db = new CalorieStackContext();

        // GET: api/Stacks
        public IQueryable<Stack> GetStacks()
        {
            return db.Stacks;
        }

        // GET: api/Stacks/5
        [ResponseType(typeof(Stack))]
        public async Task<IHttpActionResult> GetStack(string id)
        {
            Stack stack = await db.Stacks.FindAsync(id);
            if (stack == null)
            {
                return NotFound();
            }

            return Ok(stack);
        }

        // PUT: api/Stacks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStack(string id, Stack stack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stack.Id)
            {
                return BadRequest();
            }

            db.Entry(stack).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StackExists(id))
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

        // POST: api/Stacks
        [ResponseType(typeof(Stack))]
        public async Task<IHttpActionResult> PostStack(Stack stack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (stack.Days == null || stack.Days.Count < 1)
            {
                stack.Days = new List<Day>()
                {
                    Day.CreateDefault(stack.Id)
                };
            }

            db.Stacks.Add(stack);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StackExists(stack.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stack.Id }, stack);
        }

        // DELETE: api/Stacks/5
        [ResponseType(typeof(Stack))]
        public async Task<IHttpActionResult> DeleteStack(string id)
        {
            Stack stack = await db.Stacks.FindAsync(id);
            if (stack == null)
            {
                return NotFound();
            }

            db.Stacks.Remove(stack);
            await db.SaveChangesAsync();

            return Ok(stack);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StackExists(string id)
        {
            return db.Stacks.Count(e => e.Id == id) > 0;
        }
    }
}