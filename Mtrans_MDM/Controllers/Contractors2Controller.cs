using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mtrans_MDM.Controllers
{
    public class Contractors2Controller : Controller
    {
        // GET: Contractor2
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contractor2/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contractor2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contractor2/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contractor2/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contractor2/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contractor2/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contractor2/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
