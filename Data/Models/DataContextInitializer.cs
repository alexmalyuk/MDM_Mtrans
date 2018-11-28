using Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class DataContextInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext db)
        {
            db.Nodes.Add(new Node() { Name = "1C", Alias = "1c" });
            db.Nodes.Add(new Node() { Name = "Mtrans ERP", Alias = "erp" });
        }
    }
}
