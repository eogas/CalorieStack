using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public string Name { get; set; }
        public int Calories { get; set; }
    }
}