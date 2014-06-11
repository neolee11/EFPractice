using System.Collections.Generic;
using EFPractice.Domain;

namespace EFPractice.DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<EFPractice.DataLayer.SalesModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
            ContextKey = "EFPractice.DataLayer.SalesModelContext";
        }

        protected override void Seed(EFPractice.DataLayer.SalesModelContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var customers = new List<Customer>
            {
                new Customer {FirstName = "John", LastName = "Thompson", DateOfBirth = new DateTime(2000, 1, 3)},
                new Customer {FirstName = "Mike", LastName = "Woodson", DateOfBirth = new DateTime(1990, 1, 3)}
            };

            context.Customers.AddOrUpdate(c => new {c.FirstName, c.LastName}, customers.ToArray());
        }
    }
}
