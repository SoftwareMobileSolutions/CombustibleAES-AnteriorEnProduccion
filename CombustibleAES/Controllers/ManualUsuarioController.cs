using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;

namespace CombustibleAES.Controllers
{
    public class ManualUsuarioController : Controller
    {
        // GET: ManualUsuario

        LogIn DatosUsuarios;
        public ActionResult ManualUsuario()
        {
            GetSession();
            string pdfUrl = Server.MapPath("~/Manuales");
            pdfUrl += "/" + DatosUsuarios.usertypeid + "/Manual.pdf";
            byte[] bytesPDF = System.IO.File.ReadAllBytes(pdfUrl);
            return File(bytesPDF, "application/pdf");
        }

        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }
    }
}