using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mtrans_MDM.Models
{
    public class ContractorInfo : Contractor
    {
        public String NodeAlias { get; set; }

        public String NativeId { get; set; }

        public void Save()
        {

            // примерный алгоритм при сохранении контрагента
            //
            //- найти контрагента по ИНН
            //- если не нашли - создать
            //- дописать запись в Link

            using (DataContext db = new DataContext())
            {
                Node node = db.Nodes.Where(a => a.Alias == NodeAlias).FirstOrDefault();
                if (node == null)
                    return;

                // check the Contractor
                Contractor contractor = db.Contractors.Where(a => a.INN == INN).FirstOrDefault();
                if (contractor == null)
                {
                    contractor = new Contractor();
                    db.Contractors.Add(contractor);
                }
                else
                {
                    //db.Entry(contractor).State = EntityState.Modified;
                    db.Contractors.Attach(contractor);
                }
                contractor.FullName = this.FullName;
                contractor.INN = this.INN;
                contractor.LegalAddress = this.LegalAddress;
                contractor.Name = this.Name;
                contractor.OKPO = this.OKPO;
                contractor.VATCertificateNumber = this.VATCertificateNumber;

                db.SaveChanges();

                // check the Link
                if (!db.Links.Where(a => a.NativeId == NativeId && a.NodeId == node.Id).Any())
                {
                    Link link = new Link();
                    link.NodeId = node.Id;
                    link.NativeId = this.NativeId;
                    link.Contractor = contractor;
                    link.Date = DateTime.Now;
                    db.Links.Add(link);
                }

                db.SaveChanges();
            }
        }

        public static ContractorInfo GetByNodeAliasAndNativeId(string NodeAlias, string NativeId)
        {
            
            using(DataContext db = new DataContext())
            {
                Node node = db.Nodes.Where(a => a.Alias == NodeAlias).FirstOrDefault();
                if (node == null)
                    return null;

                var q = db.Links.Where(a => (a.NativeId == NativeId || NativeId == null) && a.NodeId == node.Id).Join(
                    db.Contractors,
                    l => l.ContractorId,
                    c => c.Id,
                    (l, c) => new ContractorInfo
                    {
                        NativeId = l.NativeId.ToString(),
                        NodeAlias = NodeAlias,
                        Name = c.Name,
                        FullName = c.FullName,
                        INN = c.INN,
                        OKPO = c.OKPO,
                        VATCertificateNumber = c.VATCertificateNumber,
                        LegalAddress = c.LegalAddress,
                        Id = c.Id
                    });

                return q.FirstOrDefault();
            }
        }

        public static List<ContractorInfo> GetContratorInfosByNodeAlias(string NodeAlias)
        {
            using (DataContext db = new DataContext())
            {
                Node node = db.Nodes.Where(a => a.Alias == NodeAlias).FirstOrDefault();
                if (node == null)
                    return null;

                var q = db.Links.Where(a => a.NodeId == node.Id).Join(
                    db.Contractors,
                    l => l.ContractorId,
                    c => c.Id,
                    (l, c) => new ContractorInfo
                    {
                        NativeId = l.NativeId.ToString(),
                        NodeAlias = NodeAlias,
                        Name = c.Name,
                        FullName = c.FullName,
                        INN = c.INN,
                        OKPO = c.OKPO,
                        VATCertificateNumber = c.VATCertificateNumber,
                        LegalAddress = c.LegalAddress,
                        Id = c.Id
                    });

                return q.ToList();
            }
        }
    }
}