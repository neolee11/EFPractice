using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EFPractice.Core;

namespace EFPractice.CoreDataLayer
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasKey(p => p.SomePrimeK);
            this.Property(p => p.SomePrimeK).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}