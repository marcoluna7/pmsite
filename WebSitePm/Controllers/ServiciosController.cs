using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer.DbLayer;

namespace WebSitePm.Controllers
{
    public class ServiciosController : Controller
    {
        //
        // GET: /Servicios/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddExpoEvent(int idEvento, int idExpositor)
        {
            bool bandera = false;
            using(AcessDb acceso = AcessDb.GetInstance())
            {
                bandera = acceso.insertSpeakEvent(idEvento, idExpositor);
            }
            return Json(bandera);
        }

        public JsonResult DeleteSpeakEvent(int idEvento, int idExpositor)
        {
            bool bandera = false;
            using (AcessDb acceso = AcessDb.GetInstance())
            {
                bandera = acceso.deleteSpeakEvent(idEvento, idExpositor);
            }
            return Json(bandera);
        }



    }
}
