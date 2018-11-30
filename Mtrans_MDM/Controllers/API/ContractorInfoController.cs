using Data.Models;
using Domain;
using Domain.Models;
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

namespace Mtrans_MDM.Controllers.API
{

    //[Authorize]
    public class ContractorInfoController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();

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

                    unitOfWork.ContractorInfos.CreateOrUpdate(contractorInfo);
                    unitOfWork.Save();

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
            //if (!User.IsInRole("api"))
            //    return Content(HttpStatusCode.Forbidden, "Unauthorized request");

            ContractorInfo contractorInfo = unitOfWork.ContractorInfos.GetByNativeId(NativeId, NodeAlias);
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
            ///TODO: добавить PostDate и ReadDate - подумать как их сочетать для того чтобы давать выборку данных всех контрагентов с момента последнего получения данных
            /// и надо ли это вообще ?
            /// 

            //return ContractorInfo.GetContratorInfosByNodeAlias(NodeAlias);
            return unitOfWork.ContractorInfos.GetAllByNodeAlias(NodeAlias).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
