using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLEETSMVC.Util;
using CombustibleAES.Models;
using Microsoft.Reporting.WebForms;
using System.IO;
//Kendo Grid
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ValeTaxi/ValeTaxi" })]
    public class ValeTaxiController : Controller
    {
        ValeTaxiDAL taxiDAL;
        LogIn DatosUsuarios;

        // GET: ValeTaxi
        public ActionResult ValeTaxi()
        {
            return View();
        }

        public ActionResult InsertarVale(string IdEmpresa, string Pasajero, string Desde, int IdTipoUnidad, int IdArea, string CentroCosto, string OrdenEstadistica)
        {
            try
            {
                taxiDAL = new ValeTaxiDAL();
                GetSession();
                int resultado = taxiDAL.InsertarValeTaxi(IdEmpresa, Pasajero, Desde, IdTipoUnidad, IdArea, CentroCosto, OrdenEstadistica, DatosUsuarios.userid);
                string top = "Aviso";
                string msj = "";
                int lvl = 0;
                int estado = 0;

                if (resultado > 0)
                {
                    msj = "¡El vale ha sido generado exitósamente!";
                    lvl = 2;
                    estado = 1;
                }
                /*else if (resultado == -2)
                {
                    msj = "Por favor ingrese el nombre del pasajero.";
                    lvl = 3;
                    estado = -2;
                }*/
                else if (resultado == -3)
                {
                    msj = "Por favor ingrese el lugar de partida del viaje en taxi.";
                    lvl = 3;
                    estado = -3;
                }
                else
                {
                    msj = "Ocurrió un error al generar el vale de taxi. Por favor comuníquese con soporte técnico.";
                    lvl = 4;
                    estado = -1;
                }
                
                return Json(new { ValeId =  resultado, Estado = estado, Cabecera = top, Mensaje = msj, Nivel = lvl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Estado = 0, Mensaje = e.Message, Nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerVale([DataSourceRequest] DataSourceRequest request, int IdVale)
        {
            taxiDAL = new ValeTaxiDAL();
            var valeTaxi = taxiDAL.ObtenerValeTaxi(IdVale);
            return Json(valeTaxi.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValeTaxiImprimir(int vale)
        {
            try
            {
                taxiDAL = new ValeTaxiDAL();
                List<ValeTaxiImprimir> detalle = taxiDAL.ObtenerValeTaxiImprimir(vale).ToList();
                if (detalle.Count > 0)
                {
                    CombustibleAES.DataSets.Combustible ds = new DataSets.Combustible();
                    ValeTaxiImprimir objTemp = detalle[0];
                    ds.ValeTaxi.AddValeTaxiRow(
                        objTemp.NoVale, objTemp.FechaGeneracion, objTemp.Empresa, objTemp.Pasajero, objTemp.Desde,
                        objTemp.TipoUnidad, objTemp.CentroCosto, objTemp.OrdenEstadistica, objTemp.Area, objTemp.FechaVencimiento
                    );

                    LocalReport rpViewer = new LocalReport();
                    string path = Path.Combine(Server.MapPath("~/Reportes"), "ValeTaxi.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        rpViewer.ReportPath = path;
                    }
                    else
                    {
                        return View("ValeTaxi");
                    }

                    rpViewer.DataSources.Add(new ReportDataSource("Combustible", ds.Tables[1]));

                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "<MarginTop>0in</MarginTop>" +
                    "<MarginLeft>0n</MarginLeft>" +
                    "<MarginRight>0in</MarginRight>" +
                    "<MarginBottom>0in</MarginBottom>" +
                    "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    renderedBytes = rpViewer.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                    return File(renderedBytes, mimeType);
                }
            }
            catch (Exception ex)
            {
                return Json(new { cod = -1, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return View("ValeTaxi");
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}