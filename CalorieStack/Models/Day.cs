using System;
using System.Collections.Generic;

namespace CalorieStack.Models
{
    public class Day
    {
        public int Id { get; set; }

        public string StackId { get; set; }

        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }

        public static Day CreateDefault(string stackId)
        {
            return new Day()
            {
                StackId = stackId,
                Date = DateTime.Today,
                Meals = Meal.CreateDefaultSet()
            };
        }

        public static Day CreateSample()
        {
            return new Day()
            {
                StackId = "sample",
                Date = DateTime.Today,
                Meals = Meal.CreateSampleSet()
            };
        }
    }
}