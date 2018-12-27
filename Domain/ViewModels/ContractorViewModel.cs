using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data;
using Domain.Validators;

namespace Domain.ViewModels
{
    public class ContractorViewModel : IContractor, IContractorAddress, IValidatableObject
    {
        public Guid Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Полное наименование")]
        public string FullName { get; set; }
        [Display(Name = "Страна регистрации")]
        [Required]
        public CountryEnum CountryOfRegistration { get; set; }
        [Display(Name = "Тип контрагента")]
        [Required]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
        [Display(Name = "Код ИНН")]
        public string INN { get; set; }
        [Display(Name = "Код ОКПО")]
        public string OKPO { get; set; }
        [Display(Name = "Код плат. НДС")]
        public string VATNumber { get; set; }

        [Display(Name = "Улица")]
        public string Street { get; set; }
        [Display(Name = "Дом")]
        public string House { get; set; }
        [Display(Name = "Офис")]
        public string Flat { get; set; }
        [Display(Name = "Город")]
        public string City { get; set; }
        [Display(Name = "Район")]
        public string District { get; set; }
        [Display(Name = "Область")]
        public string Region { get; set; }
        [Display(Name = "Индекс")]
        public string PostalCode { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Адрес одной строкой")]
        public string StringRepresentedAddress { get; set; }
        [Display(Name = "Является филиалом")]
        public bool IsBranch { get; set; }
        [Display(Name = "Головной контрагент")]
        public Contractor HeadContractor { get; set; }
        [Display(Name = "Код филиала")]
        public string BranchCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ContractorValidator validator = new ContractorValidator(this);
            return validator.Validate(validationContext);
        }
    }
}