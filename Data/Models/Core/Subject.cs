using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Core
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Link> Links { get; set; }

        public Subject()
        {
            Links = new List<Link>();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class SubjectConfig : EntityTypeConfiguration<Subject>
    {
        public SubjectConfig()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            HasMany(p => p.Links).WithRequired(p => p.Subject);
        }
    }

}
