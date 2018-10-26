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
        private DataContext db = new DataContext();

        [HttpPost]
        [Route("api/ContractorInfo/{NodeAlias}")]
        [ResponseType(typeof(ContractorInfo))]
        public IHttpActionResult PostNode(string NodeAlias, [FromBody]ContractorInfo contractorInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contractorInfo.NodeAlias = NodeAlias;
            contractorInfo.Save();

            //Ответ 422 (Unprocessable Entity) означает, что сервер понимает указанный вид данных, 
            //и синтаксис запроса корректен (поэтому статус 400 (Bad Request) не подходит), 
            //но содержащиеся в запросе инструкции нельзя выполнитью. Например, эта ошибка может возникнуть, 
            //если тело запроса было синтаксически правильным, но содержало семантическую ошибку.	

            return Ok(contractorInfo);
            
            //return CreatedAtRoute("DefaultApi", contractorInfo, node);
        }
        //[HttpPost]
        //[Route("api/ContractorInfo/{NodeAlias}")]
        //public bool Post(string NodeAlias, [FromBody]ContractorInfo contractorInfo)
        //{

        //    //try
        //    //{
        //    contractorInfo.NodeAlias = NodeAlias;
        //    contractorInfo.Save();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.For(this).Error("POST: api/ContractorInfo/" + NodeAlias, ex);
        //    //}

        //    return true;
        //}

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
