using OpenData.ConcreteServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData
{
    public class OpenDataClient : IDisposable
    {
        private OpenDataService openDataService;

        public OpenDataClient()
        {
            openDataService = CreateOpenDataService();
        }

        private OpenDataService CreateOpenDataService()
        {
            string providerName = string.Empty;

            try
            {
                var openDataSection = ConfigurationManager.GetSection("openData");
                NameValueCollection settings = openDataSection as NameValueCollection;
                providerName = settings["providerName"];
            }
            catch (Exception ex)
            {
                throw new OpenDataProviderException("Unable to get settings. \nCheck the Web.config file", ex);
            }

            switch (providerName)
            {
                case "egr":
                    return new EgrService();
                case "vkursi":
                    return new VkursiService();
                default:
                    throw new OpenDataProviderException(String.Format("Unknown provider \"{0}\" specified. \nCheck the Web.config file", providerName));
            }
        }

        public void Dispose()
        {
            openDataService = null;
        }

        public ICompanyOpenDataModel GetCompanyDataByCode(string okpo)
        {
            return openDataService.GetCompanyDataByCode(okpo);
        }

        public async Task<ICompanyOpenDataModel> GetCompanyDataByCodeAsync(string okpo)
        {
            return await openDataService.GetCompanyDataByCodeAsync(okpo);
        }
    }
}
