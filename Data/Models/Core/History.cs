using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Core
{
    public class HistoryEntry
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public DateTime DateUTC { get; set; }
        public string User { get; set; }
        public Node Node { get; set; }
        public string SubjectXML { get; set; }
        public Subject SubjectSnapshot
        {
            get
            {
                return SubjectXML != null
                    ? Subject.Deserialize(SubjectXML, GetType())
                    : null;
            }
            set
            {
                SubjectXML = value != null 
                    ? value.Serialize() 
                    : null;
            }
        }

        public HistoryEntry()
        {
            DateUTC = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", DateUTC, User, Node);
        }
    }

    class HistoryConfig : EntityTypeConfiguration<HistoryEntry>
    {
        public HistoryConfig()
        {
            Property(h => h.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(h => h.SubjectXML).HasColumnType("xml");

            Property(h => h.User)
                .HasMaxLength(50);

            Ignore(h => h.SubjectSnapshot);

            HasRequired(h => h.Subject).WithMany(s => s.Histories).WillCascadeOnDelete(false);
        }
    }

}
