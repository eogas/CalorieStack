using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalorieStack.Models;

namespace CalorieStack.Controllers
{
    public class HomeController : Controller
    {
        CalorieStackContext context = new CalorieStackContext();

        public ActionResult Index()
        {
            ViewBag.Title = "CalorieStack";

            return View();
        }

        public ActionResult Stack(string id)
        {
            ViewBag.Title = "CalorieStack";

            var day = context.Days.FirstOrDefault(d => d.StackId == id && d.Date == DateTime.Today);
            
            // If the sample Stack doesn't have a Day for today, delete all other sample Days,
            // then create a new Day for today
            if (day == null)
            {
                // TODO: Figure out how to do this better with cascading deletes
                var sampleDays = context.Days.Where(d => d.StackId == "sample");
                var sampleMeals = context.Meals.Where(m => sampleDays.Any(d => d.Id == m.DayId));
                var sampleFoodItems = context.FoodItems.Where(fi => sampleMeals.Any(m => fi.MealId == m.Id));

                context.FoodItems.RemoveRange(sampleFoodItems);
                context.Meals.RemoveRange(sampleMeals);
                context.Days.RemoveRange(sampleDays);

                day = context.Days.Add(Day.CreateSample());

                context.SaveChanges();
            }

            return View(day);
        }
    }
}
