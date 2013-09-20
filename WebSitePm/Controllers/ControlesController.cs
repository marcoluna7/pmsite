using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer.DbLayer;

namespace WebSitePm.Controllers
{
    public class ControlesController : Controller
    {
        //
        // GET: /Controles/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSearchExpositor()
        {
            return PartialView("_AddExpoEvent");
        }

        public ActionResult GetCtlResultExpo(string nombre)
        {
            return PartialView("_CtlResultExpo",nombre);
        }

        public ActionResult GetCtlExpositoresEvent(int idEvent)
        {
            pm_events evento = new pm_events() { uid= idEvent};
            return PartialView("_ExpoEvents", evento);
        }



    }
}
