using OpenData;
using OpenDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Mtrans_MDM.Controllers.API
{
    public class OpenDataController : ApiController
    {
        // GET: api/OpenData/50122545
        [HttpGet]
        [Route("api/OpenData/{okpo}")]
        [ResponseType(typeof(IContractorODPModel))]
        public IContractorODPModel Get(string okpo)
        {
            using (ODPService odp = new ODPService())
            {
                return odp.GetContractorData(okpo);
            }
        }

    }
}
