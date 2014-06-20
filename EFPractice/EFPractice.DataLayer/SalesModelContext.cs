using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using EFPractice.DataLayer.Migrations;
using EFPractice.Domain;

namespace EFPractice.DataLayer
{
    public class SalesModelContext : DbContext
    {
        public SalesModelContext()
            : base("name=MyLocalSQLServer")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesModelContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<SalesModelContext>());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem>  LineItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Move into its own configuration class
            //modelBuilder.Entity<ContactDetail>().HasKey(c => c.CustomerId);
            //modelBuilder.Entity<ContactDetail>().Property(c => c.MobilePhone).HasColumnName("Cell Phone");
            //modelBuilder.Entity<ContactDetail>().HasRequired(cd => cd.Customer).WithOptional(c => c.ContactDetail);
            modelBuilder.Configurations.Add(new ContactDetailMappings());

            //modelBuilder.Ignore<Category>(); //in case you don't want it to be included in the model

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //foreach (var VARIABLE in this.ChangeTracker.Entries().Where(e => e.))
            //{
                
            //}
            return base.SaveChanges();
        }
    }



    public class ContactDetailMappings : EntityTypeConfiguration<ContactDetail>
    {
        public ContactDetailMappings()
        {
            this.HasKey(c => c.CustomerId);
            this.Property(c => c.MobilePhone).HasColumnName("CellMobilePhone");
            this.HasRequired(cd => cd.Customer).WithOptional(c => c.ContactDetail);
        }
    }
}