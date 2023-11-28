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
    [Autorizacion(ModuloUrl = new string[] { "CambiarEstado/CambiarEstado" })]
    public class CambiarEstadoController : Controller
    {
        CambiarEstadoDAL ceDAL;
        LogIn DatosUsuarios;
        
        // GET: CambiarEstado
        public ActionResult CambiarEstado()
        {
            return View();
        }

        public ActionResult ObtenerValesEstado([DataSourceRequest]DataSourceRequest request, int idVale, string numEquipo, string placa, string fechaInicio, string fechaFin, int idUsuario, int filtro)
        {
            try
            {
                ceDAL = new CambiarEstadoDAL();
                GetSession();
                var resultado = ceDAL.ObtenerValesEstado(idVale, numEquipo, placa, fechaInicio, fechaFin, idUsuario, filtro, DatosUsuarios.usertypeid, DatosUsuarios.userid);
                return Json(resultado.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerFiltros()
        {
            List<FiltrosReporteValesActualizados> filtros = new List<FiltrosReporteValesActualizados>();
            //"Número de vale", "Número de equipo", "Placa", "Fecha de generación", "Fecha de actualización", "Usuario"
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 1, Nombre = "Número de vale" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 2, Nombre = "Número de equipo" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 3, Nombre = "Placa" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 4, Nombre = "Fecha de generación" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 5, Nombre = "Fecha de actualización" });
            filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 7, Nombre = "Fecha de crédito fiscal" });

            GetSession();
            if (DatosUsuarios.usertypeid == 2) filtros.Add(new FiltrosReporteValesActualizados() { IdFiltro = 6, Nombre = "Usuario" });
            return Json(filtros, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEstados()
        {
            ceDAL = new CambiarEstadoDAL();
            var resultado = ceDAL.ObtenerEstados();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarEstados(int[] vales,int estadoId, string comentarioAnulado)
        {
            try
            {
                ceDAL = new CambiarEstadoDAL();
                int resultado = 0;
                string msj = "";
                int lvl = 0;
                GetSession();
                foreach (int vale in vales)
                {
                    resultado = ceDAL.ActualizarEstadoVale(vale, estadoId, DatosUsuarios.userid, DatosUsuarios.usertypeid, comentarioAnulado);
                }

                switch (resultado)
                {
                    case 1:
                        msj = "¡Estado(s) actualizado(s) exitosamente!";
                        lvl = 2;
                        break;

                    case -1:
                        msj = "Sistema: No es posible realizar el cambio de estado especificado.";
                        lvl = 4;
                        break;

                    case -2:
                        msj = "Su nivel de acceso no es suficiente para realizar el cambio de estado especificado.";
                        lvl = 4;
                        break;

                    case -3:
                        msj = "Para realizar este cambio de estado debe utilizar el apartado de corrección de vales.";
                        lvl = 3;
                        break;

                    default:
                        msj = "Ha ocurrido un error interno. Por favor comuníquese con soporte técnico.";
                        lvl = 4;
                        break;
                }

                return Json(new { respuesta = resultado, mensaje = msj, nivel = lvl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { respuesta = -4, mensaje = "Ha ocurrido un error interno. Por favor comuníquese con soporte técnico.<br>Error: " + e.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (Session["User"] as LogIn);
        }
    }
}