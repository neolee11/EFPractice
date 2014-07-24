using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPractice.Core;
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
            //InsertDepartment();
            //GetInstructor();
            //InsertProduct();
            //GetProducts();
            //InsertCourse();
            //DeleteCourse();
            //InsertInstructorParking();
            //GetCourses();

            //var course = GetCourse("Market Efficiency");
            //if (course != null)
            //{
            //    course.Credits = 8;
            //    UpdateCourse(course);
            //}

            var onlineCourse = GetOnlineCourse("Micro Economy");
            if (onlineCourse != null)
            {
                onlineCourse.Credits = 7;
                onlineCourse.Url = "http://www.test.com";
                UpdateOnlineCourseUsingSetValue(onlineCourse);
            }

            Console.WriteLine("**End Core Context");
            Console.ReadLine();
        }

        private static void UpdateCourse(Course course)
        {
            using (var ctx = new CoreModelContext())
            {
                ctx.Entry(course).State = course.Id == 0 ? EntityState.Added : EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        private static void UpdateOnlineCourseUsingSetValue(Course course)
        {
            using (var ctx = new CoreModelContext())
            {
                //var entry = ctx.Courses.Where(c => c.Id == course.Id).FirstOrDefault();
                ctx.OnlineCourses.Load();
                var entry =
                    ctx.ChangeTracker.Entries<OnlineCourse>().FirstOrDefault(e => ((int)e.CurrentValues["Id"]) == course.Id);

                if (entry != null)
                {
                    entry.CurrentValues.SetValues(course); //Great way to use DTO objects to set only subset of properties without empty out other property values
                    ctx.SaveChanges();
                }
            }
        }

        private static Course GetCourse(string courseName)
        {
            using (var ctx = new CoreModelContext())
            {
                return ctx.Courses.FirstOrDefault(c => c.Title == courseName);
            }
        }

        private static OnlineCourse GetOnlineCourse(string courseName)
        {
            using (var ctx = new CoreModelContext())
            {
                return ctx.OnlineCourses.FirstOrDefault(c => c.Title == courseName);
            }
        }

        private static void DeleteCourse()
        {
            using (var ctx = new CoreModelContext())
            {
                var course = ctx.Courses.FirstOrDefault(c => c.Title == "Market Efficiency");
                ctx.Courses.Remove(course);
                ctx.SaveChanges();
            }
        }
        private static void GetInstructor()
        {
            using (var ctx = new CoreModelContext())
            {
                var query = ctx.Instructors.FirstOrDefault();
                if (query != null)
                {
                    var msg = string.Format("Instructor {0} : {1} {2} - Office at {3}", query.Id.ToString(),
                        query.FirstName, query.LastName, query.Office.Location);

                    Console.WriteLine(msg);
                }
            }
        }

        private static void InsertInstructorParking()
        {
            using (var ctx = new CoreModelContext())
            {
                var instructor = ctx.Instructors.First(i => i.LastName == "Li");
                instructor.AssignedParking = new AssignedParking() { SpaceNumber = "PS 1" };

                ctx.SaveChanges();
            }
        }

        private static void InsertDepartment()
        {
            using (var ctx = new CoreModelContext())
            {
                var department = new Department
                {
                    Name = "Economy",
                    Budget = 200000m,
                    StartDate = System.DateTime.Now,
                    Courses = new List<Course>
                    {
                        new OnlineCourse()
                        {
                            Title = "Micro Economy",
                            Credits = 3,
                            Url = "http://www.microeconomy.com",
                            Instructors =
                                new List<Instructor>()
                                {
                                    new Instructor()
                                    {
                                        FirstName = "Daniel",
                                        LastName = "Li",
                                        Gender = EGender.Male,
                                        Office = new Office() {Location = "Building 5"}
                                    }
                                }
                        }
                    }
                };

                if (ctx.Departments.Any(d => d.Name == "Economy") == false)
                {
                    Console.WriteLine("Insert new Department");
                    ctx.Departments.Add(department);
                    ctx.SaveChanges();
                    Console.WriteLine("End of Insert new Department");
                }
            }
        }

        private static void InsertCourse()
        {
            using (var ctx = new CoreModelContext())
            {
                var courseTitle = "Market Efficiency";

                var course = new OnSiteCourse()
                {
                    Title = courseTitle,
                    Credits = 2,
                    Department = ctx.Departments.First(d => d.Name == "Economy"),
                    Instructors = new List<Instructor>()
                    {
                        //new Instructor()
                        //{
                        //    FirstName = "Jennifer",
                        //    LastName = "Lopez",
                        //    Gender = EGender.Female,
                        //    Office = new Office() {Location = "Building 1"}
                        //},
                        ctx.Instructors.First(i => i.LastName == "Lopez"),
                        ctx.Instructors.First(i => i.LastName == "Li")
                    },
                    Details = new Details() { DurationDays = 90, Location = "Building 5", Time = System.DateTime.Now }
                };

                if (ctx.Courses.Any(c => c.Title == courseTitle) == false)
                {
                    Console.WriteLine("Insert new course " + courseTitle);
                    ctx.Courses.Add(course);
                    ctx.SaveChanges();
                    Console.WriteLine("Finish inserting new course " + courseTitle);
                }
            }
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
