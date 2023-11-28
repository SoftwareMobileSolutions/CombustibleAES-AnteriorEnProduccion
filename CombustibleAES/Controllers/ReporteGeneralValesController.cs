using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;
//Kendo grid
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteGeneralVales/ReporteGeneralVales" })]
    public class ReporteGeneralValesController : Controller
    {
        ReporteslDAL valDAL;


        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;
        public ReporteGeneralValesController()
        {
            DatosUsuarios = new LogIn();
            ListPermisosDAL = new PermissionDAL();
        }

        public ActionResult ReporteGeneralVales()
        {
            GetSession();
            bool redireccion = (DatosUsuarios == null);
            if (redireccion)
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        public JsonResult ObtenerReporteGeneral([DataSourceRequest] DataSourceRequest request, string strFechaInicio, string strFechaFin, int numVale, string creditoFiscal, int filtro)
        {
            valDAL = new ReporteslDAL();
            var datosUser = Session["User"] as LogIn;
            DateTime dtFechaInicio = new DateTime();
            DateTime dtFechaFin = new DateTime();

            dtFechaInicio = DateTime.ParseExact(strFechaInicio, "yyyy/MM/dd HH:mm:ss", null);
            dtFechaFin = DateTime.ParseExact(strFechaFin, "yyyy/MM/dd HH:mm:ss", null);

            var ReporteGen = valDAL.ObtenerReporteGeneral(datosUser.userid, datosUser.usertypeid, dtFechaInicio, dtFechaFin, numVale, creditoFiscal, filtro);
            var retorno= Json(ReporteGen.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            retorno.MaxJsonLength = int.MaxValue;
            return retorno;
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}