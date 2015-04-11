using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class Day
    {
        public int Id { get; set; }

        public string StackId { get; set; }

        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }

        public static Day GetSampleDay()
        {
            return new Day()
            {
                StackId = "sample",
                Date = DateTime.Today,
                Meals = new List<Meal>()
                {
                    new Meal()
                    {
                        Name="Breakfast",
                        Items = new List<FoodItem>()
                        {
                            new FoodItem()
                            {
                                Name="Eggs",
                                Calories=123
                            },
                            new FoodItem()
                            {
                                Name="Bacon",
                                Calories=456
                            }
                        }
                    },
                    new Meal()
                    {
                        Name="Lunch",
                        Items = new List<FoodItem>()
                        {
                            new FoodItem()
                            {
                                Name="Burger",
                                Calories=222
                            },
                            new FoodItem()
                            {
                                Name="Fries",
                                Calories=333
                            }
                        }
                    },
                    new Meal()
                    {
                        Name="Dinner",
                        Items = new List<FoodItem>()
                        {
                            new FoodItem()
                            {
                                Name="Salad",
                                Calories=100
                            },
                            new FoodItem()
                            {
                                Name="Yogurt",
                                Calories=200
                            }
                        }
                    }
                }
            };
        }
    }
}