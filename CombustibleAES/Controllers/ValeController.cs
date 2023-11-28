using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;
using Microsoft.Reporting.WebForms;
//Kendo grid
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "Vale/Vale" })]
    public class ValeController : Controller
    {
        ValeDAL valDAL;

        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;
        public ValeController()
        {
            DatosUsuarios = new LogIn();
            ListPermisosDAL = new PermissionDAL();
        }

        public ActionResult Vale()
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

        public JsonResult ObtenerValeMostrar([DataSourceRequest] DataSourceRequest request, int NumVale)
        {
            valDAL = new ValeDAL();
            //DatosUsuarios = Session["User"] as LogIn;
            var ListaValeImprimir = valDAL.ObtenerValeMostrar(NumVale);
            return Json(ListaValeImprimir.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertarVale(int Companyid,int Rentado, int TipoCosto,int TipoCombustibleID,string FechaGeneracion,string NombreEmpleado,
	                                   string NumEmpleado,string CentroCosto,string NumOrden,int mobileid,string Placa,string Alias,string Inversion, string Saldo)
        {
            try
            {
                valDAL = new ValeDAL();
                var datosUser = Session["User"] as LogIn;                
                int res = valDAL.InsertarVale(Companyid, Rentado, TipoCosto, TipoCombustibleID, FechaGeneracion, datosUser.userid, NombreEmpleado,
                                              NumEmpleado, CentroCosto, NumOrden, mobileid, Placa, Alias, Inversion, double.Parse(Saldo, System.Globalization.CultureInfo.InvariantCulture));
                if (res == -2)
                {
                    return Json(new { mensaje = "El PEP no se encuentra en la base de datos.", codigo = -2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if(res == -1)
                    {
                        return Json(new { mensaje = "No puede generar más vales para este vehículo hasta que cierre los pendientes.", codigo = -1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { mensaje = "Vale ingresado satisfactoriamente.", codigo = res }, JsonRequestBehavior.AllowGet);
                    }                    
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message + ex.StackTrace, codigo = -3 }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ValeImprimir(int vale)
        {
            try
            {
                valDAL = new ValeDAL(); 
                DatosUsuarios = Session["User"] as LogIn;
                List<ValeImprimir> detalle = valDAL.ObtenerValeImprimir(vale, DatosUsuarios.userid).ToList();                
                if (detalle.Count > 0)
                {
                    CombustibleAES.DataSets.Combustible ds = new DataSets.Combustible();
                    ValeImprimir objTemp = detalle[0];
                    ds.Vale.AddValeRow(
                        objTemp.NumeroVale, objTemp.Alias, objTemp.Placa, objTemp.Empresa, objTemp.FechaGeneracion,
                        objTemp.NumEmpleado, objTemp.TipoCombustible, objTemp.InvoCC, objTemp.NumOrden, objTemp.UserAutorizacion,
                        objTemp.NombreEmpleado, objTemp.NumInversion, objTemp.FechaVencimiento);

                    
                    
                    LocalReport rpViewer = new LocalReport();
                    string path = Path.Combine(Server.MapPath("~/Reportes"), "Vale.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        rpViewer.ReportPath = path;
                    }
                    else
                    {
                        return View("Vale");
                    }
                    
                    rpViewer.DataSources.Add(new ReportDataSource("Combustible", ds.Tables[0]));
                    
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

            return View("Vale");
        }

        public ActionResult ObtenerPEPList()
        {
            PEPDAL ppDAL = new PEPDAL();
            try
            {
                var resultado = ppDAL.ObtenerPEPList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}