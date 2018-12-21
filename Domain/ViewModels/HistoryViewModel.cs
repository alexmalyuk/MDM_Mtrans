using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class HistoryViewModel
    {
        public Guid SubjectId { get; set; }
        public Guid Id { get; set; }
        [Display(Name = "Дата (UTC)")]
        public DateTime DateUTC { get; set; }
        [Display(Name = "Пользователь")]
        public string User { get; set; }
        [Display(Name = "Узел")]
        public string Node { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date => DateUTC.ToLocalTime();
    }

}
