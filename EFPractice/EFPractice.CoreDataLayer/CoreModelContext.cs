using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using EFPractice.Core;

namespace EFPractice.CoreDataLayer
{
    public class CoreModelContext : DbContext
    {
        //public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<OnlineCourse> OnlineCourses { get; set; }
        public DbSet<OnSiteCourse> OnSiteCourses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<AssignedParking> AssignedParkings { get; set; }

        public CoreModelContext()
            : base("EFCoreConnStr")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesModelContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<CoreModelContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CoreModelContext>());
            Database.SetInitializer(new MyDropCreateDatabaseIfModelChangesInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Test");
            //modelBuilder.Configurations.Add(new ProductMapping());

            //Using Table-Per-Type Mapping
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<OnlineCourse>().ToTable("OnlineCourse");
            modelBuilder.Entity<OnSiteCourse>().ToTable("OnsiteCourse");

            //modelBuilder.Entity<Student>().Map<Student>(s => s.re)
            
            modelBuilder.Configurations.Add(new InstructorMapping());
            modelBuilder.Configurations.Add(new OfficeMapping());
            modelBuilder.Configurations.Add(new AssignedParkingMapping());

            base.OnModelCreating(modelBuilder);
        }

    }


    public class MyDropCreateDatabaseAlwaysInitializer : DropCreateDatabaseAlways<CoreModelContext>
    {
        protected override void Seed(CoreModelContext context)
        {
            var department = new Department() {Name = "Computer Science", Budget = 1000000.00m, StartDate = new DateTime(1990, 1, 1)};
            context.Departments.Add(department);
            base.Seed(context);
        }
    }

    public class MyDropCreateDatabaseIfModelChangesInitializer : DropCreateDatabaseIfModelChanges<CoreModelContext>
    {
        protected override void Seed(CoreModelContext context)
        {
            var department = new Department() { Name = "Computer Science", Budget = 1000000.00m, StartDate = new DateTime(1990, 1, 1) };
            context.Departments.Add(department);
            base.Seed(context);
        }
    }

    public class InstructorMapping : EntityTypeConfiguration<Instructor>
    {
        public InstructorMapping()
        {
            this.HasKey(i => i.Id);
            this.Property(i => i.FirstName).HasMaxLength(50);
            this.Property(i => i.LastName).HasMaxLength(50);

            this.HasMany(i => i.Courses).WithMany(c => c.Instructors).Map(
                m =>
                {
                    m.ToTable("AscCourseInstructor");
                    m.MapLeftKey("InstructorId"); //instructor
                    m.MapRightKey("CourseId"); // course
                }
                );
            
        }
    }

    public class OfficeMapping : EntityTypeConfiguration<Office>
    {
        public OfficeMapping()
        {
            this.HasKey(o => o.InstructorId);
            this.HasRequired(o => o.Instructor).WithRequiredPrincipal(i => i.Office); //Instructor <-> Office:  1 to 1 relationship 
            this.Property(o => o.Timestamp).IsRowVersion();
        }
    }

    public class AssignedParkingMapping : EntityTypeConfiguration<AssignedParking>
    {
        public AssignedParkingMapping()
        {
            this.HasKey(p => p.InstructorId);
            this.HasRequired(p => p.Instructor).WithOptional(i => i.AssignedParking); //Instructor <-> AssignedParking : 1 to 0/1 Relationship
        }
    }

    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            //this.ToTable("Course");
        }
    }
}