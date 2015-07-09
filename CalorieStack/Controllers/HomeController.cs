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
            
            if (day == null)
            {
                // Create the current Day if it is missing
                day = context.Days.Add(Day.CreateDefault());
                context.SaveChanges();
            }

            return View(day);
        }
    }
}
