using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            using(DataContext db = new DataContext())
            {
                Contractor c1 = new Contractor() { Name = "Каргилл", FullName = "ООО Каргилл" };
                db.Contractors.Add(c1);
                db.SaveChanges();

            }


        }
    }
}
