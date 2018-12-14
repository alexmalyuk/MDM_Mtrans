using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Core
{
    public class Link
    {
        public Guid Id { get; set; }
        public TypeOfSubjectEnum TypeOfSubject { get; set; }
        public string NativeId { get; set; }
        public Guid NodeId { get; set; }
        public Node Node { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t - {3}", Node.Alias, TypeOfSubject, NativeId, Subject);
        }

        public Link()
        {
            Id = Guid.NewGuid();
        }
    }

    class LinkConfig : EntityTypeConfiguration<Link>
    {
        public LinkConfig()
        {
            //Property(p => p.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Property(p => p.ContractorId)
            //    .IsRequired()
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            //Property(p => p.NodeId)
            //    .IsRequired()
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            Property(p => p.NativeId)
                .IsRequired()
                .HasMaxLength(36);
                //.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            HasRequired(p => p.Node).WithMany(p => p.Links).WillCascadeOnDelete(false);

            HasKey(l => new { l.SubjectId, l.NodeId });

        }
    }
}
