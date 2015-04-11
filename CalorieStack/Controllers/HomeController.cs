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
                context.Days.RemoveRange(context.Days.Where(d => d.StackId == "sample"));
                day = context.Days.Add(Day.GetSampleDay());

                context.SaveChanges();
            }

            return View(day);
        }
    }
}
