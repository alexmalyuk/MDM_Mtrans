using Data;
using Data.Models;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Domain.Models
{
    [DataContract(Name = "ContractorInfo", Namespace = Const.DataContractNameSpace)]
    public class ContractorInfo : BaseModel, IContractorData, IContractorAddress
    {
        [Display(Name = "Полное наименование")]
        public string FullName { get; set; }

        [DataMember]
        [Display(Name = "Код ИНН")]
        public string INN { get; set; }

        [DataMember]
        [Display(Name = "Код ОКПО")]
        public string OKPO { get; set; }

        [DataMember]
        [Display(Name = "Код плательщика НДС")]
        public string VATNumber { get; set; }

        [DataMember]
        [Display(Name = "Юридический адрес")]
        public string LegalAddress { get; set; }

        //[DataMember]
        //[Display(Name = "Код страны")]
        //public int CountryCode { get; set; }

        [DataMember]
        [Display(Name = "Страна регистрации")]
        public CountryEnum? CountryOfRegistration { get; set; }
        //{
        //    get { return (CountryEnum)CountryCode; }
        //    set { CountryCode = (int)value; }
        //}

        [DataMember]
        [Display(Name = "Тип контрагента")]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }

        [DataMember]
        [Display(Name = "Индекс")]
        public string PostalCode { get; set; }

        [DataMember]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [DataMember]
        [Display(Name = "Область")]
        public string Region { get; set; }

        [DataMember]
        [Display(Name = "Район")]
        public string District { get; set; }

        [DataMember]
        [Display(Name = "Город")]
        public string City { get; set; }

        [DataMember]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [DataMember]
        [Display(Name = "Дом")]
        public string House { get; set; }

        [DataMember]
        [Display(Name = "Офис")]
        public string Flat { get; set; }


        [DataMember]
        [Display(Name = "Адрес одной строкой (устар.)")]
        public string StringRepresentedAddress { get; set; }


        public void Save()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.ContractorInfos.CreateOrUpdate(this);
                unitOfWork.Save();
            }
        }

        public override void Validate()
        {
            ContractorValidator validator = new ContractorValidator(this as IContractorData);
            StringBuilder sResult = new StringBuilder();

            if (!validator.ValidateINN())
                sResult.AppendFormat("- Некорректный код ИНН [{0}]", INN).AppendLine();
            if (!validator.ValidateOKPO())
                sResult.AppendFormat("- Некорректный код ОКПО [{0}]", OKPO).AppendLine();
            if (!validator.ValidateVATNumber())
                sResult.AppendFormat("- Некорректный номер свидетельства [{0}]", VATNumber).AppendLine();

            isValid = (sResult.Length == 0);
            validationResult = sResult.ToString();
        }

    }
}