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
                .HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.OKPO)
                .HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.VATNumber)
                .HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            HasOptional(c => c.Address).WithRequired(a => a.Contractor).WillCascadeOnDelete(true);
        }
    }
}
