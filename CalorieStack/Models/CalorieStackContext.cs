using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion
                    <CalorieStackContext, CalorieStackMigrationsConfiguration>()
            );
        }

        // http://stackoverflow.com/a/5277642
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set Id as the key for Stack entries
            modelBuilder.Entity<Stack>().HasKey(s => s.Id);

            // Turn off autogeneration for Stack ids
            modelBuilder.Entity<Stack>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }

        public override Task<int> SaveChangesAsync()
        {
            foreach (var stackEntry in ChangeTracker.Entries<Stack>()
                .Where(e => e.State == EntityState.Added))
            {
                stackEntry.Entity.Id = Stack.GenerateId();
            }

            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            foreach(var stackEntry in ChangeTracker.Entries<Stack>()
                .Where(e => e.State == EntityState.Added))
            {
                stackEntry.Entity.Id = Stack.GenerateId();
            }

            return base.SaveChanges();
        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Day> Days { get; set; }

        public DbSet<Stack> Stacks { get; set; }

    }
}
