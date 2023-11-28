using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "Home/Index" })]
    public class HomeController : Controller
    {
        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;
        public HomeController()
        {
            DatosUsuarios = new LogIn();
            ListPermisosDAL = new PermissionDAL();
        }
        public ActionResult Index()
        {
            GetSession();
            bool redireccion = (DatosUsuarios ==null);
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

        [HttpPost]
        public ActionResult GenerarMenu()
        {
            try
            {
                DatosUsuarios = Session["User"] as LogIn;
                if (Session["PermisosUser"] == null)
                {
               
                    Session["PermisosUser"] = ListPermisosDAL.ObtenerMenu(DatosUsuarios.usertypeid);
                }
                var Menu = Session["PermisosUser"] as IList<Tree>;// Se llena en Util/Autentication

                var GrandParent = Menu.Select(t => new
                {
                    Name = t.Name,
                    codigo = t.Cod,
                    parent = t.Parent,
                    granparent = t.Grandparent,
                    Nivel = t.Nivel,
                    Icon = t.Icon,
                    valorid = t.Valorid,
                    urlpage = t.Urlpage,
                    ImageUrl = t.Urlimagen
                }).Where(t => t.Nivel == 0)/*.OrderBy(t => t.Name)*/.ToList();

                var Parent = Menu.Select(t => new
                {
                    Name = t.Name,
                    codigo = t.Cod,
                    parent = t.Parent,
                    granparent = t.Grandparent,
                    Nivel = t.Nivel,
                    Icon = t.Icon,
                    valorid = t.Valorid,
                    urlpage = t.Urlpage,
                    ImageUrl = t.Urlimagen
                }).Where(t => t.Nivel == 1).OrderBy(t => t.Name).ToList();

                var Children = Menu.Select(t => new
                {
                    Name = t.Name,
                    codigo = t.Cod,
                    parent = t.Parent,
                    granparent = t.Grandparent,
                    Nivel = t.Nivel,
                    Icon = t.Icon,
                    valorid = t.Valorid,
                    urlpage = t.Urlpage,
                    ImageUrl = t.Urlimagen
                }).Where(t => t.Nivel == 2).OrderBy(t => t.Name).ToList();

                var arbolMenu = GrandParent.Select(p => new
                {
                    Name = p.Name,
                    codigo = p.codigo,
                    parent = p.parent,
                    granparent = p.granparent,
                    Icon = p.Icon,
                    Nivel = p.Nivel,
                    ImageUrl = p.ImageUrl,
                    PageUrl = p.urlpage,
                    ValorID = p.valorid,
                    HasChildren = Parent.Any(),
                    Children = Parent.Select(c => new
                    {
                        Name = c.Name,
                        codigo = c.codigo,
                        parent = c.parent,
                        granparent = c.granparent,
                        Icon = c.Icon,
                        Nivel = c.Nivel,
                        ImageUrl = c.ImageUrl,
                        PageUrl = c.urlpage,
                        ValorID = c.valorid,
                        HasChildren = Children.Any(),
                        Children = Children.Select(t => new
                        {
                            Name = t.Name,
                            codigo = t.codigo,
                            parent = t.parent,
                            granparent = t.granparent,
                            Nivel = t.Nivel,
                            Icon = t.Icon,
                            PageUrl = t.urlpage,
                            ImageUrl = t.ImageUrl,
                            ValorID = t.valorid,
                            HasChildren = false
                        }).Where(t => t.parent == c.codigo).OrderBy(t => t.Name).ToList()
                    }).Where(c => c.parent == p.codigo).OrderBy(c => c.Name).ToList()
                }).OrderBy(p => p.ValorID).ToList();
                return Json(arbolMenu, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return View();
            }
        }
       
        private void GetSession()
        {
            DatosUsuarios = (System.Web.HttpContext.Current.Session["User"] as LogIn);
        }

    }
}