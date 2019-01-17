using OpenDataProvider;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkursiAPI.Models;
using VkursiAPI.Services;

namespace OpenData
{

    public class ODPService : IODPService, IDisposable
    {
        private APIService _api;
        private bool _authentificated = false;

        public ODPService()
        {
            string email, password;

            try
            {
                var Section = ConfigurationManager.GetSection("vkursiPro");
                NameValueCollection settings = Section as NameValueCollection;
                email = settings["email"];
                password = settings["password"];
            }
            catch (Exception ex)
            {
                throw new OpenDataProviderException("Unable get settings. Check the Web.config file", ex);
            }

            _api = APIService.getInstance();
            _authentificated = _api.Authentificate(email, password);

            if (!_authentificated)
            {
                throw new OpenDataProviderException(String.Format("Authentification failed for {0}", email));
            }
        }

        public void Dispose()
        {
            _api = null;
        }

        public IContractorODPModel GetContractorData(string okpo)
        {
            IContractorODPModel contractorODPModel = new ContractorODPModel();
            CompanyModel[] result = (CompanyModel[])_api.GetData(new string[] { okpo }, VkursiAPI.Enums.APIType.GetOrganizationInfo);
            if (result.Count() > 0)
            {
                CompanyModel companyModel = result[0];
                contractorODPModel.name = companyModel.name;
                contractorODPModel.shortName = companyModel.shortName;
                contractorODPModel.edrpou = companyModel.edrpou;
                contractorODPModel.inn = companyModel.inn;
                contractorODPModel.boss = companyModel.boss;
                contractorODPModel.kved = companyModel.kved;
                contractorODPModel.location = companyModel.location;
                contractorODPModel.state = companyModel.state;
                contractorODPModel.registrationDate = companyModel.DateRegisterInn;
            }

            return contractorODPModel;
        }

    }
}
