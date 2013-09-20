using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer.DbLayer;

namespace WebSitePm.Controllers
{ 
    public class EventosController : Controller
    {
        private pmsiteEntities db = new pmsiteEntities();

        //
        // GET: /Eventos/

        public ViewResult Index(string nombre, DateTime? fi, DateTime? ff)
        {
            var consulta = from e in db.pm_events
                           where (string.IsNullOrEmpty(nombre) || e.name.Contains(nombre)) &&
                           (fi == null || e.eventDate >= fi && e.eventDate <= ff)
                           select e;

            return View(consulta.ToList());
            //return View(db.pm_events.ToList());
        }



        //
        // GET: /Eventos/Details/5

        public ViewResult Details(int id)
        {
            pm_events pm_events = db.pm_events.Find(id);
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
        public ActionResult Create(pm_events pm_events)
        {
            pm_events.createdDate = DateTime.Now;
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
 
        public ActionResult Edit(int id)
        {
            pm_events pm_events = db.pm_events.Find(id);
            return View(pm_events);
        }

        //
        // POST: /Eventos/Edit/5

        [HttpPost]
        public ActionResult Edit(pm_events pm_events)
        {
            pm_events.updateDate = DateTime.Now;
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
 
        public ActionResult Delete(int id)
        {
            pm_events pm_events = db.pm_events.Find(id);
            return View(pm_events);
        }

        //
        // POST: /Eventos/Delete/5

        [HttpPost, ActionName("Delete")]
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