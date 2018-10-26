using Data.Models;
using Mtrans_MDM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Mtrans_MDM.API_Controllers
{
    public class ContractorInfoController : ApiController
    {
        //private DataContext db = new DataContext();

        [HttpPost]
        [Route("api/ContractorInfo/{NodeAlias}")]
        [ResponseType(typeof(ContractorInfo))]
        public IHttpActionResult PostNode(string NodeAlias, [FromBody]ContractorInfo contractorInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                contractorInfo.Validate();

                if (contractorInfo.IsValid)
                {
                    contractorInfo.NodeAlias = NodeAlias;
                    contractorInfo.Save();
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, contractorInfo.ValidationResult);
                }
            }
            catch (Exception ex)
            {
                //Log.For(this).Error("POST: api/ContractorInfo/" + NodeAlias, ex);
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("api/ContractorInfo/{NodeAlias}/{NativeId}")]
        [ResponseType(typeof(ContractorInfo))]
        public IHttpActionResult Get(string NodeAlias, string NativeId)
        {
            ContractorInfo contractorInfo = ContractorInfo.GetByNodeAliasAndNativeId(NodeAlias, NativeId);
            if (contractorInfo == null)
            {
                return NotFound();
            }

            return Ok(contractorInfo);
        }

        [HttpGet]
        [Route("api/ContractorInfo/{NodeAlias}")]
        public List<ContractorInfo> Get(string NodeAlias)
        {
            ///TODO: добавить PostDate и ReadDate - подумать как их сочетать для того чтобы давать выборку данных всех контргаентов с момента последнего получения данных
            /// 

            return ContractorInfo.GetContratorInfosByNodeAlias(NodeAlias);
        }

        [HttpGet]
        [Route("api/ContractorInfo1/{NodeAlias}")]
        public JsonResult<List<ContractorInfo>> Get1(string NodeAlias)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            //serializerSettings.
            return Json(ContractorInfo.GetContratorInfosByNodeAlias(NodeAlias), serializerSettings, System.Text.Encoding.UTF8);
        }
    }
}
