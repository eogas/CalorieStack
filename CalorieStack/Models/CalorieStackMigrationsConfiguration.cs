using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace CalorieStack.Models
{
    public class CalorieStackMigrationsConfiguration :
        DbMigrationsConfiguration<CalorieStackContext>
    {
        public CalorieStackMigrationsConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CalorieStackContext context)
        {
            // Seed the db with a sample stack
            context.Stacks.AddOrUpdate(new Stack()
            {
                Id = "sample",
                Days = new List<Day>()
                {
                    Day.GetSampleDay()
                }
            });

            context.SaveChanges();
        }
    }
}
