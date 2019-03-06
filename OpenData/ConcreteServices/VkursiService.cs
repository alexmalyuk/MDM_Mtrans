using OpenData.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkursiAPI.Models;
using VkursiAPI.Services;

namespace OpenData.ConcreteServices
{
    public class VkursiService : OpenDataService
    {
        private APIService _api;
        private bool _authentificated = false;

        public VkursiService()
        {

            string email, password;

            try
            {
                var Section = ConfigurationManager.GetSection("openData");
                NameValueCollection settings = Section as NameValueCollection;
                email = settings["vkursi_email"];
                password = settings["vkursi_password"];
            }
            catch (Exception ex)
            {
                throw new OpenDataException("Unable get settings. Check the Web.config file", ex);
            }

            _api = APIService.getInstance();
            _authentificated = _api.Authentificate(email, password);

            if (!_authentificated)
            {
                throw new OpenDataException(string.Format("Authentification failed for {0}", email));
            }
        }

        private ICompanyOpenDataModel MapFromApiModel(CompanyModel apiDataModel)
        {
            if (apiDataModel != null)
            {
                ICompanyOpenDataModel companyOpenDataModel = new CompanyOpenDataModel();
                companyOpenDataModel.shortName = apiDataModel.shortName;
                companyOpenDataModel.name = apiDataModel.name;
                companyOpenDataModel.edrpou = apiDataModel.edrpou;
                companyOpenDataModel.inn = apiDataModel.inn;
                companyOpenDataModel.boss = apiDataModel.boss;
                companyOpenDataModel.kved = apiDataModel.kved;
                companyOpenDataModel.location = apiDataModel.location;
                companyOpenDataModel.state = apiDataModel.state;
                companyOpenDataModel.registrationDate = apiDataModel.DateRegisterInn;
                return companyOpenDataModel;
            }
            else
            {
                return null;
            }
        }

        private async Task<CompanyModel> GetDataByOKPO(string okpo)
        {
            CompanyModel[] result = (CompanyModel[]) await _api.GetData(new string[] { okpo }, VkursiAPI.Enums.APIType.GetOrganizationInfo);

            return result.FirstOrDefault();
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
