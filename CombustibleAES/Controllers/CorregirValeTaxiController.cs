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
    [Autorizacion(ModuloUrl = new string[] { "CorregirValeTaxi/CorregirValeTaxi" })]
    public class CorregirValeTaxiController : Controller
    {
        LogIn DatosUsuarios;
        CorregirValeTaxiDAL cvDAL;
        EstadosValeTaxiDAL evDAL;

        // GET: CorregirValeTaxi
        public ActionResult CorregirValeTaxi()
        {
            return View();
        }

        public ActionResult ObtenerEstadosValeTaxi(int estadoId)
        {
            try
            {
                GetSession();
                evDAL = new EstadosValeTaxiDAL();
                var resultado = evDAL.ObtenerEstadosValesTaxi(DatosUsuarios.usertypeid, estadoId);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null);
            }
        }

        public ActionResult ObtenerValeTaxiCorregir(int idVale)
        {
            try
            {
                GetSession();
                cvDAL = new CorregirValeTaxiDAL();
                IList<CorregirValeTaxi> resultado = cvDAL.ObtenerValeTaxiCorregir(idVale, DatosUsuarios.usertypeid, DatosUsuarios.userid);
                if (resultado.Count() > 0)
                {
                    /*
                    if (DatosUsuarios.usertypeid == 2)
                    {
                        resultado[0].EnableAnular = true;
                    }
                    else
                    {
                        if (resultado[0].IdEstado > 1) resultado[0].EnableAnular = false;
                        else if (resultado[0].IdEstado == 1) resultado[0].EnableAnular = true;
                    }
                    */
                    return Json(new { codigo = 1, Vale = resultado.ElementAt(0) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { codigo = 0, mensaje = "El número de vale que ha ingresado no existe." });
                }
            }
            catch(Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MostrarValeCorregido([DataSourceRequest]DataSourceRequest request, int idvale)
        {
            try
            {
                GetSession();
                cvDAL = new CorregirValeTaxiDAL();
                List<CorregirValeTaxi> resultado = new List<CorregirValeTaxi>();
                resultado.Add(cvDAL.ObtenerValeTaxiCorregir(idvale, DatosUsuarios.usertypeid, DatosUsuarios.userid)[0]);
                if (resultado != null)
                {
                    return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CorregirVale(int idVale, int idEstado, string hacia, string hacia2, string hacia3, string fechaSalida, string fechaLlegada, string conductor, string monto, string noVehiculo, string comentarioAnulado, string creditoFiscal, string fechaCredito, string fechaEstado)
        {
            try
            {
                cvDAL = new CorregirValeTaxiDAL();
                if (fechaCredito == "")
                {
                    fechaCredito = null;
                }
                if (hacia2 == "") hacia2 = null;
                if (hacia3 == "") hacia3 = null;
                if (comentarioAnulado == "") comentarioAnulado = null;
                if (fechaEstado == "getFecha")
                {
                    fechaEstado = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                }
                else if (fechaEstado == "")
                {
                    fechaEstado = null;
                }
                GetSession();
                int resultado = cvDAL.CorregirValeTaxi(idVale, idEstado, hacia, hacia2, hacia3, fechaSalida, fechaLlegada, conductor, double.Parse(monto, System.Globalization.CultureInfo.InvariantCulture), noVehiculo, comentarioAnulado, creditoFiscal, fechaCredito, DatosUsuarios.userid, fechaEstado, DatosUsuarios.usertypeid);
                string msj = "";
                int lvl = 0;
                switch (resultado)
                {
                    case 1:
                        msj = "¡Estado(s) actualizado/actualizados exitosamente!";
                        lvl = 2;
                        break;

                    case -1:
                        msj = "Sistema: No es posible realizar el cambio de estado especificado.";
                        lvl = 4;
                        break;

                    case -2:
                        msj = "Su nivel de acceso no es suficiente para realizar el cambio de estado especificado.";
                        lvl = 4;
                        break;

                    case -3:
                        msj = "Para realizar este cambio de estado debe utilizar el apartado de corrección de vales.";
                        lvl = 3;
                        break;

                    default:
                        msj = "Ha ocurrido un error interno. Por favor comuníquese con soporte técnico.";
                        lvl = 4;
                        break;
                }

                return Json(new { respuesta = resultado, mensaje = msj, nivel = lvl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { respuesta = -4, mensaje = "Ha ocurrido un error interno. Por favor comuníquese con soporte técnico.<br>Error: " + e.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}