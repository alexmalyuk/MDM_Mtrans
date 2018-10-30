using Data.Models;
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
            //db.Links.Add(item);
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ContractorInfo Get(Guid id)
        {
            var q = db.Links.Where(a => a.Id == id).Join(
                db.Contractors,
                l => l.ContractorId,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
                    Id = c.Id
                });
            
            return q.FirstOrDefault();
        }

        public ContractorInfo GetByNativeId(string nativeId, string alias)
        {

            var q = db.Nodes.Where(a => a.Alias == alias).Join(
                db.Links.Where(a => a.NativeId == nativeId),
                n => n.Id,
                l => l.NodeId,
                (n, l) => new
                {
                    NativeId = l.NativeId,
                    ContractorId = l.ContractorId,
                    NodeAlias = n.Alias
                }).Join(
                db.Contractors,
                l => l.ContractorId,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.NodeAlias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
                    Id = c.Id
                });

            return q.FirstOrDefault();
        }

        public IQueryable<ContractorInfo> GetAllByNodeAlias(string alias)
        {

            var q = db.Nodes.Where(a => a.Alias == alias).Join(
                db.Links,
                n => n.Id,
                l => l.NodeId,
                (n, l) => new
                {
                    NativeId = l.NativeId,
                    ContractorId = l.ContractorId,
                    NodeAlias = n.Alias
                }).Join(
                db.Contractors,
                l => l.ContractorId,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    NodeAlias = l.NodeAlias,
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
                    Id = c.Id
                });

            return q;
        }

        public IQueryable<ContractorInfo> GetAll()
        {
            var q = db.Links.Join(
                db.Contractors,
                l => l.ContractorId,
                c => c.Id,
                (l, c) => new ContractorInfo
                {
                    NativeId = l.NativeId.ToString(),
                    Name = c.Name,
                    FullName = c.FullName,
                    INN = c.INN,
                    OKPO = c.OKPO,
                    VATNumber = c.VATNumber,
                    LegalAddress = c.LegalAddress,
                    Id = c.Id
                });
            return q;
        }

        public void Update(ContractorInfo item)
        {
            //db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            throw new NotImplementedException();
        }
    }
}
