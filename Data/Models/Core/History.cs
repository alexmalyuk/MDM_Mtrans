using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Core
{
    public class History
    {
        public Subject Subject { get; set; }
        public DateTime DateUTC { get; set; }
        public string User { get; set; }
        public Node Node { get; set; }
        public string XMLSnapshot { get; set; }

        ///TODO: About saving a snapshot of an entity in a database
        /// https://stackoverflow.com/questions/27711671/entity-framework-saving-a-snapshot-of-a-document-in-a-sql-database

        public History()
        {
            DateUTC = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", DateUTC, User, Node);
        }
    }

    class HistoryConfig : EntityTypeConfiguration<History>
    {
        public HistoryConfig()
        {

        }
    }

}
