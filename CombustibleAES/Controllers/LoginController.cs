using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CombustibleVales.Utilidades;
using CombustibleAES.Models;
using FLEETSMVC.Util;
using System.Web.Mvc;

namespace CombustibleAES.Controllers
{
    public class LoginController : Controller
    {
        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;

        //
        // GET: /Login/
        public static IList<LogIn> Users { get; set; }
        LogInDal logDal;
        public LoginController()
        {
            logDal = new LogInDal();
            ListPermisosDAL = new PermissionDAL();
        }

        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return View("Login");
        }

        public ActionResult Login()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return View();
        }

        public JsonResult ValidatePasswordDate(LogIn iUser)
        {
            string user = iUser.usuario;
            string pass = iUser.contrasenia;
            Users = logDal.CheckSessionByLastUpdate(user, pass);
            int ValidationType = Users.ElementAt(0).userid;
            if(ValidationType > 0)
            {
                Session["User"] = Users[0];
                FormsAuthentication.SetAuthCookie(Users[0].usuario, false);
                DatosUsuarios = Session["User"] as LogIn;
                if (Session["PermisosUser"] == null)
                {
                    Session["PermisosUser"] = ListPermisosDAL.ObtenerMenu(DatosUsuarios.usertypeid);
                }
            }

            return Json(ValidationType);
        }

        public JsonResult UpdatePassByExpired(string iUserName, string iPassword, string iNewPass)
        {
            int ansService = logDal.UpdatePasswordByExpiration(iUserName, iPassword, iNewPass);
            if(ansService == 1)
            {
                Users = logDal.CheckSession(iUserName, iNewPass);
                Session["User"] = Users[0];
                FormsAuthentication.SetAuthCookie(Users[0].usuario, false);
                DatosUsuarios = Session["User"] as LogIn;
                if (Session["PermisosUser"] == null)
                {
                    Session["PermisosUser"] = ListPermisosDAL.ObtenerMenu(DatosUsuarios.usertypeid);
                }
            }
            return Json(ansService, JsonRequestBehavior.AllowGet);
        }

       [HttpPost]
       [Throttle(Name="TestThrottle",Message="Usted ha realizado muchos intentos seguidos, espere {n} segundos para intentarlo de nuevo y refresque la página",Seconds=1)]
        public ActionResult Index(LogIn tempUser, string ReturnUrl)
        {
            try
            {
                string user = tempUser.usuario;
                string pass = tempUser.contrasenia;
                Users = logDal.CheckSession(user, pass);
                if(Users.Count <= 0) // Comentario de prueba.
                {
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    ModelState.AddModelError("errordeinicio", "Error interno, notificar a soporte.");
                }
                else
                {
                    if(Users.ElementAt(0).userid == 0)
                    {
                        ModelState.AddModelError("errodeinicio", "Erro de inicio, verifique su usuario y contraseña");
                    }
                    else
                    {
                        Session["User"] = Users[0];                        
                        FormsAuthentication.SetAuthCookie(Users[0].usuario, false);
                        DatosUsuarios = Session["User"] as LogIn;
                        if (Session["PermisosUser"] == null)
                        {
                            Session["PermisosUser"] = ListPermisosDAL.ObtenerMenu(DatosUsuarios.usertypeid);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("errordeinicio", ex.Message);
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");                      
        }
       public ActionResult LogOut()
       {
           Session["User"] = null;
           FormsAuthentication.SignOut();
           return RedirectToAction("Index", "Login");
       }

        public JsonResult RecoveryPassService(string iUser, string iEmail, string iRandomPass)
        {
            try
            {
                int res = logDal.recoveryPass(iUser, iEmail, iRandomPass);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}