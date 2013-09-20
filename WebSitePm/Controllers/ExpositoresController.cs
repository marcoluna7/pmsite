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
    public class ExpositoresController : Controller
    {
        private pmsiteEntities db = new pmsiteEntities();

        //
        // GET: /Expositores/

        public ViewResult Index(string nombre)
        {
            var consulta = db.pm_speaker.Where(s => string.IsNullOrEmpty(nombre) || s.firstName.ToLower().Contains(nombre) || s.lastName.ToLower().Contains(nombre));
            return View(db.pm_speaker.ToList());
        }

        //
        // GET: /Expositores/Details/5

        public ViewResult Details(int id)
        {
            pm_speaker pm_speaker = db.pm_speaker.Find(id);
            return View(pm_speaker);
        }

        //
        // GET: /Expositores/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Expositores/Create

        [HttpPost]
        public ActionResult Create(pm_speaker pm_speaker)
        {
            if (ModelState.IsValid)
            {
                db.pm_speaker.Add(pm_speaker);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pm_speaker);
        }
        
        //
        // GET: /Expositores/Edit/5
 
        public ActionResult Edit(int id)
        {
            pm_speaker pm_speaker = db.pm_speaker.Find(id);
            return View(pm_speaker);
        }

        //
        // POST: /Expositores/Edit/5

        [HttpPost]
        public ActionResult Edit(pm_speaker pm_speaker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pm_speaker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pm_speaker);
        }

        //
        // GET: /Expositores/Delete/5
 
        public ActionResult Delete(int id)
        {
            pm_speaker pm_speaker = db.pm_speaker.Find(id);
            return View(pm_speaker);
        }

        //
        // POST: /Expositores/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            pm_speaker pm_speaker = db.pm_speaker.Find(id);
            db.pm_speaker.Remove(pm_speaker);
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