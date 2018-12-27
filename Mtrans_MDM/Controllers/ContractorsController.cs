using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Domain.Validators;
using Domain;
using Domain.ViewModels;
using Microsoft.AspNet.Identity;
using Data.Models.Core;
using Domain.Core;

namespace Mtrans_MDM.Controllers
{
    [Authorize]
    public class ContractorsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Contractors
        public ActionResult Index()
        {
            return View(unitOfWork.ContractorViewModel.GetAll().ToList());
        }

        // GET: Contractors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorViewModel contractor = unitOfWork.ContractorViewModel.Get((Guid)id);
            if (contractor == null)
            {
                return HttpNotFound();
            }

            ViewResult view = View(contractor);
            view.ViewBag.HistoryList = unitOfWork.HistoryViewModel.GetAllBySubject((Guid)id);
            return view;
        }

        // GET: Contractors/Snapshot/5
        public ActionResult Snapshot(Guid? snapshotId)
        {
            if (snapshotId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            unitOfWork.HistoryViewModel.Get((Guid)snapshotId);

            Subject subjectSnapshot = unitOfWork.HistoryViewModel.GetSubjectSnapshot((Guid)snapshotId);
            Contractor contractor = subjectSnapshot as Contractor;

            if (contractor == null)
                return HttpNotFound();

            ContractorViewModel contractorVM;
            ModelConvertor.ContractorToContractorViewModel(contractor, out contractorVM);
            ViewResult view = View(contractorVM);



            view.ViewBag.HistoryList = unitOfWork.HistoryViewModel.GetAllBySubject(contractor.Id);
            //view.ViewBag.CurrentSnapshot = ;



            return view;
        }

        // GET: Contractors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FullName,INN,OKPO,VATNumber,CountryOfRegistration,TypeOfCounterparty,Street,House,Flat,District,Region,City,Country,PostalCode,StringRepresentedAddress,IsBranch,HeadContractor,BranchCode")] ContractorViewModel contractor)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ContractorViewModel.AddOrUpdate(contractor, User.Identity.GetUserName());
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorViewModel contractor = unitOfWork.ContractorViewModel.Get((Guid)id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FullName,INN,OKPO,VATNumber,CountryOfRegistration,TypeOfCounterparty,Street,House,Flat,District,Region,City,Country,PostalCode,StringRepresentedAddress,IsBranch,HeadContractor,BranchCode")] ContractorViewModel contractor)
        {
            if (ModelState.IsValid)
            {

                unitOfWork.ContractorViewModel.AddOrUpdate(contractor, User.Identity.GetUserName());
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorViewModel contractor = unitOfWork.ContractorViewModel.Get((Guid)id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            unitOfWork.ContractorViewModel.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
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
