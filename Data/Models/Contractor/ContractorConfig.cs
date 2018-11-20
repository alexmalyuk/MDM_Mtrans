using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models
{
    class ContractorConfig : EntityTypeConfiguration<Contractor>
    {
        public ContractorConfig()
        {
            ToTable("Contractors");

            Property(p => p.INN)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.OKPO)
                .HasMaxLength(10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.VATNumber)
                .HasMaxLength(10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            //Property(p => p.CountryCode)
            //    .IsOptional()
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));
        }
    }
}
