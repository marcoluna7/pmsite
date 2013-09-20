using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace PmWebSite.Controllers
{
    public class EventosController : Controller
    {
        private PmSiteEntities1 db = new PmSiteEntities1();

        //
        // GET: /Eventos/

        public ActionResult Index()
        {
            return View(db.pm_events.ToList());
        }

        //
        // GET: /Eventos/Details/5

        public ActionResult Details(int id = 0)
        {
            pm_events pm_events = db.pm_events.Find(id);
            if (pm_events == null)
            {
                return HttpNotFound();
            }
            return View(pm_events);
        }

        //
        // GET: /Eventos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Eventos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pm_events pm_events)
        {
            if (ModelState.IsValid)
            {
                db.pm_events.Add(pm_events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pm_events);
        }

        //
        // GET: /Eventos/Edit/5

        public ActionResult Edit(int id = 0)
        {
            pm_events pm_events = db.pm_events.Find(id);
            if (pm_events == null)
            {
                return HttpNotFound();
            }
            return View(pm_events);
        }

        //
        // POST: /Eventos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pm_events pm_events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pm_events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pm_events);
        }

        //
        // GET: /Eventos/Delete/5

        public ActionResult Delete(int id = 0)
        {
            pm_events pm_events = db.pm_events.Find(id);
            if (pm_events == null)
            {
                return HttpNotFound();
            }
            return View(pm_events);
        }

        //
        // POST: /Eventos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pm_events pm_events = db.pm_events.Find(id);
            db.pm_events.Remove(pm_events);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}