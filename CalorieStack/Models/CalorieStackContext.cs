using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class CalorieStackContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CalorieStackContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Day> Days { get; set; }

        public DbSet<Stack> Stacks { get; set; }

    }
}
