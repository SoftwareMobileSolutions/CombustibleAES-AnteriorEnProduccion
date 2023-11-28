using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class ReporteRendimientoMensual
    {
        private int iMes;

        public string DistribucionArea { get; set; }
        public string Empresa { get; set; }
        public int IMes { get { return iMes; } set { iMes = value; Mes = iMes == 1 ? "Enero" : (iMes == 2 ? "Febrero" : (iMes == 3 ? "Marzo" : (iMes == 4 ? "Abril" : (iMes == 5 ? "Mayo" : (iMes == 6 ? "Junio" : (iMes == 7 ? "Julio" : (iMes == 8 ? "Agosto" : (iMes == 9 ? "Septiembre" : (iMes == 10 ? "Octubre" : (iMes == 11 ? "Noviembre" : "Diciembre")))))))))); } }
        public string Mes { get; set; }
        public string Area { get; set; }
        public string EstadoVehiculo { get; set; } //Estatus
        public string TipoVehiculo { get; set; }
        public string Equipo { get; set; }
        public decimal KilometrosRecorridos { get; set; }
        public decimal GalonesConsumidos { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal RendimientoKM_Galon { get; set; }
        public string Utilizacion { get; set; } //En porcentaje
        public decimal CostoPorGalon { get; set; } //Se debe sacar la media
        public int CantRegistros { get; set; }
        public int year { get; set; }
    }

    public class GraficaRendimientoMensual
    {
        public string X { get; set; }
        public decimal Y { get; set; }
    }

    public class TipoFiltro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class RendimientoMensualDAL
    {
        public IList<ReporteRendimientoMensual> ObtenerDatosRendimientoMensual(int mes, int year)
        {
            IList<ReporteRendimientoMensual> resultado;
            NHibernateLoginSession nHSession = new NHibernateLoginSession();
            string cadena = "exec MVC_ObtenerDatosRendimientoMensual " + mes + "," + year;
            using (ISession sesion = nHSession.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerDatosRendimientoMensual :mes, :year")
                .SetParameter("mes", mes)
                .SetParameter("year", year)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReporteRendimientoMensual)))
                .List<ReporteRendimientoMensual>();
            }
            return resultado;
        }

        public int IngresarRendimiento(ReporteRendimientoMensual registro, int mes, int year)
        {
            NHibernateLoginSession nHSession = new NHibernateLoginSession();
            int status;
            using (ISession sesion = nHSession.OpenSession())
            {
                status = sesion.CreateSQLQuery("exec MVC_IngresarRendimientoMensual :mobile, :km, :galonesConsumidos, :montoPagado, :area, :utilizacion, :costoPorGalon, :rendimientoKM_Galon, :mes, :year")
                .SetParameter("mobile", registro.Equipo)
                .SetParameter("km", registro.KilometrosRecorridos)
                .SetParameter("galonesConsumidos", registro.GalonesConsumidos)
                .SetParameter("montoPagado", registro.MontoPagado)
                .SetParameter("area", registro.Area)
                .SetParameter("utilizacion", registro.Utilizacion)
                .SetParameter("costoPorGalon", registro.CostoPorGalon)
                .SetParameter("rendimientoKM_Galon", registro.RendimientoKM_Galon)
                .SetParameter("mes", mes)
                .SetParameter("year", year)
                .List<int>()[0];
            }
            return status;
        }

        public IList<ReporteRendimientoMensual> ObtenerRendimientoMensual(int mes, int year)
        {
            IList<ReporteRendimientoMensual> resultado;
            NHibernateLoginSession nHSession = new NHibernateLoginSession();
            string cadena = "exec MVC_ObtenerRendimientoMensual " + mes + "," + year;
            using (ISession sesion = nHSession.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerRendimientoMensual :mes, :year")
                .SetParameter("mes", mes)
                .SetParameter("year", year)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReporteRendimientoMensual)))
                .List<ReporteRendimientoMensual>();
            }
            return resultado;
        }

        public IList<GraficaRendimientoMensual> ObtenerEstadisticas(int year, int month, int filtro)
        {
            IList<GraficaRendimientoMensual> resultado;
            NHibernateLoginSession nHSession = new NHibernateLoginSession();
            using (ISession sesion = nHSession.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerEstadisticas :filtro, :mes, :year")
                .SetParameter("filtro", filtro)
                .SetParameter("mes", month)
                .SetParameter("year", year)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(GraficaRendimientoMensual)))
                .List<GraficaRendimientoMensual>();
            }
            return resultado;
        }
    }
}