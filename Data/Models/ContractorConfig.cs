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
    class ContractorConfig : EntityTypeConfiguration<Contractor>
    {
        public ContractorConfig()
        {

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            ///TODO: Индекс по INN сделать уникальным
            ///
            Property(p => p.INN)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(p => p.OKPO)
                .HasMaxLength(10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.VATNumber)
                .HasMaxLength(10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            //HasIndex(p => p.Name);
            //HasIndex(p => p.INN);
            //HasIndex(p => p.OKPO);
            //HasIndex(p => p.VATCertificateNumber);

        }
    }
}
