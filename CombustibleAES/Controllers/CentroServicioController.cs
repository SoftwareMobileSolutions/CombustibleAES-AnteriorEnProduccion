using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CombustibleAES.Models;
using FLEETSMVC.Util;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "CentroServicio/CentroServicio" })]
    public class CentroServicioController : Controller
    {
        
        CentroServicioDAL centroDAL;
        //
        // GET: /CentroServicio/
        public ActionResult CentroServicio()
        {
            return View();
        }

        public ActionResult GridCentroServicios([DataSourceRequest] DataSourceRequest request)
        {            
            centroDAL=new CentroServicioDAL();
            var GridCentro = centroDAL.ObtenerCentrosServicio();
            return Json(GridCentro.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }


        public JsonResult InsertarCentroServicio(string CentroServicio,string Direccion,string NIT,string Email,string Codigo,string Ciudad,string Contacto,string Telefono)
        {
            try
            {
                centroDAL = new CentroServicioDAL();
                int res = centroDAL.InsertarCentroServicio(CentroServicio, Direccion, NIT, Email, Codigo, Ciudad, Contacto, Telefono);
                return Json(new { mensaje = "", codigo = 1 }, JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1 }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ActualizarCentroServicio(int Gasolineraid,string CentroServicio, string Direccion, string NIT, string Email, string Codigo, string Ciudad, 
            string Contacto, string Telefono)
        {
            try
            {
                centroDAL = new CentroServicioDAL();
                int res = centroDAL.ActualizarCentroServicio(Gasolineraid,CentroServicio, Direccion, NIT, Email, Codigo, Ciudad, Contacto, Telefono);
                return Json(new { mensaje = "", codigo = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1 }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}