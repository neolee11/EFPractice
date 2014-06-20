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
            modelBuilder.Configurations.Add(new ProductMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}