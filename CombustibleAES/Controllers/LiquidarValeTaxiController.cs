using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;
//Kendo Grid
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "LiquidarValeTaxi/LiquidarValeTaxi" })]
    public class LiquidarValeTaxiController : Controller
    {
        ValeTaxiDAL tDAL;

        // GET: LiquidarValeTaxi
        public ActionResult LiquidarValeTaxi()
        {
            return View();
        }

        public ActionResult ObtenerValeLiquidado([DataSourceRequest]DataSourceRequest request, int IdVale)
        {
            try
            {
                tDAL = new ValeTaxiDAL();
                var resultado = tDAL.ObtenerValeTaxiLiquidado(IdVale);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult ObtenerVale(int IdVale)
        {
            try
            {
                tDAL = new ValeTaxiDAL();
                IList<ValeTaxi> resultado = tDAL.ObtenerValeTaxi(IdVale);
                if (resultado.Count() > 0)
                {
                    return Json(new { vale = resultado[0], codigo = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { codigo = -1, mensaje = "El número de vale ingresado no se ha encontrado en sistema ó ya se encuentra actualizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { codigo = -1, mensaje = e.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LiquidarVale(string desde, int IdVale, string Destino1, string Destino2, string Destino3, string FechaSalida, string FechaLlegada, string Conductor, string MontoVale, string NoVehiculo)
        {
            try
            {
                tDAL = new ValeTaxiDAL();
                int resultado = tDAL.LiquidarValeTaxi(desde, IdVale, Destino1, Destino2, Destino3, FechaSalida, FechaLlegada, NoVehiculo, Conductor, double.Parse(MontoVale, System.Globalization.CultureInfo.InvariantCulture));
                return Json(new { codigo = 1, mensaje = "Vale actualizado satisfactoriamente." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { codigo = -1, mensaje = "Se presentó un problema al momento de actualiza el vale.<br />" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}