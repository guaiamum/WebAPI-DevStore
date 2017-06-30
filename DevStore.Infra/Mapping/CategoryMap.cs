using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");

            HasKey(e => e.Id); //precisa?
            Property(e => e.Title).HasMaxLength(60).IsRequired();

        }
    }
}
