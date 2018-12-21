using Data.Models;
using Data.Models.Core;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ContractorApiModelRepository : IRepository<ContractorApiModel>
    {
        private DataContext db;

        public ContractorApiModelRepository(DataContext db)
        {
            this.db = db;
        }

        public void AddOrUpdate(ContractorApiModel contractorInfo, string currentUserName="")
        {

            Contractor contractor = null;
            ContractorRepository contractorRepository = new ContractorRepository(db);
            Node node = new NodeRepository(db).GetByAlias(contractorInfo.NodeAlias);
            Link link = node.Links.Where(c => c.TypeOfSubject == Data.TypeOfSubjectEnum.Contractor && c.NativeId == contractorInfo.NativeId).FirstOrDefault();

            if (link == null)
            {
                // нет линка - ищем контрагента по кодам
                contractor = contractorRepository.GetByCodes(contractorInfo);
                link = new Link() { Node = node, NativeId = contractorInfo.NativeId, TypeOfSubject = Data.TypeOfSubjectEnum.Contractor };
            }
            else
            {
                contractor = db.Contractors.Include(c => c.Address).Where(a => a.Id == link.Subject.Id).FirstOrDefault();
            }

            if (contractor == null)
            {
                contractor = new Contractor();
                contractorRepository.AddOrUpdate(contractor);
            }
            else
            {
                db.Entry(contractor).State = EntityState.Unchanged;
            }

            if (!contractor.Links.Contains(link))
                contractor.Links.Add(link);

            if (contractor.Name != contractorInfo.Name)
                contractor.Name = contractorInfo.Name;

            if (contractor.FullName != contractorInfo.FullName)
                contractor.FullName = contractorInfo.FullName;

            if (contractor.INN != contractorInfo.INN)
                contractor.INN = contractorInfo.INN;

            if (contractor.OKPO != contractorInfo.OKPO)
                contractor.OKPO = contractorInfo.OKPO;

            if (contractor.VATNumber != contractorInfo.VATNumber)
                contractor.VATNumber = contractorInfo.VATNumber;

            if (contractor.CountryOfRegistration != contractorInfo.CountryOfRegistration)
                contractor.CountryOfRegistration = contractorInfo.CountryOfRegistration;

            if (contractor.TypeOfCounterparty != contractorInfo.TypeOfCounterparty)
                contractor.TypeOfCounterparty = contractorInfo.TypeOfCounterparty;

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

            if ((!string.IsNullOrEmpty(contractorInfo.Street)) && (address.Street != contractorInfo.Street))
                address.Street = contractorInfo.Street;

            if ((!string.IsNullOrEmpty(contractorInfo.House)) && (address.House != contractorInfo.House))
                address.House = contractorInfo.House;

            if ((!string.IsNullOrEmpty(contractorInfo.Flat)) && (address.Flat != contractorInfo.Flat))
                address.Flat = contractorInfo.Flat;

            if ((!string.IsNullOrEmpty(contractorInfo.City)) && (address.City != contractorInfo.City))
                address.City = contractorInfo.City;

            if ((!string.IsNullOrEmpty(contractorInfo.District)) && (address.District != contractorInfo.District))
                address.District = contractorInfo.District;

            if ((!string.IsNullOrEmpty(contractorInfo.Region)) && (address.Region != contractorInfo.Region))
                address.Region = contractorInfo.Region;

            if ((!string.IsNullOrEmpty(contractorInfo.PostalCode)) && (address.PostalCode != contractorInfo.PostalCode))
                address.PostalCode = contractorInfo.PostalCode;

            if ((!string.IsNullOrEmpty(contractorInfo.Country)) && (address.Country != contractorInfo.Country))
                address.Country = contractorInfo.Country;

            if ((!string.IsNullOrEmpty(contractorInfo.StringRepresentedAddress)) && (address.StringRepresentedAddress != contractorInfo.StringRepresentedAddress))
                address.StringRepresentedAddress = contractorInfo.StringRepresentedAddress;
            
            // History
            HistoryEntry historyEntry = new HistoryEntry();
            historyEntry.User = contractorInfo.User;
            historyEntry.SubjectSnapshot = contractor;
            contractor.Histories.Add(historyEntry);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ContractorApiModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ContractorApiModel GetByNativeId(string nativeId, string alias)
        {
            var q = db.Nodes.Include(n => n.Links).Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Where(c => c.NativeId == nativeId).Join(
                db.Contractors.Include(c => c.Address),
                l => l.Subject.Id,
                c => c.Id,
                (l, c) => new ContractorApiModel
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.Node.Alias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    CountryOfRegistration = c.CountryOfRegistration,
                    TypeOfCounterparty = c.TypeOfCounterparty,
                    Street = c.Address.Street,
                    House = c.Address.House,
                    Flat = c.Address.Flat,
                    City = c.Address.City,
                    District = c.Address.District,
                    Region = c.Address.Region,
                    PostalCode = c.Address.PostalCode,
                    Country = c.Address.Country,
                    StringRepresentedAddress = c.Address.StringRepresentedAddress
                });

            return q.FirstOrDefault();
        }

        public IEnumerable<ContractorApiModel> GetAllByNodeAlias(string alias)
        {
            var q = db.Nodes.Include(n => n.Links).Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Join(
                db.Contractors.Include(c => c.Address),
                l => l.Subject.Id,
                c => c.Id,
                (l, c) => new ContractorApiModel
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.Node.Alias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    CountryOfRegistration = c.CountryOfRegistration,
                    TypeOfCounterparty = c.TypeOfCounterparty,
                    Street = c.Address.Street,
                    House = c.Address.House,
                    Flat = c.Address.Flat,
                    City = c.Address.City,
                    District = c.Address.District,
                    Region = c.Address.Region,
                    PostalCode = c.Address.PostalCode,
                    Country = c.Address.Country,
                    StringRepresentedAddress = c.Address.StringRepresentedAddress
                });

            return q.ToList();
        }

        public IQueryable<ContractorApiModel> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
