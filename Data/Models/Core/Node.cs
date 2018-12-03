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
    public class Node
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public ICollection<Link> Links { get; set; }
        public Node()
        {
            Links = new List<Link>();
        }
    }

    class NodeConfig : EntityTypeConfiguration<Node>
    {
        public NodeConfig()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(p => p.Alias)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            HasMany(p => p.Links).WithRequired(p => p.Node);
        }
    }
}
