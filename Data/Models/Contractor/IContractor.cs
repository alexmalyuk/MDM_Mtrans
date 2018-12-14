using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public interface IContractor
    {
        string Name { get; set; }
        string FullName { get; set; }
        string INN { get; set; }
        string OKPO { get; set; }
        string VATNumber { get; set; }
        CountryEnum CountryOfRegistration { get; set; }
        TypeOfCounterpartyEnum TypeOfCounterparty { get; set; }

    }
}
