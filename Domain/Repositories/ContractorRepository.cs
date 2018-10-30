using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ContractorRepository : IRepository<Contractor>
    {
        private DataContext db;

        public ContractorRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(Contractor item)
        {
            db.Contractors.Add(item);
        }

        public void Delete(Guid id)
        {
            Contractor item = db.Contractors.Find(id);
            if (item != null)
                db.Contractors.Remove(item);
        }

        public Contractor Get(Guid id)
        {
            return db.Contractors.Find(id);
        }

        public IQueryable<Contractor> GetAll()
        {
            return db.Contractors;
        }

        public void Update(Contractor item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public Contractor GetByINN(string INN)
        {
            return db.Contractors.Where(a => a.INN == INN).FirstOrDefault();
        }
    }
}
