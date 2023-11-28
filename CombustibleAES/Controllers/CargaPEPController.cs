using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FLEETSMVC.Util;
using CombustibleAES.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ClosedXML.Excel;

namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "CargaPEP/CargaPEP" })]
    public class CargaPEPController : Controller
    {
        List<PEPs> peps;
        LogIn DatosUsuarios;
        PEPsDAL pDAL;
        string lastPath = "";

        // GET: CargaPEP
        public ActionResult CargaPEP()
        {
            return View();
        }

        public int LeerExcel(string path)
        {
            int count = 0;
            peps = new List<PEPs>();
            //ex = new ArchivoExcel();
            int resultado = 0;
            
            try
            {
                /*
                LogIn Usuario = Session["User"] as LogIn;
                string path = Server.MapPath("~/App_Data");
                IEnumerable<string> rutas = Directory.GetFiles(path);
                rutas = rutas.Where(x => x.Split(new[] { "\\App_Data\\" }, StringSplitOptions.None)[1].Split(new[] { "_" }, StringSplitOptions.None)[0] == Usuario.userid.ToString());
                */
                /*
                for (int x = 0; x < rutas.Count(); x++)
                {*//*
                    ex.appExcel = new Excel.Application();
                    ex.libroExcel = ex.appExcel.Workbooks.Open(Server.MapPath("~/App_Data") + "/" + rutas.ElementAt(x)path);
                    ex.hojaDelLibro = ex.libroExcel.Worksheets[1]; //No se comienza desde 0
                    ex.rangoDeHoja = ex.hojaDelLibro.UsedRange;

                    int cantColumnas = ex.rangoDeHoja.Columns.Count;
                    int cantFilas = ex.rangoDeHoja.Rows.Count;

                    int i1 = 0, i2 = 0, i3 = 0, i4 = 0;
                    int columnasValidas = 0;

                    for (int y = 1; y <= cantColumnas; y++)
                    {
                        string _n = "";
                        if (ex.hojaDelLibro.Cells[1, y].value2 != null)
                        {
                            _n = ex.hojaDelLibro.Cells[1, y].value2.ToString();
                        }
                        else if (ex.hojaDelLibro.Cells[2, y].value2 != null)
                        {
                            _n = ex.hojaDelLibro.Cells[2, y].value2.ToString();
                        }

                        switch (_n)
                        {
                            case "PEP":
                                columnasValidas++;
                                i1 = y;
                                break;

                            case "Nombre del Proyecto":
                                columnasValidas++;
                                i2 = y;
                                break;

                            case "Responsable":
                                columnasValidas++;
                                i3 = y;
                                break;

                            case "Estado":
                                columnasValidas++;
                                i4 = y;
                                break;
                        }
                    }

                    if (columnasValidas == 4)
                    {
                        for (int i = 2; i <= cantFilas; i++)
                        {
                            PEPs pep = new PEPs();


                            if (ex.hojaDelLibro.Cells[i, i1].value2 != null && ex.hojaDelLibro.Cells[i, i2].value2 != null && ex.hojaDelLibro.Cells[i, i4].value2 != null)
                            {
                                string _pep = ex.hojaDelLibro.Cells[i, i1].value2.ToString();
                                string _nombre = ex.hojaDelLibro.Cells[i, i2].value2.ToString();
                                string _responsable = ex.hojaDelLibro.Cells[i, i3].value2 == null ? null : ex.hojaDelLibro.Cells[i, i3].value2.ToString();
                                string _estado = ex.hojaDelLibro.Cells[i, i4].value2.ToString();

                                if (_pep != "PEP" && _nombre != "Nombre del Proyecto" && _responsable != "Responsable" && _estado != "Estado")
                                {
                                    count++;
                                    pep.PEP = _pep;
                                    pep.NombreProyecto = _nombre;
                                    pep.Responsabe = _responsable;
                                    pep.Estado = _estado;
                                    peps.Add(pep);
                                }
                            }
                        }
                    }
                    else resultado = -1;

                    ex.FinalizarProcesosExcel();*/
                //}

                using (var libroExcel = new XLWorkbook(path))
                {
                    var hojaExcel = libroExcel.Worksheet(1);
                    var primeraFilaUsada = hojaExcel.FirstRowUsed();
                    var primeraPosibleUbicacion = hojaExcel.Row(primeraFilaUsada.RowNumber()).FirstCell().Address;
                    var ultimaPosibleUbicacion = hojaExcel.LastCellUsed().Address;

                    //Se obtiene el rango de filas y columnas usadas en el archivo
                    var rango = hojaExcel.Range(primeraPosibleUbicacion, ultimaPosibleUbicacion).AsRange();
                    //Se convierte el rango de datos en una tabla
                    var tabla = rango.AsTable();

                    //Se puede especificar cuáles son las colmnas que se quieren de todo el excel
                    //y luego se convierten en lista
                    var listaData = new List<string[]>
                    {
                        tabla.DataRange.Rows()
                        .Select(fila => fila.Field("PEP").GetString()).ToArray(),
                        tabla.DataRange.Rows()
                        .Select(fila => fila.Field("Nombre del Proyecto").GetString()).ToArray(),
                        tabla.DataRange.Rows()
                        .Select(fila => fila.Field("Responsable").GetString()).ToArray(),
                        tabla.DataRange.Rows()
                        .Select(fila => fila.Field("Estado").GetString()).ToArray()
                    };


                    if (listaData.Count == 4)
                    {
                         int cantRegistros = listaData[0].Length;
                         for (int i = 0; i < cantRegistros; i++)
                         {
                             resultado++;
                             PEPs pep = new PEPs();
                             pep.PEP = listaData[0][i];
                             pep.NombreProyecto = listaData[1][i];
                             pep.Responsabe = listaData[2][i];
                             pep.Estado = listaData[3][i];
                             peps.Add(pep);
                         }
                    }
                    else
                    {
                        resultado = -1;
                    }
                }
                return resultado;
            }
            catch (Exception e)
            {
                BorrarExcels(lastPath);
                return 0;
            }
        }

        public JsonResult BorrarExcels(string path)
        {
            GetSession();
            try
            {
                /*
                string path = Server.MapPath("~/App_Data");
                IEnumerable<string> archivos = Directory.GetFiles(path);
                archivos = archivos.Where(x => x.Split(new[] { "\\App_Data\\" }, StringSplitOptions.None)[1].Split(new[] { "_" }, StringSplitOptions.None)[0] == DatosUsuarios.userid.ToString());
                int cFiles = archivos.Count();
                for (int i = 0; i < cFiles; i++)
                {*/
                    System.IO.File.Delete(/*archivos.ElementAt(i)*/path);
                //}
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarExcel(IEnumerable<HttpPostedFileBase> excels)
        {
            try
            {
                if (excels != null)
                {
                    /*foreach (var file in excels)
                    {*/
                    var file = excels.ElementAt(0);
                    var name = Path.GetFileName(file.FileName);
                    GetSession();
                    name = DatosUsuarios.userid + "_" + name;
                    lastPath = Path.Combine(Server.MapPath("~/App_Data"), name);

                    //Se guarda una copia del archivo en la ruta ~/App_Data/archivo
                    file.SaveAs(@lastPath);
                    //}

                    int resultado = LeerExcel(lastPath);
                    BorrarExcels(lastPath);
                    if (resultado == -1)
                    {
                        return Json(new { codigo = -1, mensaje = "Por favor ingrese un archivo de excel con formato válido.", nivel = 4 }, JsonRequestBehavior.AllowGet);
                    }
                    else return Json(new { codigo = 1, PEPs = peps }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                //return Json(new { codigo = -2, mensaje = "Error interno: " + e.Message, nivel = 4 }, JsonRequestBehavior.AllowGet);
                return Json(new { codigo = -2, mensaje = "Ha ocurrido un error interno. Por favor contacte a soporte técnico. Error: " + e.Message + " - " + e.StackTrace, nivel = 4 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InsertarPeps(List<PEPs> lpeps)
        {
            try
            {
                int cantPeps = lpeps.Count;
                int i = 0;
                pDAL = new PEPsDAL();
                for (i = 0; i < cantPeps; i++)
                {
                    IList<PEPs> pps = pDAL.InsertarPEP(lpeps[i].PEP, lpeps[i].NombreProyecto, lpeps[i].Responsabe, lpeps[i].Estado);
                }
                return Json(lpeps, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerListaPeps([DataSourceRequest]DataSourceRequest request, List<PEPs> peps)
        {
            try
            {
                pDAL = new PEPsDAL();
                return Json(peps.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        private void GetSession()
        {
            DatosUsuarios = (Session["User"] as LogIn);
        }
    }
}