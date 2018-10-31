using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Link
    {
        public Guid Id { get; set; }

        [Display(Name = "Дата добавления/изменения")]
        public DateTime Date { get; set; }

        [Display(Name = "Контрагент")]
        public Guid ContractorId { get; set; }

        [Display(Name = "Узел")]
        public Guid NodeId { get; set; }

        [Display(Name = "Id в своем узле")]
        public string NativeId { get; set; }

        [Display(Name = "Пользователь")]
        public string User { get; set; }


        public virtual Node Node { get; set; }
        public virtual Contractor Contractor { get; set; }

    }
}
