using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public int DayId { get; set; }

        public string Name { get; set; }
        public List<FoodItem> Items { get; set; }

        public static List<Meal> CreateDefaultSet()
        {
            return new List<Meal>()
            {
                new Meal() { Name="Breakfast", Items=new List<FoodItem>() },
                new Meal() { Name="Lunch", Items=new List<FoodItem>() },
                new Meal() { Name="Dinner", Items=new List<FoodItem>() }
            };
        }
    }
}