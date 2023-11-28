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
    [Autorizacion(ModuloUrl = new string[] { "CorregirVale/CorregirVale" })]
    public class CorregirValeController : Controller
    {
        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;

        // GET: CorregirVale
        public ActionResult CorregirVale()
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

        public ActionResult ObtenerValeCorregir(int idvale)
        {
            try
            {
                CorregirValeDAL corregirValeDAL = new CorregirValeDAL();
                GetSession();
                List<CorregirVale> resultado = corregirValeDAL.obtenerValeCorregir(idvale, DatosUsuarios.usertypeid, DatosUsuarios.userid);
                if (resultado.Count <= 0)
                {
                    //string msj = (DatosUsuarios.usertypeid == 1 || DatosUsuarios.usertypeid == 6 || DatosUsuarios.usertypeid == 7) ? "El número de vale ingresado no se ha encontrado en sistema, aún no se encuentra actualizado o no ha sido generado por este usuario." : "El número de vale ingresado no se ha encontrado en sistema ó aún no se encuentra actualizado.";
                    string msj = "El número de vale ingresado no se ha encontrado en sistema ó aún no se encuentra actualizado.";
                    return Json(new { vale = "", mensaje = msj, codigo = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    /*
                    GetSession();
                    if (DatosUsuarios.usertypeid == 2)
                    {
                        resultado[0].EnableAnular = true;
                    }
                    else
                    {
                        if (resultado[0].IdEstadoVale > 1) resultado[0].EnableAnular = false;
                        else if (resultado[0].IdEstadoVale == 1) resultado[0].EnableAnular = true;
                    }
                    */
                    return Json(new { vale = resultado, codigo = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { vale = "", mensaje = ex.Message, codigo = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerValeGrid([DataSourceRequest]DataSourceRequest request, int idvale)
        {
            try
            {
                CorregirValeDAL corregirValeDAL = new CorregirValeDAL();
                GetSession();
                var resultado = corregirValeDAL.obtenerValeCorregir(idvale, DatosUsuarios.usertypeid, DatosUsuarios.userid);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Corregir(int idVale, int estadoVale, string creditoFiscal, string creditoFiscal_ant, int odometro , int odometro_ant, string galones, string galones_ant, string precioGalon, string precioGalon_ant, string total, string total_ant, int estacionServicio, int tipoCombustible, string comentarioAnulado, string fechaCF)
        {
            try
            {
                var userInfo = Session["user"] as LogIn;
                string fechaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                CorregirValeDAL corregirValeDAL = new CorregirValeDAL();
                switch (estadoVale)
                {
                    case 3:
                        creditoFiscal = null;
                        comentarioAnulado = null;
                        break;

                    case 4:
                        comentarioAnulado = null;
                        break;
                }
                GetSession();
                int resultado = corregirValeDAL.corregirVale(idVale, estadoVale, creditoFiscal, creditoFiscal_ant, odometro, odometro_ant, double.Parse(galones, System.Globalization.CultureInfo.InvariantCulture), double.Parse(galones_ant, System.Globalization.CultureInfo.InvariantCulture), double.Parse(precioGalon, System.Globalization.CultureInfo.InvariantCulture), double.Parse(precioGalon_ant, System.Globalization.CultureInfo.InvariantCulture), double.Parse(total, System.Globalization.CultureInfo.InvariantCulture), double.Parse(total_ant, System.Globalization.CultureInfo.InvariantCulture), estacionServicio, tipoCombustible, fechaActualizacion, userInfo.userid, comentarioAnulado, DatosUsuarios.usertypeid, fechaCF);


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
            catch (Exception ex)
            {
                return Json(new { respuesta = -4, mensaje = "Ha ocurrido un error interno. Por favor comuníquese con soporte técnico.<br>Error: " + ex.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}