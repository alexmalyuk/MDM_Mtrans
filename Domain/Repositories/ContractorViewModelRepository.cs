using Data.Models;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ContractorViewModelRepository : IRepository<ContractorViewModel>
    {
        private DataContext db;

        public ContractorViewModelRepository(DataContext db)
        {
            this.db = db;
        }

        public void AddOrUpdate(ContractorViewModel model)
        {
            Contractor contractor = db.Contractors.Include(c => c.Address).Where(a => a.Id == model.Id).FirstOrDefault();

            if (contractor == null)
            {
                contractor = new Contractor();
                db.Entry(contractor).State = EntityState.Added;
            }
            else
            {
                db.Entry(contractor).State = EntityState.Unchanged;
            }

            if (contractor.Name != model.Name)
                contractor.Name = model.Name;

            if (contractor.FullName != model.FullName)
                contractor.FullName = model.FullName;

            if (contractor.INN != model.INN)
                contractor.INN = model.INN;

            if (contractor.OKPO != model.OKPO)
                contractor.OKPO = model.OKPO;

            if (contractor.VATNumber != model.VATNumber)
                contractor.VATNumber = model.VATNumber;

            if (contractor.CountryOfRegistration != model.CountryOfRegistration)
                contractor.CountryOfRegistration = model.CountryOfRegistration;

            if (contractor.TypeOfCounterparty != model.TypeOfCounterparty)
                contractor.TypeOfCounterparty = model.TypeOfCounterparty;

            // Address
            ContractorAddress address = contractor.Address;
            if (address == null)
            {
                address = new ContractorAddress();
                contractor.Address = address;
                db.Entry(address).State = EntityState.Added;
            }
            else
            {
                db.Entry(address).State = EntityState.Unchanged;
            }


            if (address.Street != model.Street)
                address.Street = model.Street;

            if (address.House != model.House)
                address.House = model.House;

            if (address.Flat != model.Flat)
                address.Flat = model.Flat;

            if (address.City != model.City)
                address.City = model.City;

            if (address.District != model.District)
                address.District = model.District;

            if (address.Region != model.Region)
                address.Region = model.Region;

            if (address.PostalCode != model.PostalCode)
                address.PostalCode = model.PostalCode;

            if (address.Country != model.Country)
                address.Country = model.Country;

            if (address.StringRepresentedAddress != model.StringRepresentedAddress)
                address.StringRepresentedAddress = model.StringRepresentedAddress;
        }

        public void Delete(Guid id)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor != null)
                db.Entry(contractor).State = EntityState.Deleted;

            ContractorAddress adress = db.ContractorAddreses.Find(id);
            if (adress != null)
                db.Entry(adress).State = EntityState.Deleted;
        }

        public ContractorViewModel Get(Guid id)
        {
            var q = db.Contractors.Include("Address").Where(c => c.Id == id)
                .Select(c => new ContractorViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    FullName = c.FullName,
                    CountryOfRegistration = c.CountryOfRegistration,
                    TypeOfCounterparty = c.TypeOfCounterparty,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    Street = c.Address.Street,
                    House = c.Address.House,
                    Flat = c.Address.Flat,
                    District = c.Address.District,
                    Region = c.Address.Region,
                    City = c.Address.City,
                    Country = c.Address.Country,
                    PostalCode = c.Address.PostalCode,
                    StringRepresentedAddress = c.Address.StringRepresentedAddress
                });

            return q.FirstOrDefault();
        }

        public IQueryable<ContractorViewModel> GetAll()
        {
            var q = db.Contractors.Include("Address")
                .Select(c => new ContractorViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    FullName = c.FullName,
                    CountryOfRegistration = c.CountryOfRegistration,
                    TypeOfCounterparty = c.TypeOfCounterparty,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    Street = c.Address.Street,
                    House = c.Address.House,
                    Flat = c.Address.Flat,
                    District = c.Address.District,
                    Region = c.Address.Region,
                    City = c.Address.City,
                    Country = c.Address.Country,
                    PostalCode = c.Address.PostalCode,
                    StringRepresentedAddress = c.Address.StringRepresentedAddress
                });

            return q;
        }

    }
}
