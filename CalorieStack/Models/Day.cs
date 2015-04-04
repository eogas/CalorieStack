using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class Day
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }
    }
}