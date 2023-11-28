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
    [Autorizacion(ModuloUrl = new string[] { "LiquidarVale/LiquidarVale" })]
    public class LiquidarValeController : Controller
    {
        LiquidarValeDAL liquidarvalDal;

        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;
        public LiquidarValeController()
        {
            DatosUsuarios = new LogIn();
            ListPermisosDAL = new PermissionDAL();
        }

        // GET: LiquidarVale
        public ActionResult LiquidarVale()
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

        public JsonResult ObtenerValeLiqui(int valeid)
        {
            try
            {
                liquidarvalDal = new LiquidarValeDAL();
                List<LiquidarVale> detalle = liquidarvalDal.ObtenerValeMostrar(valeid).ToList();
                if(detalle.Count <= 0)
                {
                    return Json(new { mensaje = "El número de vale ingresado no se ha encontrado en sistema ó ya se encuentra actualizado.", codigo = 0,
                         NumeroVale = 0,Alias = "",Placa  = "",Empresa = "",FechaGeneracion = "",NoEmpleado = "",TipoCombustible = "",
                         CentroCosto = "",Odometro = 0,CantidadGalones = 0,TotalPrecio = 0,NombreEmpleado = "",OdometroAnt = 0,mobileid = 0
                         }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mensaje = "", codigo = 1,
                        NumeroVale = detalle[0].NumeroVale,Alias = detalle[0].Alias,Placa = detalle[0].Placa,Empresa = detalle[0].Empresa,
                        FechaGeneracion = detalle[0].FechaGeneracion,NoEmpleado = detalle[0].NoEmpleado,TipoCombustible = detalle[0].TipoCombustible,
                        CentroCosto = detalle[0].CentroCosto,Odometro = detalle[0].Odometro,CantidadGalones = detalle[0].CantidadGalones,
                        TotalPrecio = detalle[0].TotalPrecio,NombreEmpleado = detalle[0].NombreEmpleado,OdometroAnt = detalle[0].OdometroAnt,
                        mobileid = detalle[0].mobileid
                    }, JsonRequestBehavior.AllowGet);
                }                
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1,
                    NumeroVale = 0,Alias = "",Placa = "",Empresa = "",FechaGeneracion = "",NoEmpleado = "",TipoCombustible = "",
                    CentroCosto = "",Odometro = 0,CantidadGalones = 0,TotalPrecio = 0,NombreEmpleado = "",OdometroAnt = 0,
                    mobileid = 0
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ActualizarVale(int vale,int Odometro,string Galones,string Precio,int EstacionId,string PrecioGalon)
        {
            try
            {
                liquidarvalDal = new LiquidarValeDAL();
                var datosUser = Session["User"] as LogIn;
                int res = liquidarvalDal.ActualizarVale(vale, Odometro, double.Parse(Galones, System.Globalization.CultureInfo.InvariantCulture), double.Parse(Precio, System.Globalization.CultureInfo.InvariantCulture), EstacionId, datosUser.userid, double.Parse(PrecioGalon, System.Globalization.CultureInfo.InvariantCulture));
                if(res == 1)
                {
                    return Json(new { mensaje = "Vale actualizado satisfactoriamente.", codigo = res }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mensaje = "Se presentó un problema al momento de actualizar el vale.", codigo = res }, JsonRequestBehavior.AllowGet);
                }
                                
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = "Se presentó un problema al momento de actualiza el vale.", codigo = -3 }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Obtiene toda la informacion del vale ya actualizado para mostrarlo en un kendoGrid
        /// </summary>
        /// <param name="request">Request de la tabla</param>
        /// <param name="NumVale">Número de vale de combustible</param>
        /// <returns>Json con información</returns>
        public ActionResult obtenerValeActualizado([DataSourceRequest] DataSourceRequest request, int NumVale)
        {
            liquidarvalDal = new LiquidarValeDAL();
            //Se obtiene la información del vale numero "NumVale"
            List<LiquidarVale> resultado = liquidarvalDal.ObtenerValeActualizado(NumVale).ToList();
            //Se retorna la información obtenida
            return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}