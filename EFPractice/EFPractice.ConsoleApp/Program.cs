using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPractice.CoreDataLayer;
using EFPractice.DataLayer;
using EFPractice.DataLayer.Migrations;
using EFPractice.Domain;

namespace EFPractice.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunSalesModelContext();
            RunCoreModelContext();
        }

        private static void RunCoreModelContext()
        {
            Console.WriteLine("**Start Core Context");
            //InsertProduct();
            //GetProducts();

            GetCourses();
            Console.WriteLine("**End Core Context");
            Console.ReadLine();
        }

        private static void GetCourses()
        {
            using (var ctx = new CoreModelContext())
            {
                Console.WriteLine("In Get Courses");
                var courses = ctx.OnlineCourses.ToList();
                foreach (var onlineCourse in courses)
                {
                    Console.WriteLine("Course " + onlineCourse.Id);
                }
            }
        }

        //private static void InsertProduct()
        //{
        //    using (var ctx = new CoreModelContext())
        //    {
        //        //var product1 = new EFPractice.Core.Product { SomePrimeK = 1, Name = "iPhone 5", Price = 300.00M };
        //        var product1 = new EFPractice.Core.Product { Name = "iPhone 4", Price = 200.00M };
        //        var product2 = new EFPractice.Core.Product { Name = "iPhone 5", Price = 300.00M };
        //        var product3 = new EFPractice.Core.Product { Name = "iPhone 6", Price = 400.00M };

        //        ctx.Products.Add(product1);
        //        ctx.Products.Add(product2);
        //        ctx.Products.Add(product3);

        //        ctx.SaveChanges();
        //    }
        //}

        //private static void GetProducts()
        //{
        //    using (var ctx = new CoreModelContext())
        //    {
        //        var products = ctx.Products.ToList();
        //        foreach (var product in products)
        //        {
        //            Console.WriteLine(string.Format("Product {0} - {1} - {2}", product.SomePrimeK, product.Name, product.Price));
        //        }
        //    }
        //}




        private static void RunSalesModelContext()
        {
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
                var customer = new Customer { FirstName = "First", LastName = "Last", DateOfBirth = new DateTime(1980, 1, 5) };
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
