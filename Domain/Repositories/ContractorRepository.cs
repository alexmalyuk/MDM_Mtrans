﻿using Data.Models;
using Data.Models.Core;
using Domain.Models;
using Domain.ViewModels;
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

        public void AddOrUpdate(Contractor item, string currentUserName = "")
        {
            Contractor contractor = db.Contractors.Find(item.Id);
            if (contractor == null)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Unchanged;
            }
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

        public Contractor GetByCodes(ContractorApiModel contractorInfo)
        {
            var q = db.Contractors.Include("Address").Include("Links")
                .Where(c => c.TypeOfCounterparty == contractorInfo.TypeOfCounterparty 
                && c.CountryOfRegistration == contractorInfo.CountryOfRegistration);

            switch (contractorInfo.CountryOfRegistration)
            {
                case Data.CountryEnum.UA:

                    if (contractorInfo.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                        q = q.Where(c => c.INN == contractorInfo.INN);

                    else if (contractorInfo.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                        q = q.Where(c => c.OKPO == contractorInfo.OKPO);

                    break;

                case Data.CountryEnum.RU:

                    q = q.Where(c => c.INN == contractorInfo.INN);
                    break;

                default:

                    q = q.Where(c => c.INN == contractorInfo.INN);
                    break;

            }

            if (contractorInfo.IsBranch)
            {
                q = q.Where(c => c.BranchCode == contractorInfo.BranchCode);
            }

            return q.FirstOrDefault();

        }


        public Contractor GetByNativeId(string nativeId, string nodeAlias)
        {
            var q = db.Contractors.Where(c => c.Links.Where(l => l.NativeId == nativeId && l.Node.Alias == nodeAlias).Any());
            return q.FirstOrDefault();
        }

    }
}
