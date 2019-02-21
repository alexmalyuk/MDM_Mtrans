using OpenData.ConcreteServices;
using System;
using System.Collections.Generic;
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
            // читает конфиг и создает конкретный экземпляр сервиса vkursi, egr и т.п.
            //return new VkursiService();
            return new EgrService();
            //return new OpenDataBotService();
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
