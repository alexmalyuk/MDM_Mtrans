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
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string INN { get; set; }
        [DataMember]
        public string OKPO { get; set; }
        [DataMember]
        public string VATNumber { get; set; }
        [DataMember]
        public CountryEnum CountryOfRegistration { get; set; }
        [DataMember]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
        [DataMember]
        public ContractorAddress Address { get; set; }
    }
}
