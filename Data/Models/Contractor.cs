using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Models
{
    [DataContract(Name = "ContractorInfo", Namespace = "http://www.metrans.com.ua")]
    public class Contractor
    {
        [IgnoreDataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

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
        [Display(Name = "Код страны")]
        public int CountryCode { get; set; }

        [IgnoreDataMember]
        public CountryEnum Country
        {
            get { return (CountryEnum)CountryCode; }
        }

        [DataMember]
        [Display(Name = "Тип контрагента")]
        public TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }
    }
}
