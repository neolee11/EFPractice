using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPractice.DataLayer;
using EFPractice.DataLayer.Migrations;

namespace EFPractice.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesModelContext, Configuration>());

            Console.WriteLine("Before");
            GetCustomers();
            Console.WriteLine("After");

            Console.ReadLine();
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
