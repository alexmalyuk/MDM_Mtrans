using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData
{
    public abstract class OpenDataService
    {
        abstract public ICompanyOpenDataModel GetCompanyDataByCode(string okpo);
        abstract public Task<ICompanyOpenDataModel> GetCompanyDataByCodeAsync(string okpo);
    }
}
