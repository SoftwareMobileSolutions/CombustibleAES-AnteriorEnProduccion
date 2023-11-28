using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLEETSMVC.Util;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CombustibleAES.Models;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteGeneralValesTaxi/ReporteGeneralValesTaxi" })]
    public class ReporteGeneralValesTaxiController : Controller
    {
        ReportesValesTaxiDAL rDAL;
        LogIn DatosUsuarios;
        // GET: ReporteGeneralValesTaxi
        public ActionResult ReporteGeneralValesTaxi()
        {
            return View();
        }

        public ActionResult ObtenerReporteGeneralTaxi([DataSourceRequest]DataSourceRequest request, string strFechaInicio, string strFechaFin, int numVale, string creditoFiscal, int filtro)
        {
            rDAL = new ReportesValesTaxiDAL();
            try
            {
                GetSession();
                var resultado = rDAL.ObtenerReporteGeneralTaxi(DatosUsuarios.userid, DatosUsuarios.usertypeid, strFechaInicio, strFechaFin, numVale, creditoFiscal, filtro);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}