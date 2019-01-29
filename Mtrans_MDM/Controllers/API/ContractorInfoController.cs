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
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace Mtrans_MDM.Controllers.API
{

    //[Authorize]
    public class ContractorInfoController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        [Route("api/ContractorInfo/{NodeAlias}")]
        [ResponseType(typeof(ModelStateDictionary))]
        public IHttpActionResult PostNode(string NodeAlias, [FromBody]ContractorApiModel contractorInfo)
        {
            try
            {
                contractorInfo.NodeAlias = NodeAlias;

                if (ModelState.IsValid)
                {
                    if ( !string.IsNullOrEmpty(contractorInfo.NativeId))
                    {
                        unitOfWork.ContractorApiModel.AddOrUpdate(contractorInfo);

                        try
                        {
                            unitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            ///TODO: parse exception details to determine duplication and other errors
                            ModelState.AddModelError("Model", "Попытка записать дублирующиеся данные. Контрагент с такими кодами уже существует.");
                            return BadRequest(ModelState);
                        }
                    }
                    return Ok();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/ContractorInfo/{NodeAlias}/{NativeId}")]
        [ResponseType(typeof(ContractorApiModel))]
        public IHttpActionResult Get(string NodeAlias, string NativeId)
        {
            //if (!User.IsInRole("api"))
            //    return Content(HttpStatusCode.Forbidden, "Unauthorized request");

            ContractorApiModel contractorInfo = unitOfWork.ContractorApiModel.GetByNativeId(NativeId, NodeAlias);
            if (contractorInfo == null)
            {
                return NotFound();
            }

            return Ok(contractorInfo);
        }

        [HttpGet]
        [Route("api/ContractorInfo/{NodeAlias}")]
        public List<ContractorApiModel> Get(string NodeAlias)
        {
            return unitOfWork.ContractorApiModel.GetAllByNodeAlias(NodeAlias).ToList();
        }

        [HttpPost]
        [Route("api/ContractorInfo/GetByCodes")]
        [ResponseType(typeof(ContractorApiModel))]
        public IHttpActionResult GetByCodes([FromBody]ContractorApiModel inputContractorInfo)
        {

            ContractorApiModel outputContractorInfo = unitOfWork.ContractorApiModel.GetByCodes(inputContractorInfo);
            if (outputContractorInfo == null)
            {
                return NotFound();
            }

            return Ok(outputContractorInfo);
        }

        [HttpPost]
        [Route("api/ContractorInfo/Validate")]
        [ResponseType(typeof(ModelStateDictionary))]
        public IHttpActionResult ValidateModel([FromBody]ContractorApiModel contractorInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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
