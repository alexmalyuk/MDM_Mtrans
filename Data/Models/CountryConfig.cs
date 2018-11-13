using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class CountryConfig : EntityTypeConfiguration<Country>
    {
        public CountryConfig()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Countries");
            });

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(p => p.Iso)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(p => p.Alpha2)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(p => p.Alpha3)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}
