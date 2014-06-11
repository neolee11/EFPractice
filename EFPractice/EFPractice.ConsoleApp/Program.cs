using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPractice.DataLayer;
using EFPractice.DataLayer.Migrations;
using EFPractice.Domain;

namespace EFPractice.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesModelContext, Configuration>());

            Console.WriteLine("Before");
            InsertCustomer();
            GetCustomers();
            Console.WriteLine("After");

            Console.ReadLine();
        }

        private static void InsertCustomer()
        {
            using (var ctx = new SalesModelContext())
            {
                var customer = new Customer {FirstName = "First", LastName = "Last", DateOfBirth = new DateTime(1980, 1, 5)};
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
            }
        }

        private static void GetCustomers()
        {
            using (var ctx = new SalesModelContext())
            {
                var customers = ctx.Customers.ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.FirstName);
                }
            }
        }

        
    }
}
