using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using FLEETSMVC.Util;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteValesActualizadosTaxi/ReporteValesActualizadosTaxi" })]
    public class ReporteValesActualizadosTaxiController : Controller
    {
        private ReporteValesActualizadosTaxiDAL rvatDAL;

        // GET: ReporteValesActualizadosTaxi
        public ActionResult ReporteValesActualizadosTaxi()
        {
            return View();
        }

        public ActionResult ObtenerValesTaxiCF([DataSourceRequest]DataSourceRequest request, string fechaInicio, string fechaFin)
        {
            rvatDAL = new ReporteValesActualizadosTaxiDAL();
            try
            {
                var resultado = rvatDAL.ObtenerValesTaxiCF(fechaInicio, fechaFin);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActualizarCreditoFiscal(List<int> idVales, string fechaCF, string CF)
        {
            rvatDAL = new ReporteValesActualizadosTaxiDAL();
            try
            {
                int resultado = -1;
                int v = idVales.Count;
                for (int i = 0; i < v; i++)
                {
                    resultado = rvatDAL.ActualizarCreditoFiscal(idVales[i], fechaCF, CF);
                }
                if (resultado == 1) return Json(new { codigo = 1, mensaje = "El número y fecha de crédito fiscal de" + (v > 1 ? " los vales de taxi han sido actualizados." : "l vale de taxi han sido actualizados.") });
                else return Json(new { codigo = -1, mensaje = "Algo salió mal al actualizar el crédito fiscal." });
            }
            catch(Exception e)
            {
                return Json(new { codigo = -1, mensaje = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}