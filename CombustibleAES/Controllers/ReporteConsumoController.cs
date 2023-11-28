using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CombustibleAES.Models;
using FLEETSMVC.Util;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteConsumo/ReporteConsumo" })]
    public class ReporteConsumoController : Controller
    {
        LogIn DatosUsuarios;
        // GET: ReporteConsumo
        public ActionResult ReporteConsumo()
        {
            return View();
        }

        public ActionResult ObtenerReporteConsumo([DataSourceRequest]DataSourceRequest request, int filtro, string fechaInicio, string fechaFin)
        {
            try
            {
                GetSession();
                ReporteConsumoDAL rcDAL = new ReporteConsumoDAL();
                var resultado = rcDAL.ObtenerReporteConsumo(DatosUsuarios.userid, DatosUsuarios.usertypeid, filtro, fechaInicio, fechaFin);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerFiltros()
        {
            //filtros.Add(new FiltroReporteConsumo() { });
            List<FiltroReporteConsumo> filtros = new List<FiltroReporteConsumo>();
            filtros.Add(new FiltroReporteConsumo() { FiltroId = 1, Filtro = "Fecha de Generación" });
            filtros.Add(new FiltroReporteConsumo() { FiltroId = 2, Filtro = "Fecha de Actualización" });
            filtros.Add(new FiltroReporteConsumo() { FiltroId = 3, Filtro = "Fecha de Crédito Fiscal" });
            return Json(filtros, JsonRequestBehavior.AllowGet);
        }

        private void GetSession()
        {
            DatosUsuarios = Session["User"] as LogIn;
        }
    }
}