using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class HistoryViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateUTC { get; set; }
        public string User { get; set; }
        public string Node { get; set; }

        public DateTime Date => DateUTC.ToLocalTime();
    }

    public class HistoryListViewModel : List<HistoryViewModel>
    {
    }
}
