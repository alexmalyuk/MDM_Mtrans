using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData.ConcreteServices
{
    public class VkursiService : OpenDataService
    {
        public VkursiService()
        {

        }
        public override ICompanyOpenDataModel GetCompanyDataByCode(string okpo)
        {
            throw new NotImplementedException();
        }

        public override async Task<ICompanyOpenDataModel> GetCompanyDataByCodeAsync(string okpo)
        {
            throw new NotImplementedException();
        }
    }
}
