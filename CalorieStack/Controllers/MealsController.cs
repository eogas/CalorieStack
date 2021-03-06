﻿using CalorieStack.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CalorieStack.Controllers
{
    public class MealsController : ApiController
    {
        private CalorieStackContext db = new CalorieStackContext();

        // GET: api/Meals
        public IQueryable<Meal> GetMeals()
        {
            return db.Meals;
        }

        // GET: api/Meals/5
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> GetMeal(int id)
        {
            Meal meal = await db.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        // PUT: api/Meals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeal(int id, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> PostMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meals.Add(meal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> DeleteMeal(int id)
        {
            Meal meal = await db.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            await db.SaveChangesAsync();

            return Ok(meal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(int id)
        {
            return db.Meals.Count(e => e.Id == id) > 0;
        }
    }
}