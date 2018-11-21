using Data.Models.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Models
{
    
    public class Contractor : Subject
    {
        [Display(Name = "Полное наименование")]
        public string FullName { get; set; }

        [Display(Name = "Код ИНН")]
        public string INN { get; set; }

        [Display(Name = "Код ОКПО")]
        public string OKPO { get; set; }

        [Display(Name = "Код плательщика НДС")]
        public string VATNumber { get; set; }

        [Display(Name = "Юридический адрес")]
        public string LegalAddress { get; set; }

        [Display(Name = "Код страны")]
        public int CountryCode { get; set; }

        public CountryEnum Country
        {
            get { return (CountryEnum)CountryCode; }
            set { CountryCode = (int)value; }
        }

        [Display(Name = "Тип контрагента")]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
    }
}
