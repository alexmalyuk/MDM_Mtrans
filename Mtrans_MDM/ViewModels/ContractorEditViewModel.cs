using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data;
using Domain.Validators;

namespace Mtrans_MDM.ViewModels
{
    public class ContractorEditViewModel : IContractorData, IContractorAddress, IValidatableObject
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public CountryEnum? CountryOfRegistration { get; set; }
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
        public string INN { get; set; }
        public string OKPO { get; set; }
        public string VATNumber { get; set; }

        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string StringRepresentedAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ContractorValidator validator = new ContractorValidator(this);
            return validator.Validate(validationContext);
        }
    }
}