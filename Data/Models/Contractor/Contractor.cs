using Data.Models;
using Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Models
{
    
    public class Contractor : Subject, IContractor
    {
        public string FullName { get; set; }
        public string INN { get; set; }
        public string OKPO { get; set; }
        public string VATNumber { get; set; }
        public CountryEnum CountryOfRegistration { get; set; }
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
        public ContractorAddress Address { get; set; }
    }
}
