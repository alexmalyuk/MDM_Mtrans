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
    public class ContractorInfoRepository : IRepository<ContractorInfo>
    {
        private DataContext db;

        public ContractorInfoRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(ContractorInfo item)
        {
            CreateOrUpdate(item);
        }

        public void Update(ContractorInfo item)
        {
            CreateOrUpdate(item);
        }

        public void CreateOrUpdate(ContractorInfo contractorInfo)
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
                contractor = db.Contractors.Where(a => a.Id == link.Subject.Id).FirstOrDefault();
            }

            if (contractor == null)
            {
                contractor = new Contractor();
                contractorRepository.Create(contractor);
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

            if (contractor.LegalAddress != contractorInfo.LegalAddress)
                contractor.LegalAddress = contractorInfo.LegalAddress;

            //if (contractor.CountryCode != contractorInfo.CountryCode)
            //    contractor.CountryCode = contractorInfo.CountryCode;

            if (contractor.TypeOfCounterparty != contractorInfo.TypeOfCounterparty)
                contractor.TypeOfCounterparty = contractorInfo.TypeOfCounterparty;

            // Address
            ContractorAddress contractorAddress = contractor.Address;
            if (contractorAddress == null)
                contractorAddress = new ContractorAddress();

            if (contractorAddress.Street != contractorInfo.Street)
                contractorAddress.Street = contractorInfo.Street;

            if (contractorAddress.House != contractorInfo.House)
                contractorAddress.House = contractorInfo.House;

            if (contractorAddress.Flat != contractorInfo.Flat)
                contractorAddress.Flat = contractorInfo.Flat;

            if (contractorAddress.City != contractorInfo.City)
                contractorAddress.City = contractorInfo.City;

            if (contractorAddress.District != contractorInfo.District)
                contractorAddress.District = contractorInfo.District;

            if (contractorAddress.Region != contractorInfo.Region)
                contractorAddress.Region = contractorInfo.Region;

            if (contractorAddress.PostalCode != contractorInfo.PostalCode)
                contractorAddress.PostalCode = contractorInfo.PostalCode;

            //if (contractorAddress.Country != contractorInfo.Country)
            //    contractorAddress.Country = contractorInfo.Country;
        }

        public void Delete(Guid id)
        {
            //new LinkRepository(db).Delete(id);
            throw new NotImplementedException();
        }

        public ContractorInfo Get(Guid id)
        {
            throw new NotImplementedException();

            //var q = db.Nodes.Join(
            //    db.Links.Where(a => a.Id == id),
            //    n => n.Id,
            //    l => l.NodeId,
            //    (n, l) => new
            //    {
            //        NativeId = l.NativeId,
            //        ContractorId = l.ContractorId,
            //        NodeAlias = n.Alias,
            //        User = l.User
            //    }).Join(
            //    db.Contractors,
            //    l => l.ContractorId,
            //    c => c.Id,
            //    (l, c) => new ContractorInfo
            //    {
            //        NativeId = l.NativeId.ToString(),
            //        Name = c.Name,
            //        FullName = c.FullName,
            //        INN = c.INN,
            //        OKPO = c.OKPO,
            //        VATNumber = c.VATNumber,
            //        LegalAddress = c.LegalAddress,
            //        Id = c.Id,
            //        CountryCode = c.CountryCode,
            //        TypeOfCounterparty = c.TypeOfCounterparty,
            //        User = l.User
            //    });

            //return q.FirstOrDefault();

        }

        public ContractorInfo GetByNativeId(string nativeId, string alias)
        {
            var q = db.Nodes.Include(n => n.Links).Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Where(c => c.NativeId == nativeId).Join(
                db.Contractors.Include(c => c.Address),
                l => l.Subject.Id,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.Node.Alias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
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

        public IEnumerable<ContractorInfo> GetAllByNodeAlias(string alias)
        {
            var q = db.Nodes.Include(n => n.Links).Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Join(
                db.Contractors.Include(c => c.Address),
                l => l.Subject.Id,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.Node.Alias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
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

            //var q = db.Nodes.Where(a => a.Alias == alias).Join(
            //    db.Links,
            //    n => n.Id,
            //    l => l.NodeId,
            //    (n, l) => new
            //    {
            //        NativeId = l.NativeId,
            //        ContractorId = l.ContractorId,
            //        NodeAlias = n.Alias,
            //        User = l.User
            //    }).Join(
            //    db.Contractors,
            //    l => l.ContractorId,
            //    c => c.Id,
            //    (l, c) => new ContractorInfo
            //    {
            //        NativeId = l.NativeId.ToString(),
            //        NodeAlias = l.NodeAlias,
            //        Name = c.Name,
            //        FullName = c.FullName,
            //        INN = c.INN,
            //        OKPO = c.OKPO,
            //        VATNumber = c.VATNumber,
            //        LegalAddress = c.LegalAddress,
            //        Id = c.Id,
            //        CountryCode = c.CountryCode,
            //        TypeOfCounterparty = c.TypeOfCounterparty,
            //        User = l.User
            //    });

            //return q;
        }

        public IQueryable<ContractorInfo> GetAll()
        {
            throw new NotImplementedException();

            //var q = db.Nodes.Join(
            //    db.Links,
            //    n => n.Id,
            //    l => l.NodeId,
            //    (n, l) => new
            //    {
            //        NativeId = l.NativeId,
            //        ContractorId = l.ContractorId,
            //        NodeAlias = n.Alias,
            //        User = l.User
            //    }).Join(
            //    db.Contractors,
            //    l => l.ContractorId,
            //    c => c.Id,
            //    (l, c) => new ContractorInfo
            //    {
            //        NativeId = l.NativeId.ToString(),
            //        NodeAlias = l.NodeAlias,
            //        Name = c.Name,
            //        FullName = c.FullName,
            //        INN = c.INN,
            //        OKPO = c.OKPO,
            //        VATNumber = c.VATNumber,
            //        LegalAddress = c.LegalAddress,
            //        Id = c.Id,
            //        CountryCode = c.CountryCode,
            //        TypeOfCounterparty = c.TypeOfCounterparty,
            //        User = l.User
            //    });

            //return q;
        }

    }
}
