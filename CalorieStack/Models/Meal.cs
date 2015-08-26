using System.Collections.Generic;

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

        internal static List<Meal> CreateSampleSet()
        {
            return new List<Meal>()
            {
                new Meal()
                {
                    Name="Breakfast",
                    Items = new List<FoodItem>()
                    {
                        new FoodItem()
                        {
                            Name="Eggs",
                            Calories=180
                        },
                        new FoodItem()
                        {
                            Name="Bacon",
                            Calories=172
                        },
                        new FoodItem()
                        {
                            Name="Orange Juice",
                            Calories=111
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
                            Calories=254
                        },
                        new FoodItem()
                        {
                            Name="Fries",
                            Calories=365
                        },
                        new FoodItem()
                        {
                            Name="Soda",
                            Calories=182
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
                            Calories=100
                        },
                        new FoodItem()
                        {
                            Name="Beer",
                            Calories=308
                        }
                    }
                }
            };
        }
    }
}