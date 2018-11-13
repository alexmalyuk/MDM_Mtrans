using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models
{
    class DictionaryDataEntityConfig : EntityTypeConfiguration<DictionaryDataEntity>
    {
        public DictionaryDataEntityConfig()
        {
            //Map(m =>
            //{
            //    //m.MapInheritedProperties();
            //    m.ToTable("DictionaryDataEntities");
            //});

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

        }
    }
}
