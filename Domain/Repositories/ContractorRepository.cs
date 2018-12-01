using Data.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ContractorRepository : IRepository<Data.Models.Contractor>
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
            return db.Contractors.Include("Address").Where(c => c.Id == id).FirstOrDefault();
        }

        public IQueryable<Contractor> GetAll()
        {
            return db.Contractors;
        }

        public void Update(Contractor item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public Contractor GetByCodes(ContractorInfo contractorInfo)
        {
            var q = db.Contractors.Include("Address").Where(c => c.TypeOfCounterparty == contractorInfo.TypeOfCounterparty && c.CountryOfRegistration == contractorInfo.CountryOfRegistration);

            switch (contractorInfo.CountryOfRegistration)
            {
                case Data.CountryEnum.UA:

                    if (contractorInfo.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                        return q.Where(c => c.INN == contractorInfo.INN).FirstOrDefault();

                    else if (contractorInfo.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                        return q.Where(c => c.OKPO == contractorInfo.OKPO).FirstOrDefault();

                    break;

                case Data.CountryEnum.RU:
                    return q.Where(c => c.INN == contractorInfo.INN).FirstOrDefault();

            }

            return null;
        }

        public Contractor GetByNativeId(string nativeId, string alias)
        {
            throw new NotImplementedException();
            
            //Link link = new LinkRepository(db).GetByNativeId(nativeId, alias);
            //return db.Contractors.Where(a => a.Id == link.ContractorId).FirstOrDefault();
        }
    }
}
