using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Domain;
using Data.Models.Core;

namespace Mtrans_MDM.Controllers
{
    public class NodesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Nodes
        public ActionResult Index()
        {
            return View(unitOfWork.Nodes.GetAll().ToList());
        }

        // GET: Nodes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Node node = unitOfWork.Nodes.Get((Guid)id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // GET: Nodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nodes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Alias")] Node node)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Nodes.Create(node);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(node);
        }

        // GET: Nodes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = unitOfWork.Nodes.Get((Guid)id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Alias")] Node node)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Nodes.Update(node);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(node);
        }

        // GET: Nodes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = unitOfWork.Nodes.Get((Guid)id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            unitOfWork.Nodes.Delete(id);
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
