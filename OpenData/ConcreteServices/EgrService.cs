using OpenData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenData.ConcreteServices
{
    public class EgrService : OpenDataService
    {
        private class CompanyModel
        {
            public string id { get; set; }
            public string edrpou { get; set; }
            public string officialName { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string mainPerson { get; set; }
            public string occupation { get; set; }
            public string status { get; set; }
        }

        private ICompanyOpenDataModel MapFromApiModel(CompanyModel apiDataModel)
        {
            if (apiDataModel != null)
            {
                ICompanyOpenDataModel companyOpenDataModel = new CompanyOpenDataModel();
                companyOpenDataModel.shortName = apiDataModel.name;
                companyOpenDataModel.name = apiDataModel.officialName;
                companyOpenDataModel.edrpou = apiDataModel.edrpou;
                //companyOpenDataModel.inn = 
                companyOpenDataModel.boss = apiDataModel.mainPerson;
                companyOpenDataModel.kved = apiDataModel.occupation;
                companyOpenDataModel.location = apiDataModel.address;
                //companyOpenDataModel.state = 
                //companyOpenDataModel.registrationDate =
                return companyOpenDataModel;
            }
            else
            {
                return null;
            }
        }

        private async Task<CompanyModel> GetDataByOKPO(string okpo)
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync(new Uri("http://edr.data-gov-ua.org/api/companies?where={\"edrpou\":{\"contains\":\"" + okpo + "\"}}"));
                result.EnsureSuccessStatusCode();
                var users = await result.Content.ReadAsAsync<IEnumerable<CompanyModel>>();

                return users.Where(c => c.edrpou == okpo).FirstOrDefault();
            }
        }

        public override ICompanyOpenDataModel GetCompanyDataByCode(string okpo)
        {
            CompanyModel companyData = GetDataByOKPO(okpo).GetAwaiter().GetResult();

            return MapFromApiModel(companyData);
        }

        public override async Task<ICompanyOpenDataModel> GetCompanyDataByCodeAsync(string okpo)
        {
            CompanyModel companyData = await GetDataByOKPO(okpo);

            return MapFromApiModel(companyData);
        }
    }
}
