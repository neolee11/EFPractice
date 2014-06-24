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
            Database.SetInitializer(new DropCreateDatabaseAlways<CoreModelContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Test");
            //modelBuilder.Configurations.Add(new ProductMapping());

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<OnlineCourse>().ToTable("OnlineCourse");
            modelBuilder.Entity<OnSiteCourse>().ToTable("OnsiteCourse");


            modelBuilder.Configurations.Add(new InstructorMapping());
            modelBuilder.Configurations.Add(new OfficeMapping());
            modelBuilder.Configurations.Add(new AssignedParkingMapping());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class InstructorMapping : EntityTypeConfiguration<Instructor>
    {
        public InstructorMapping()
        {
            this.HasKey(i => i.Id);

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
}