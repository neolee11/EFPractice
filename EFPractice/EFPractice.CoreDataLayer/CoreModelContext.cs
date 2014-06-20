using System.Data.Entity;
using EFPractice.Core;

namespace EFPractice.CoreDataLayer
{
    public class CoreModelContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }

        public CoreModelContext()
            : base("name=EFCoreConnStr")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesModelContext, Configuration>());
            Database.SetInitializer(new DropCreateDatabaseAlways<CoreModelContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Test");

            modelBuilder.Configurations.Add(new ProductMapping());

            modelBuilder.Entity<Student>().Map<Student>(s => s.re)
            
            //modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("Something");

            base.OnModelCreating(modelBuilder);
        }
    }
}