using Data.Models;
using Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Models
{
    
    public class Contractor : Subject, IContractorData
    {
        [Display(Name = "Полное наименование")]
        public string FullName { get; set; }

        [Display(Name = "Код ИНН")]
        public string INN { get; set; }

        [Display(Name = "Код ОКПО")]
        public string OKPO { get; set; }

        [Display(Name = "Код плательщика НДС")]
        public string VATNumber { get; set; }

        [Display(Name = "Страна регистрации")]
        public CountryEnum? CountryOfRegistration { get; set; }

        [Display(Name = "Тип контрагента")]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }

        [Display(Name = "Юридический адрес")]
        public ContractorAddress Address { get; set; }
    }
}
