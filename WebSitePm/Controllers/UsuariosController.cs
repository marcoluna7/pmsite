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
    public class UsuariosController : Controller
    {
        private pmsiteEntities db = new pmsiteEntities();

        //
        // GET: /Usuarios/

        public ViewResult Index()
        {
            return View(db.pm_user.ToList());
        }

        //
        // GET: /Usuarios/Details/5

        public ViewResult Details(int id)
        {
            pm_user pm_user = db.pm_user.Find(id);
            return View(pm_user);
        }

        //
        // GET: /Usuarios/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Usuarios/Create

        [HttpPost]
        public ActionResult Create(pm_user pm_user)
        {
            if (ModelState.IsValid)
            {
                db.pm_user.Add(pm_user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pm_user);
        }
        
        //
        // GET: /Usuarios/Edit/5
 
        public ActionResult Edit(int id)
        {
            pm_user pm_user = db.pm_user.Find(id);
            return View(pm_user);
        }

        //
        // POST: /Usuarios/Edit/5

        [HttpPost]
        public ActionResult Edit(pm_user pm_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pm_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pm_user);
        }

        //
        // GET: /Usuarios/Delete/5
 
        public ActionResult Delete(int id)
        {
            pm_user pm_user = db.pm_user.Find(id);
            return View(pm_user);
        }

        //
        // POST: /Usuarios/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            pm_user pm_user = db.pm_user.Find(id);
            db.pm_user.Remove(pm_user);
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