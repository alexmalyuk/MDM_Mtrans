using Data.Models.Core;
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

        [HttpGet]
        [Route("History/Index/{Id}")]
        public ActionResult Index(Guid? Id, Guid? versionId = null)
        {
            if(Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewResult view = View(unitOfWork.HistoryViewModel.GetAllBySubject((Guid)Id).ToList());

            view.ViewBag.SubjectSnapshot = 
                versionId != null
                ? unitOfWork.HistoryViewModel.GetSubjectSnapshot((Guid)versionId)
                : null;

            return view;
        }
    }
}