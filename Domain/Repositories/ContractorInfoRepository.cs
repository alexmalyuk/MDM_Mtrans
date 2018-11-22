using Data.Models;
using Data.Models.Core;
using Domain.Models;
using System;
using System.Collections.Generic;
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
                db.Entry(contractor).State = System.Data.Entity.EntityState.Unchanged;
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

            if (contractor.CountryCode != contractorInfo.CountryCode)
                contractor.CountryCode = contractorInfo.CountryCode;

            if (contractor.TypeOfCounterparty != contractorInfo.TypeOfCounterparty)
                contractor.TypeOfCounterparty = contractorInfo.TypeOfCounterparty;



            //LinkRepository linkRepository = new LinkRepository(db);
            //ContractorRepository contractorRepository = new ContractorRepository(db);

            //Node node = new NodeRepository(db).GetByAlias(contractorInfo.NodeAlias);
            //Contractor contractor = null;
            //Link link = linkRepository.GetByNativeId(contractorInfo.NativeId, contractorInfo.NodeAlias);

            //if (link == null)
            //{
            //    // нет линка - ищем контрагента по ИНН
            //    contractor = contractorRepository.GetByINN(contractorInfo.INN);
            //}
            //else
            //{
            //    contractor = db.Contractors.Where(a => a.Id == link.ContractorId).FirstOrDefault();
            //}

            //// Контрагент
            //if (contractor == null)
            //{
            //    contractor = new Contractor();
            //    contractorRepository.Create(contractor);
            //}
            //else
            //{
            //    db.Entry(contractor).State = System.Data.Entity.EntityState.Unchanged;
            //}

            //if (contractor.Name != contractorInfo.Name)
            //    contractor.Name = contractorInfo.Name;

            //if (contractor.FullName != contractorInfo.FullName)
            //    contractor.FullName = contractorInfo.FullName;

            //if (contractor.INN != contractorInfo.INN)
            //    contractor.INN = contractorInfo.INN;

            //if (contractor.OKPO != contractorInfo.OKPO)
            //    contractor.OKPO = contractorInfo.OKPO;

            //if (contractor.VATNumber != contractorInfo.VATNumber)
            //    contractor.VATNumber = contractorInfo.VATNumber;

            //if (contractor.LegalAddress != contractorInfo.LegalAddress)
            //    contractor.LegalAddress = contractorInfo.LegalAddress;

            //if (contractor.CountryCode != contractorInfo.CountryCode)
            //    contractor.CountryCode = contractorInfo.CountryCode;

            //if (contractor.TypeOfCounterparty != contractorInfo.TypeOfCounterparty)
            //    contractor.TypeOfCounterparty = contractorInfo.TypeOfCounterparty;

            //// Link
            //if (link == null)
            //{
            //    link = new Link();
            //    linkRepository.Create(link);
            //}
            //else
            //{
            //    //linkRepository.Update(link);
            //    db.Entry(link).State = System.Data.Entity.EntityState.Unchanged;
            //}

            //if (link.NativeId != contractorInfo.NativeId)
            //    link.NativeId = contractorInfo.NativeId;

            //if (link.NodeId != node.Id)
            //    link.Node = node;

            //if (link.ContractorId != contractor.Id || contractor.Id == Guid.Empty)
            //    link.Contractor = contractor;

            //if (link.User != contractorInfo.User)
            //    link.User = contractorInfo.User;

            //link.Date = DateTime.Now;
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

            return new ContractorInfo();
        }

        public ContractorInfo GetByNativeId(string nativeId, string alias)
        {
            var q = db.Nodes.Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Where(c => c.NativeId == nativeId).Join(
                db.Contractors,
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
                    CountryCode = c.CountryCode,
                    TypeOfCounterparty = c.TypeOfCounterparty
                });

            return q.FirstOrDefault();

        }

        public IEnumerable<ContractorInfo> GetAllByNodeAlias(string alias)
        {
            var q = db.Nodes.Where(c => c.Alias == alias).FirstOrDefault()
                .Links.Join(
                db.Contractors,
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
                    CountryCode = c.CountryCode,
                    TypeOfCounterparty = c.TypeOfCounterparty
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
