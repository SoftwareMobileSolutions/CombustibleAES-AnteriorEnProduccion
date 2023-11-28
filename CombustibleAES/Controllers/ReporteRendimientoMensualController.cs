using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CombustibleAES.Models;
using FLEETSMVC.Util;
using System.IO;
//Lectura de archivos excel
//using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
//Kendo Grid
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ClosedXML.Excel;


namespace CombustibleAES.Controllers
{
    [Autorizacion(ModuloUrl = new string[] { "ReporteRendimientoMensual/ReporteRendimientoMensual" })]
    public class ReporteRendimientoMensualController : Controller
    {
        List<ReporteRendimientoMensual> rendimientoMensual;
        LogIn DatosUsuarios;
        PermissionDAL ListPermisosDAL;
        public ReporteRendimientoMensualController()
        {
            DatosUsuarios = new LogIn();
            ListPermisosDAL = new PermissionDAL();
        }

        // GET: RendimientoMensual
        public ActionResult ReporteRendimientoMensual()
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

        public void LeerExcel()
        {
            /*
            Excel.Application appExcel;
            Excel.Workbook libroExcel;
            Excel._Worksheet hojaDelLibro;
            Excel.Range rangoDeHoja;
            */
            try
            {
                LogIn Usuario = Session["User"] as LogIn;
                string path = Server.MapPath("~/App_Data");
                IEnumerable<string> rutas = Directory.GetFiles(path);
                rutas = rutas.Where(x => x.Split(new[] { "\\App_Data\\" }, StringSplitOptions.None)[1].Split(new[] { "_" }, StringSplitOptions.None)[0] == Usuario.userid.ToString());

                for (int x = 0; x < rutas.Count(); x++)
                {
                    /*
                    appExcel = new Excel.Application();
                    libroExcel = appExcel.Workbooks.Open(Server.MapPath("~/App_Data") + "/" + rutas.ElementAt(x));
                    hojaDelLibro = libroExcel.Worksheets[1]; //No se comienza desde 0
                    rangoDeHoja = hojaDelLibro.UsedRange;

                    int cantColumnas = rangoDeHoja.Columns.Count;
                    int cantFilas = rangoDeHoja.Rows.Count;
                    //Indice de las columnas a utilizar
                    int col1 = 0, col2 = 0, col3 = 0;

                    int cKilometraje = 0;
                    int cUtilizacion = 0;

                    for (int i = 1; i <= rangoDeHoja.Columns.Count; i++)
                    {
                        string tempColValue = hojaDelLibro.Cells[1, i].value2.ToLower();

                        switch (tempColValue)
                        {
                            case "asignación": case "area": case "vehículo": case "hora real mtd":
                            case "hora meta mtd": case "utilización mtd (%)": case "real anual ytd":
                            case "meta anual ytd": case "utilización ytd (%)": case "estatus actual":
                                cUtilizacion++;
                            break;
                            
                            case "mes": case "id": case "flota": case "nombre":
                            case "placa": case "descr": case "km total":
                                cKilometraje++;
                            break;

                            case "empresa":
                                if (cKilometraje == 2)
                                {
                                    cKilometraje++;
                                }
                                else if (cUtilizacion == 1)
                                {
                                    cUtilizacion++;
                                }
                                break;

                            case "tipo":
                                if (cKilometraje == 6)
                                {
                                    cKilometraje++;
                                }
                                else if (cUtilizacion == 4)
                                {
                                    cUtilizacion++;
                                }
                                break;
                        }

                        if (tempColValue == "area")
                        {
                            col1 = i;
                        }
                        else if (tempColValue == "vehículo")
                        {
                            col2 = i;
                        }
                        else if (tempColValue == "utilización mtd (%)")
                        {
                            col3 = i;
                        }
                        else if (tempColValue == "nombre")
                        {
                            col1 = i;
                        }
                        else if (tempColValue == "km total")
                        {
                            col2 = i;
                        }
                    }

                    //se evalua el valor de la variable "tipoArchivo"
                    if (cUtilizacion == 12) // % de Utilización (Cantidad de columnas con nombres especificos)
                    {
                        //Se almacenan los datos del libro de excel
                        for (int i = 2; i <= cantFilas; i++)
                        {
                            if (hojaDelLibro.Cells[i, col1].value != null && hojaDelLibro.Cells[i, col1].value2 != null && hojaDelLibro.Cells[i, col2].value != null && hojaDelLibro.Cells[i, col2].value2 != null && hojaDelLibro.Cells[i, col3].value != null && hojaDelLibro.Cells[i, col3].value2 != null)
                            {
                                string area = hojaDelLibro.Cells[i, col1].value2.ToString();
                                string equipo = hojaDelLibro.Cells[i, col2].value2.ToString();
                                string utilizacion = (hojaDelLibro.Cells[i, col3].value2 * 100).ToString() + "%";
                                foreach(ReporteRendimientoMensual rm in rendimientoMensual)
                                {
                                    if (rm.Equipo == equipo)
                                    {
                                        rm.Area = area;
                                        rm.Utilizacion = utilizacion;
                                    }
                                }
                            }                            
                        }
                    }
                    else if (cKilometraje == 9) //Kilometraje (Cantidad de columnas con nombres especificos)
                    {
                        //Se almacenan los datos del libro de excel
                        for (int i = 2; i <= cantFilas; i++)
                        {
                            if (hojaDelLibro.Cells[i, col1].value != null && hojaDelLibro.Cells[i, col1].value2 != null && hojaDelLibro.Cells[i, col2].value != null && hojaDelLibro.Cells[i, col2].value2 != null)
                            {
                                string equipo = hojaDelLibro.Cells[i, col1].value2.ToString();
                                string km = hojaDelLibro.Cells[i, col2].value2.ToString();
                                foreach (ReporteRendimientoMensual rm in rendimientoMensual)
                                {
                                    if (rm.Equipo == equipo)
                                    {
                                        rm.KilometrosRecorridos = decimal.Parse(km, System.Globalization.CultureInfo.InvariantCulture);
                                        rm.RendimientoKM_Galon = Convert.ToDecimal(Convert.ToDouble(km) / Convert.ToDouble(rm.GalonesConsumidos));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //Se libera la memoria no utilizada y se finalizan subprocesos
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //Se libera memoria de los objetos COM para poder finalizar completamente el proceso de Excel
                    Marshal.ReleaseComObject(rangoDeHoja);
                    Marshal.ReleaseComObject(hojaDelLibro);

                    //Se cierra el libro de excel(archivo) y se libera la memoria que estaba en uso
                    libroExcel.Close();
                    Marshal.ReleaseComObject(libroExcel);

                    //Se cierra el proceso de excel y se libera la memoria que estaba en uso
                    appExcel.Quit();
                    Marshal.ReleaseComObject(appExcel);
                    */

                    using (var libroExcel = new XLWorkbook(Server.MapPath("~/App_Data") + "/" + rutas.ElementAt(x)))
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

                        //Kilometraje
                        string[] mes = null;
                        string[] id = null;
                        string[] flota = null;
                        string[] nombre = null;
                        string[] placa = null;
                        string[] descripcion = null;
                        string[] kmTotal = null;

                        //Ambos
                        string[] empresa = null;
                        string[] tipo = null;

                        //% Utilizacion
                        string[] asignacion = null;
                        string[] area = null;
                        string[] vehiculo = null;
                        string[] horaRealMTD = null;//Hora Real MTD
                        string[] horaMetaMTD = null;//Hora Meta MTD
                        string[] utilizacionMTD = null; //Utilización MTD (%)
                        string[] realAnualYTD = null;//Real Anual YTD
                        string[] metaAnualYTD = null;//Meta anual YTD
                        string[] utilizacionYTD = null;//Utilización YTD (%)
                        string[] estatusActual = null;//Estatus actual

                        int tipoArchivo = 0;

                        try
                        {
                            mes = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("MES").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e){}
                        try
                        {
                            id = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("ID").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            empresa = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("EMPRESA").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            flota = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("FLOTA").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            nombre = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("NOMBRE").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            placa = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("PLACA").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            tipo = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("TIPO").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            descripcion = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("DESCR").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            kmTotal = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("KM TOTAL").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            asignacion = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Asignación").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            area = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Area").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            vehiculo = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Vehículo").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            horaRealMTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Hora Real MTD").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            horaMetaMTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Hora Meta MTD").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            utilizacionMTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Utilización MTD (%)").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            realAnualYTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Real Anual YTD").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            metaAnualYTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Meta Anual YTD").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            utilizacionYTD = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Utilización YTD (%)").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        try
                        {
                            estatusActual = tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Estatus actual").GetString()).ToArray();
                            tipoArchivo++;
                        }
                        catch (Exception e) { }
                        /*
                        var listaData = new List<string[]>
                        {
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("MES").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("ID").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("EMPRESA").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("FLOTA").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("NOMBRE").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("PLACA").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("TIPO").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("DESCR").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("KM TOTAL").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Asignación").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Empresa").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Area").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("Vehículo").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("TIPO").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray(),
                            tabla.DataRange.Rows()
                            .Select(fila => fila.Field("").GetString()).ToArray()
                        };
                        */
                        if (tipoArchivo == 9)
                        {
                            int cantRegistros = mes.Length;
                            //Se almacenan los datos del libro de excel
                            for (int i = 0; i <= cantRegistros; i++)
                            {
                                foreach (ReporteRendimientoMensual rm in rendimientoMensual)
                                {
                                    if (rm.Equipo == nombre[i])
                                    {
                                        rm.KilometrosRecorridos = decimal.Parse(kmTotal[i], System.Globalization.CultureInfo.InvariantCulture);
                                        rm.RendimientoKM_Galon = Convert.ToDecimal(Convert.ToDouble(kmTotal[i]) / Convert.ToDouble(rm.GalonesConsumidos));
                                        break;
                                    }
                                }
                            }
                        }
                        else if (tipoArchivo == 12)
                        {
                            int cantRegistros = asignacion.Length;
                            //Se almacenan los datos del libro de excel
                            for (int i = 0; i <= cantRegistros; i++)
                            {
                                foreach (ReporteRendimientoMensual rm in rendimientoMensual)
                                {
                                    if (rm.Equipo == vehiculo[i])
                                    {
                                        rm.Area = area[i];
                                        rm.Utilizacion = utilizacionMTD[i] + "%";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CerrarProcesosExcel(appExcel, libroExcel, hojaDelLibro, rangoDeHoja);
            }
        } 

        public ActionResult GuardarExcel(IEnumerable<HttpPostedFileBase> excels)
        {
            if (excels != null)
            {
                foreach (var file in excels)
                {
                    var name = Path.GetFileName(file.FileName);
                    GetSession();
                    name = DatosUsuarios.userid + "_" + name;
                    var realPath = Path.Combine(Server.MapPath("~/App_Data"), name);

                    //Se guarda una copia del archivo en la ruta ~/App_Data/archivo
                    file.SaveAs(@realPath);
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult BorrarExcels()
        {
            GetSession();
            try
            {
                string path = Server.MapPath("~/App_Data");
                IEnumerable<string> archivos = Directory.GetFiles(path);
                archivos = archivos.Where(x => x.Split(new[] { "\\App_Data\\" }, StringSplitOptions.None)[1].Split(new[] { "_" }, StringSplitOptions.None)[0] == DatosUsuarios.userid.ToString());
                int cFiles = archivos.Count();
                for (int i = 0; i < cFiles; i++)
                {
                    System.IO.File.Delete(archivos.ElementAt(i));
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerRendimientoMensual([DataSourceRequest] DataSourceRequest request, int year, int month, int token)
        {
            try
            {
                RendimientoMensualDAL rmDAL = new RendimientoMensualDAL();
                if (token > 0)
                {
                    rendimientoMensual = rmDAL.ObtenerDatosRendimientoMensual(month, year).ToList<ReporteRendimientoMensual>();
                    rendimientoMensual = rendimientoMensual.GroupBy(x => x.Equipo).Select(rl => new ReporteRendimientoMensual
                    {
                        DistribucionArea = rl.First().DistribucionArea,
                        Empresa = rl.First().Empresa,
                        IMes = rl.First().IMes,
                        Mes = rl.First().Mes,
                        Area = rl.First().Area,
                        EstadoVehiculo = rl.First().EstadoVehiculo,
                        TipoVehiculo = rl.First().TipoVehiculo,
                        Equipo = rl.First().Equipo,
                        KilometrosRecorridos = rl.First().KilometrosRecorridos,
                        GalonesConsumidos = rl.Sum(s => s.GalonesConsumidos),
                        MontoPagado = rl.Sum(s => s.MontoPagado),
                        RendimientoKM_Galon = rl.First().RendimientoKM_Galon,
                        Utilizacion = rl.First().Utilizacion,
                        CostoPorGalon = rl.Sum(s => s.CostoPorGalon),
                        CantRegistros = rl.Count()
                    }).ToList<ReporteRendimientoMensual>();
                    LeerExcel();
                    foreach (ReporteRendimientoMensual r in rendimientoMensual)
                    {
                        int status = rmDAL.IngresarRendimiento(r, month, year);
                    }
                }
                rendimientoMensual = rmDAL.ObtenerRendimientoMensual(month, year).ToList<ReporteRendimientoMensual>();

                return Json(rendimientoMensual.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        public ActionResult ObtenerEstadisticas(int year, int month, int filtro)
        {
            try
            {
                RendimientoMensualDAL rDAL = new RendimientoMensualDAL();
                var resultado = rDAL.ObtenerEstadisticas(year, month, filtro);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerTipofiltros()
        {
            List<TipoFiltro> filtro = new List<TipoFiltro>();
            filtro.Add(new TipoFiltro() { Id = 1, Nombre = "Empresa Combustible" });
            filtro.Add(new TipoFiltro() { Id = 2, Nombre = "CC Combustible" });
            filtro.Add(new TipoFiltro() { Id = 3, Nombre = "Estación de servicio" });
            filtro.Add(new TipoFiltro() { Id = 4, Nombre = "Empleado Combustible" });

            return Json(filtro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerTipoGrid()
        {
            List<TipoFiltro> filtro = new List<TipoFiltro>();
            filtro.Add(new TipoFiltro() { Id = 1, Nombre = "Vista Normal" });
            filtro.Add(new TipoFiltro() { Id = 2, Nombre = "Flota & Compañía" });
            return Json(filtro, JsonRequestBehavior.AllowGet);
        }
    }
}
 