using Mtrans_MDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mtrans_MDM.API_Controllers
{
    public class ContractorInfoController : ApiController
    {

        [HttpPost]
        [Route("api/ContractorInfo/{NodeAlias}")]
        public bool Post(string NodeAlias, [FromBody]ContractorInfo contractorInfo)
        {

            //try
            //{
            contractorInfo.NodeAlias = NodeAlias;
            contractorInfo.Save();
            //}
            //catch (Exception ex)
            //{
            //    Log.For(this).Error("POST: api/ContractorInfo/" + NodeAlias, ex);
            //}

            return true;
        }
    }
}
