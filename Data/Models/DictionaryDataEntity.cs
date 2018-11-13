using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Models
{
    [DataContract(Namespace = "http://www.metrans.com.ua")]
    public class DictionaryDataEntity
    {
        [IgnoreDataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Display(Name = "Наименование")]
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? string.Format("{0} (Id - {1})", GetType().Name,Id.ToString()) : Name;
        }
    }
}
