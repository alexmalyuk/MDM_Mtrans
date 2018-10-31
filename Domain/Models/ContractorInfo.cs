using Data.Models;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Domain.Models
{
    public class ContractorInfo : Contractor
    {
        public String NodeAlias { get; set; }
        public String NativeId { get; set; }
        public string User { get; set; }


        public void Save()
        {

            ///TODO: При записи ContractorInfo разделить логику если это новый и если уже существующий
            ///
            /// сначала проверить таблицу Links
            /// новый - таблица Links не содержит записи (NativeId + NodeAlias) - тогда проверить уникальность ИНН
            /// существующий - запись есть найти по Link.Id и перезаписать поля
            /// 

            //using (UnitOfWork unitOfWork = new UnitOfWork())
            //{
            //    //Node node = db.Nodes.Where(a => a.Alias == NodeAlias).FirstOrDefault();
            //    Node node =
            //    if (node == null)
            //        return;

            //    // check the Contractor
            //    Contractor contractor = db.Contractors.Where(a => a.INN == INN).FirstOrDefault();
            //    if (contractor == null)
            //    {
            //        contractor = new Contractor();
            //        db.Contractors.Add(contractor);
            //    }
            //    else
            //    {
            //        //db.Entry(contractor).State = EntityState.Modified;
            //        db.Contractors.Attach(contractor);
            //    }
                
            //    ///TODO: При записи Contractor перезаписывать только поля, которые изменились
            //    ///

            //    contractor.FullName = this.FullName;
            //    contractor.INN = this.INN;
            //    contractor.LegalAddress = this.LegalAddress;
            //    contractor.Name = this.Name;
            //    contractor.OKPO = this.OKPO;
            //    contractor.VATNumber = this.VATNumber;

            //    db.SaveChanges();

            //    // check the Link
            //    if (!db.Links.Where(a => a.NativeId == NativeId && a.NodeId == node.Id).Any())
            //    {
            //        Link link = new Link();
            //        link.NodeId = node.Id;
            //        link.NativeId = this.NativeId;
            //        link.Contractor = contractor;
            //        link.Date = DateTime.Now;
            //        link.User = this.User;
            //        db.Links.Add(link);
            //    }

            //    db.SaveChanges();
            //}
        }

        public void Validate()
        {

            StringBuilder sResult = new StringBuilder();

            if (!ContractorValidator.ValidateINN(INN))
            {
                sResult.AppendFormat("- Некорректный код ИНН [{0}]", INN).AppendLine();
            }
            if (!ContractorValidator.ValidateOKPO(OKPO))
            {
                sResult.AppendFormat("- Некорректный код ОКПО [{0}]", OKPO).AppendLine();
            }
            if (!ContractorValidator.ValidateVATNumber(VATNumber))
            {
                sResult.AppendFormat("- Некорректный номер свидетельства [{0}]", VATNumber).AppendLine();
            }

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