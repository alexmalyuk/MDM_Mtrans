using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models
{
    class ContractorAddressConfig : EntityTypeConfiguration<ContractorAddress>
    {
        public ContractorAddressConfig()
        {
            ToTable("ContractorAddreses");

            Property(p => p.Country)
                .HasMaxLength(120);

            Property(p => p.Region)
                .HasMaxLength(120);

            Property(p => p.District)
                .HasMaxLength(120);

            Property(p => p.City)
                .HasMaxLength(120);

            Property(p => p.Street)
                .HasMaxLength(120);

            Property(p => p.House)
                .HasMaxLength(20);

            Property(p => p.Flat)
                .HasMaxLength(20);

            Property(p => p.PostalCode)
                .HasMaxLength(15);

            //HasRequired(a => a.Contractor).WithOptional(c => c.Address).WillCascadeOnDelete(true);
        }

    }
}
