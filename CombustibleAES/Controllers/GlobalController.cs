using System;
using System.Collections.Generic;
using System.Linq;
using CombustibleAES.Models;
using System.Web;
using System.Web.Mvc;

namespace CombustibleVales.Controllers
{
    public class GlobalController : Controller
    {        
        GlobalDAL globalDAL;
        LogIn DatosUsuarios;

        public JsonResult ComboEmpresas()
        {
            globalDAL = new GlobalDAL();
            var ListaEmpresas = globalDAL.ObtenerEmpresasAES();
            return Json(ListaEmpresas, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult ComboEquipos(int EmpresaID)
        {
            globalDAL = new GlobalDAL();
            var ListaEquipos = globalDAL.ObtenerEquiposFlota(EmpresaID);
            return Json(ListaEquipos, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ComboCombustible()
        {
            globalDAL = new GlobalDAL();
            var ListaCombustible = globalDAL.ObtenerTiposCombustible();
            return Json(ListaCombustible, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ComboGasolineras()
        {

            globalDAL = new GlobalDAL();
            var datosUser = Session["User"] as LogIn;
            globalDAL = new GlobalDAL();
            var ListaGasolinera = globalDAL.ObtenerGasolineras(datosUser.userid, datosUser.usertypeid);
            return Json(ListaGasolinera, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerPlaca(int mobileid)
        {                        
            try
            {
                globalDAL = new GlobalDAL();
                string strPlaca = globalDAL.ObtenerPlaca(mobileid);
                return Json(new { mensaje = "", codigo = 1, placa = strPlaca }, JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1, placa="" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObtenerFecha()
        {
            try
            {
                string strFecha = DateTime.UtcNow.AddHours(-6).ToString("dd/MM/yyyy");
                return Json(new { mensaje = "", codigo = 1, fecha = strFecha }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { mensaje = ex.Message, codigo = -1, fecha = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerEstados(int estadoId)
        {
            globalDAL = new GlobalDAL();
            GetSession();
            var resultado = globalDAL.ObtenerEstados(DatosUsuarios.usertypeid, estadoId);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerYears()
        {
            int start = 2000;
            int end = DateTime.Now.Year;
            List<Years_Months> years = new List<Years_Months>();
            for (int i = end; i >= start; i--)
            {
                Years_Months y = new Years_Months();
                y.Text = i.ToString();
                y.Value = i;
                years.Add(y);
            }
            return Json(years, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerUsuarios()
        {
            try
            {
                globalDAL = new GlobalDAL();
                var resultado = globalDAL.ObtenerUsuarios();
                resultado.Insert(0, new Usuarios() { IdUsuario = 0, Usuario = "Todos" });
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult ObtenerMeses(int year)
        {
            int mStart = 1;
            int mEnd = year == 0 ? DateTime.Now.Month : 12;
            List<Years_Months> meses = new List<Years_Months>();
            for (int i = mStart; i <= mEnd; i++)
            {
                Years_Months mes = new Years_Months();
                mes.Value = i;
                switch (i)
                {
                    case 1:
                        mes.Text = "Enero";
                        break;

                    case 2:
                        mes.Text = "Febrero";
                        break;

                    case 3:
                        mes.Text = "Marzo";
                        break;

                    case 4:
                        mes.Text = "Abril";
                        break;

                    case 5:
                        mes.Text = "Mayo";
                        break;

                    case 6:
                        mes.Text = "Junio";
                        break;

                    case 7:
                        mes.Text = "Julio";
                        break;

                    case 8:
                        mes.Text = "Agosto";
                        break;

                    case 9:
                        mes.Text = "Septiembre";
                        break;

                    case 10:
                        mes.Text = "Octubre";
                        break;

                    case 11:
                        mes.Text = "Noviembre";
                        break;

                    case 12:
                        mes.Text = "Diciembre";
                        break;
                }
                meses.Add(mes);
            }
            return Json(meses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipoUnidad()
        {
            List<TipoTaxi> tipos = new List<TipoTaxi>();
            TipoTaxi t1 = new TipoTaxi() { IdTipo = 1, Tipo = "[Seleccione uno]" };
            TipoTaxi t2 = new TipoTaxi() { IdTipo = 1, Tipo = "Microbús" };
            TipoTaxi t3 = new TipoTaxi() { IdTipo = 2, Tipo = "Taxi" };
            tipos.Add(t1);
            tipos.Add(t2);
            tipos.Add(t3);
            return Json(tipos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerAreas()
        {
            try
            {
                globalDAL = new GlobalDAL();
                var resultado = globalDAL.ObtenerAreas();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (Session["User"] as LogIn);
        }
    }
}