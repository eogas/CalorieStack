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
    public class DaysController : ApiController
    {
        private CalorieStackContext db = new CalorieStackContext();

        // GET: api/Days
        public IQueryable<Day> GetDays()
        {
            return db.Days;
        }

        // GET: api/Days/5
        [ResponseType(typeof(Day))]
        public async Task<IHttpActionResult> GetDay(int id)
        {
            Day day = await db.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            return Ok(day);
        }

        // GET: api/Days/5/2015/04/10
        [ResponseType(typeof(Day))]
        public async Task<IHttpActionResult> GetDay(string stackId, int year, int month, int dayOfMonth)
        {
            Day day = await db.Days.Include(d => d.Meals.Select(m => m.Items)).FirstOrDefaultAsync(d =>
                d.StackId == stackId &&
                d.Date.Year == year &&
                d.Date.Month == month &&
                d.Date.Day == dayOfMonth
            );

            if (day == null)
            {
                return NotFound();
            }

            return Ok(day);
        }

        // PUT: api/Days/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDay(int id, Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != day.Id)
            {
                return BadRequest();
            }

            db.Entry(day).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Days
        [ResponseType(typeof(Day))]
        public async Task<IHttpActionResult> PostDay(Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Days.Add(day);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = day.Id }, day);
        }

        // DELETE: api/Days/5
        [ResponseType(typeof(Day))]
        public async Task<IHttpActionResult> DeleteDay(int id)
        {
            Day day = await db.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            db.Days.Remove(day);
            await db.SaveChangesAsync();

            return Ok(day);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DayExists(int id)
        {
            return db.Days.Count(e => e.Id == id) > 0;
        }
    }
}