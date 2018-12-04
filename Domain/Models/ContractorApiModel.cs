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
    public class ContractorApiModel : BaseApiModel, IContractorData, IContractorAddress, IValidatableObject
    {
        [DataMember]
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

        [DataMember]
        [Display(Name = "Страна регистрации")]
        public CountryEnum? CountryOfRegistration { get; set; }

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
                unitOfWork.ContractorApiModel.AddOrUpdate(this);
                unitOfWork.Save();
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ContractorValidator validator = new ContractorValidator(this as IContractorData);
            return validator.Validate(validationContext);
        }
    }
}