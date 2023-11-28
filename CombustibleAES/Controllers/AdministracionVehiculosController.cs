using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "AdministracionVehiculos/AdministracionVehiculos" })]
    public class AdministracionVehiculosController : Controller
    {
        AdministracionVehiculosDAL avDAL;

        // GET: AdministracionVehiculos
        public ActionResult AdministracionVehiculos()
        {
            return View();
        }

        public ActionResult ObtenerEstadosVehiculos()
        {
            try
            {
                EstadosVehiculosDAL evDAL = new EstadosVehiculosDAL();
                var resultado = evDAL.ObtenerEstados();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerVehiculo(int idVehiculo)
        {
            try
            {
                avDAL = new AdministracionVehiculosDAL();
                var resultado = avDAL.ObtenerVehiculo(idVehiculo);
                return Json(new { data = resultado, codigo = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { codigo = -1, mensaje = "Error al obtener la información de el/los vehículos. " + e.Message });
            }
        }

        public ActionResult ObtenerVehiculos([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                avDAL = new AdministracionVehiculosDAL();
                var resultado = avDAL.ObtenerVehiculo(0);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null);
            }
        }

        public JsonResult InsertarMobile(int empresaid, string alias, string placa, string make, string model, int yearmade, string kmxGalon)
        {
            try
            {
                avDAL = new AdministracionVehiculosDAL();
                int resultado = avDAL.InsertarMobile(empresaid, alias, placa, make, model, yearmade, double.Parse(kmxGalon, System.Globalization.CultureInfo.InvariantCulture));
                if (resultado == 1)
                {
                    return Json(new { codigo = 1, mensaje = "El vehículo ha sido guardado exitosamente!", nivel = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { codigo = -1, mensaje = "Ocurrió un error al guardar la información del vehículo.", nivel = 4 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { codigo = -1, mensaje = e.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ActualizarMobile(int mobileid, int empresaid, string alias, string placa, string make, string model, int yearmade, string kmxGalon, int status)
        {
            try
            {
                avDAL = new AdministracionVehiculosDAL();
                int resultado = avDAL.ActualizarMobile(mobileid, empresaid, alias, placa, make, model, yearmade, double.Parse(kmxGalon, System.Globalization.CultureInfo.InvariantCulture), status);
                if (resultado == 1)
                {
                    return Json(new { codigo = 1, mensaje = "La información ha sido actualizada exitosamente!", nivel = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { codigo = -1, mensaje = "Ocurrió un error al actualizar la información del vehículo.", nivel = 4 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { codigo = -1, mensaje = e.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}