using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLEETSMVC.Util;
using CombustibleAES.Models;
using CombustibleVales.Models;
using CombustibleVales.Utilidades;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;



namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "Usuario/Usuario" })]
    public class UsuarioController : Controller
    {
        UserDal userDalAcceso;



        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult GridUsuarios ([DataSourceRequest] DataSourceRequest request)
        {
            userDalAcceso = new UserDal();
            var GridUsuariosT = userDalAcceso.ObtenerUsuarios();
            return Json(GridUsuariosT.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }



        public JsonResult ComboPerfil()
        {
            userDalAcceso = new UserDal();
            var ListaPerfiles = userDalAcceso.ObtenerPerfiles();
            return Json(ListaPerfiles, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ComboGasolinera()
        {
            userDalAcceso = new UserDal();
            var ListaGasolineras = userDalAcceso.ObtenerGasolineras();
            return Json(ListaGasolineras, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertarUsuario(string usuario,string nombre,string apellido,string contrasenia,string email,int usertypeid,int gasolineraID)
        {
            try
            {
                userDalAcceso = new UserDal();
                int res = userDalAcceso.InsertarUsuario(usuario, nombre, apellido, contrasenia, email, usertypeid, gasolineraID);
                if(res == -2)
                {
                    return Json(new { mensaje = "El usuario ya existe, por favor cambie el nombre de usuario.", codigo = -1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mensaje = "", codigo = 1 }, JsonRequestBehavior.AllowGet);
                }                
            }
            catch(Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ActualizarUsuario(int userid,string nombre,string apellido,string contrasenia,string email,int usertypeid,int activo,int gasolineraID,int actualizarcontra)
        {
            try
            {
                userDalAcceso = new UserDal();
                int res = userDalAcceso.ModificarUsuario(userid, nombre, apellido, contrasenia, email, usertypeid, activo, gasolineraID,actualizarcontra);                
                return Json(new { mensaje = "", codigo = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1 }, JsonRequestBehavior.AllowGet);
            }
        }





    }
}