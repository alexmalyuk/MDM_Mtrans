using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData
{
    public interface ICompanyOpenDataModel
    {
        string shortName { get; set; }
        string name { get; set; }
        string edrpou { get; set; }
        string inn { get; set; }
        string boss { get; set; }
        string kved { get; set; }
        string location { get; set; }
        string state { get; set; }
        DateTime? registrationDate { get; set; }
    }
}
