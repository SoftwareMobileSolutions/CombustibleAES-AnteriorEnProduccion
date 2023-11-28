using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLEETSMVC.Util;
using CombustibleAES.Models;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ActualizarGas/ActualizarGas" })]
    public class ActualizarGasController : Controller
    {
        ActualizarGasDAL actualizarGasDAL;
        LiquidarValeDAL liquidarvalDal;

        // GET: ActualizarGas
        public ActionResult ActualizarGas()
        {
            return View();
        }

        public JsonResult ObtenerValeLiqui(int valeid)
        {
            try
            {
                liquidarvalDal = new LiquidarValeDAL();
                List<LiquidarVale> detalle = liquidarvalDal.ObtenerValeMostrar(valeid).ToList();
                if (detalle.Count <= 0)
                {
                    return Json(new
                    {
                        mensaje = "El número de vale ingresado no se ha encontrado en sistema ó ya se encuentra actualizado.",
                        codigo = 0,
                        NumeroVale = 0,
                        Alias = "",
                        Placa = "",
                        Empresa = "",
                        FechaGeneracion = "",
                        NoEmpleado = "",
                        TipoCombustible = "",
                        CentroCosto = "",
                        Odometro = 0,
                        CantidadGalones = 0,
                        TotalPrecio = 0,
                        NombreEmpleado = "",
                        OdometroAnt = 0,
                        mobileid = 0
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        mensaje = "",
                        codigo = 1,
                        NumeroVale = detalle[0].NumeroVale,
                        Alias = detalle[0].Alias,
                        Placa = detalle[0].Placa,
                        Empresa = detalle[0].Empresa,
                        FechaGeneracion = detalle[0].FechaGeneracion,
                        NoEmpleado = detalle[0].NoEmpleado,
                        TipoCombustible = detalle[0].TipoCombustible,
                        CentroCosto = detalle[0].CentroCosto,
                        Odometro = detalle[0].Odometro,
                        CantidadGalones = detalle[0].CantidadGalones,
                        TotalPrecio = detalle[0].TotalPrecio,
                        NombreEmpleado = detalle[0].NombreEmpleado,
                        OdometroAnt = detalle[0].OdometroAnt,
                        mobileid = detalle[0].mobileid
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    mensaje = ex.Message,
                    codigo = -1,
                    NumeroVale = 0,
                    Alias = "",
                    Placa = "",
                    Empresa = "",
                    FechaGeneracion = "",
                    NoEmpleado = "",
                    TipoCombustible = "",
                    CentroCosto = "",
                    Odometro = 0,
                    CantidadGalones = 0,
                    TotalPrecio = 0,
                    NombreEmpleado = "",
                    OdometroAnt = 0,
                    mobileid = 0
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ActualizarVale(int vale, int EstacionId)
        {
            try
            {
                actualizarGasDAL = new ActualizarGasDAL();
                var datosUser = Session["User"] as LogIn;
                int res = actualizarGasDAL.ActualizarVale(vale,EstacionId);
                if (res == 1)
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

    }


    

}