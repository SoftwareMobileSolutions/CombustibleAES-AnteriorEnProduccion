using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using FLEETSMVC.Util;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteValesActualizados/ReporteValesActualizados" })]
    public class ReporteValesActualizadosController : Controller
    {
        private ReporteValesActualizadosDAL rvaDAL;
        LogIn DatosUsuarios;
        
        // GET: ReporteValesActualizados
        public ActionResult ReporteValesActualizados()
        {
            return View();
        }

        public ActionResult ObtenerValesSinCF([DataSourceRequest]DataSourceRequest request, int idVale, string numEquipo, string placa, string fechaInicio, string fechaFin, int idUsuario, int filtro)
        {
            rvaDAL = new ReporteValesActualizadosDAL();
            try
            {
                GetSession();
                var resultado = rvaDAL.ObtenerValesSinCF(idVale, numEquipo, placa, fechaInicio, fechaFin, idUsuario, filtro, DatosUsuarios.usertypeid, DatosUsuarios.userid);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActualizarCreditoFiscal(List<int> idVales, string CF, string fechaCF)
        {
            rvaDAL = new ReporteValesActualizadosDAL();
            try
            {
                int v = idVales.Count;
                int resultado = -1;
                for (int i = 0; i < v; i++)
                {
                     resultado = rvaDAL.ActualizarCreditoFiscal(idVales[i], CF, fechaCF);
                }
                if (resultado == 1) return Json(new { codigo = 1, mensaje = "El número y fecha de crédito fiscal de" + (v > 1 ? " los vales han sido actualizados." : "l vale ha sido actualizado.") });
                else return Json(new { codigo = -1, mensaje = "Algo salió mal al actualizar la fecha de factura." });
            }
            catch(Exception e)
            {
                return Json(new { codigo = -1, mensaje = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerFiltros()
        {
            List<FiltrosReporteValesActualizados> filtros = new List<FiltrosReporteValesActualizados>();
            //"Número de vale", "Número de equipo", "Placa", "Fecha de generación", "Fecha de actualización", "Usuario"
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 1, Nombre = "Número de vale" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 2, Nombre = "Número de equipo" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 3, Nombre = "Placa" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 4, Nombre = "Fecha de generación" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 5, Nombre = "Fecha de actualización" });
            GetSession();
            if (DatosUsuarios.usertypeid == 2) filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 6, Nombre = "Usuario" });
            return Json(filtros, JsonRequestBehavior.AllowGet);
        }

        private void GetSession()
        {
            DatosUsuarios = (Session["User"] as LogIn);
        }
    }
}