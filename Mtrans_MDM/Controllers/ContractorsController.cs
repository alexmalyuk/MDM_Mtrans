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

namespace Mtrans_MDM.Controllers
{
    [Authorize]
    public class ContractorsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Contractors
        public ActionResult Index()
        {
            return View(unitOfWork.Contractors.GetAll().ToList());
        }

        // GET: Contractors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = unitOfWork.Contractors.Get((Guid)id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
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
        public ActionResult Create([Bind(Include = "Id,Name,FullName,INN,OKPO,VATNumber,CountryOfRegistration,TypeOfCounterparty")] Contractor contractor)
        {
            ContractorValidator validator = new ContractorValidator(contractor);

            if (!validator.ValidateINN())
                ModelState.AddModelError("INN", "Некорректный код ИНН");
            if (!validator.ValidateOKPO())
                ModelState.AddModelError("OKPO", "Некорректный код ОКПО");
            if (!validator.ValidateVATNumber())
                ModelState.AddModelError("VATNumber", "Некорректный код плательщика НДС");

            if (ModelState.IsValid)
            {
                unitOfWork.Contractors.Create(contractor);
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
            Contractor contractor = unitOfWork.Contractors.Get((Guid)id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,FullName,INN,OKPO,VATNumber,CountryOfRegistration,TypeOfCounterparty")] Contractor contractor)
        {
            ContractorValidator validator = new ContractorValidator(contractor);

            if (!validator.ValidateINN())
                ModelState.AddModelError("INN", "Некорректный код ИНН");
            if (!validator.ValidateOKPO())
                ModelState.AddModelError("OKPO", "Некорректный код ОКПО");
            if (!validator.ValidateVATNumber())
                ModelState.AddModelError("VATNumber", "Некорректный код плательщика НДС");

            if (ModelState.IsValid)
            {
                unitOfWork.SetEntityStateAsModified(contractor);
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
            Contractor contractor = unitOfWork.Contractors.Get((Guid)id);
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
            unitOfWork.Contractors.Delete(id);
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
