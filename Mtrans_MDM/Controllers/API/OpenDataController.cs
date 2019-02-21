using OpenData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Mtrans_MDM.Controllers.API
{
    public class OpenDataController : ApiController
    {
        // GET: api/OpenData/50122545
        [HttpGet]
        [Route("api/OpenData/{okpo}")]
        [ResponseType(typeof(ICompanyOpenDataModel))]
        public async Task<ICompanyOpenDataModel> Get(string okpo)
        {
            using (OpenDataClient odClient = new OpenDataClient())
            {
                ICompanyOpenDataModel companyOpenDataModel = await odClient.GetCompanyDataByCodeAsync(okpo);
                return companyOpenDataModel;
            }
        }

    }
}
