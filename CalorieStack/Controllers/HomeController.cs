using CalorieStack.Models;
using System;
using System.Linq;
using System.Web.Mvc;

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
                day = context.Days.Add(id == "sample" ? Day.CreateSample() : Day.CreateDefault(id));
                context.SaveChanges();
            }

            return View(day);
        }
    }
}
