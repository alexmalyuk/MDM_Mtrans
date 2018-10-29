using Data.Models;
using Mtrans_MDM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Mtrans_MDM.Models
{
    public class ContractorInfo : Contractor
    {
        public String NodeAlias { get; set; }
        public String NativeId { get; set; }
        public string User { get; set; }


        //private Regex rexex = new Regex()
        public void Save()
        {

            ///TODO: При записи ContractorInfo разделить логику если это новый и если уже существующий
            ///
            /// сначала проверить таблицу Links
            /// новый - таблица Links не содержит записи (NativeId + NodeAlias) - тогда проверить уникальность ИНН
            /// существующий - запись есть найти по Link.Id и перезаписать поля
            /// 

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
                
                ///TODO: При записи Contractor перезаписывать только поля, которые изменились
                ///

                contractor.FullName = this.FullName;
                contractor.INN = this.INN;
                contractor.LegalAddress = this.LegalAddress;
                contractor.Name = this.Name;
                contractor.OKPO = this.OKPO;
                contractor.VATNumber = this.VATNumber;

                db.SaveChanges();

                // check the Link
                if (!db.Links.Where(a => a.NativeId == NativeId && a.NodeId == node.Id).Any())
                {
                    Link link = new Link();
                    link.NodeId = node.Id;
                    link.NativeId = this.NativeId;
                    link.Contractor = contractor;
                    link.Date = DateTime.Now;
                    link.User = this.User;
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
                        VATNumber = c.VATNumber,
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
                        VATNumber = c.VATNumber,
                        LegalAddress = c.LegalAddress,
                        Id = c.Id
                    });

                return q.ToList();
            }
        }

        public void Validate()
        {

            string INN_IsMatchPattern = @"^(\d{10})$|^(\d{12})$";
            string OKPO_IsMatchPattern = @"^(\d{8})$|^(\d{10})$";
            string VATNumber_IsMatchPattern = @"^(\d{12})$";

            StringBuilder sResult = new StringBuilder();

            if (!Regex.IsMatch(INN, INN_IsMatchPattern) || ContractorChecksumValidator.ValidateINN(INN))
            {
                sResult.AppendFormat("- Некорректный код ИНН [{0}]", INN).AppendLine();
            }
            if (Regex.IsMatch(OKPO, OKPO_IsMatchPattern))
            {
                sResult.AppendFormat("- Некорректный код ОКПО [{0}]", OKPO).AppendLine();
            }
            if (Regex.IsMatch(VATNumber, VATNumber_IsMatchPattern))
            {
                sResult.AppendFormat("- Некорректный номер свидетельства [{0}]", VATNumber).AppendLine();
            }

            ///TODO: ContractorInfo сделать проверку контрольных сумм ИНН, ОКПО, НомерСвидетельства
            ///

            isValid = (sResult.Length == 0);
            validationResult = sResult.ToString();
        }

        private bool isValid = false;

        public bool IsValid
        {
            get { return isValid; }
        }

        private string validationResult;

        public string ValidationResult
        {
            get { return validationResult; }
        }

    }
}