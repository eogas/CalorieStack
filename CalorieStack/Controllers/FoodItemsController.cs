using CalorieStack.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CalorieStack.Controllers
{
    public class FoodItemsController : ApiController
    {
        private CalorieStackContext db = new CalorieStackContext();

        // GET: api/FoodItems
        public IQueryable<FoodItem> GetFoodItems()
        {
            return db.FoodItems;
        }

        // GET: api/FoodItems/5
        [ResponseType(typeof(FoodItem))]
        public async Task<IHttpActionResult> GetFoodItem(int id)
        {
            FoodItem foodItem = await db.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return Ok(foodItem);
        }

        // PUT: api/FoodItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFoodItem(int id, FoodItem foodItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodItem.Id)
            {
                return BadRequest();
            }

            db.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
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

        // POST: api/FoodItems
        [ResponseType(typeof(FoodItem))]
        public async Task<IHttpActionResult> PostFoodItem(FoodItem foodItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodItems.Add(foodItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = foodItem.Id }, foodItem);
        }

        // DELETE: api/FoodItems/5
        [ResponseType(typeof(FoodItem))]
        public async Task<IHttpActionResult> DeleteFoodItem(int id)
        {
            FoodItem foodItem = await db.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            db.FoodItems.Remove(foodItem);
            await db.SaveChangesAsync();

            return Ok(foodItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodItemExists(int id)
        {
            return db.FoodItems.Count(e => e.Id == id) > 0;
        }
    }
}