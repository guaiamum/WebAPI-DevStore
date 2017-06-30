using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");

            HasKey(e => e.Id); //precisa?

            Property(e => e.Title).HasMaxLength(160).IsRequired();
            //Property(e => e.Description).HasMaxLength(150).IsRequired();
            Property(e => e.Price).IsRequired();
            Property(e => e.AcquiredDate).IsRequired();

            HasRequired(e => e.Category);
            
        }
    }
}
