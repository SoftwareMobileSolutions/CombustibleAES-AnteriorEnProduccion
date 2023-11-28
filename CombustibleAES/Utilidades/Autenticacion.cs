using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using System.IO;

namespace FLEETSMVC.Util
{
    public class Autorizacion : AuthorizeAttribute
    {

        LogIn Usuario;
        PermissionDAL ListPermisosDAL;
        //public static IList<Tree> ListPermisos;
        public virtual string[] ModuloUrl { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var authroized = base.AuthorizeCore(httpContext);
            if (!authroized) {
                return false; //1-El usuario no está autenticado 2-La cookie ha expirado
            }
            var myvar = httpContext.Session["User"];// Chequea la session:
            if (myvar == null)
            {
                return false; // El inicio de sesión ha expirado
            }
            else
            {
                Usuario = httpContext.Session["User"] as LogIn;
                /*--- Limpia de archivos basura del usuario actual ---*/
                //if (httpContext.Session["DeletedFiles"] == null)
                //{
                //    string path = HttpContext.Current.Server.MapPath("~/App_Data");
                //    IEnumerable<string> archivos = Directory.GetFiles(path);
                //    archivos = archivos.Where(x => x.Split(new[] { "\\App_Data\\" }, StringSplitOptions.None)[1].Split(new[] { "_" }, StringSplitOptions.None)[0] == Usuario.userid.ToString());
                //    int cFiles = archivos.Count();
                //    for (int i = 0; i < cFiles; i++)
                //    {
                //        File.Delete(archivos.ElementAt(i));
                //    }

                //    httpContext.Session["DeletedFiles"] = "true";
                //}
                /*--- [////] ---*/

                ListPermisosDAL = new PermissionDAL();
                int bandera = 0;
                if (httpContext.Session["PermisosUser"] == null)
                {
                    httpContext.Session["PermisosUser"] = ListPermisosDAL.ObtenerMenu(Usuario.usertypeid);
                }
                foreach (var permiso in httpContext.Session["PermisosUser"]  as IList<Tree>)
                {
                    foreach (var modulo in ModuloUrl)
                    {
                        if (modulo == permiso.Urlpage)
                        {
                            bandera = 1;
                           
                            return true;
                        }
                    }
                }
                if (bandera == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}