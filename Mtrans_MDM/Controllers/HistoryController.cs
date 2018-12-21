using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mtrans_MDM.Controllers
{
    public class HistoryController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: History/5
        [HttpGet]
        [Route("History/Index/{Id}")]
        public ActionResult Index(Guid? Id)
        {
            if(Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(unitOfWork.HistoryViewModel.GetAllBySubject((Guid)Id).ToList());
        }
    }
}