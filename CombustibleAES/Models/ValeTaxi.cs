using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FLEETSMVC;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class ValeTaxi
    {
        int idTipoUnidad;

        public int NoVale { get; set; }
        public int IdEstado { get; set; }
        //Primeros Datos
        public string Empresa { get; set; }
        public int IdEmpresa { get; set; }
        public string Pasajero { get; set; }
        public string Desde { get; set; }
        public string FechaGeneracion { get; set; }
        public string TipoUnidad { get; set; }
        public int IdTipoUnidad { get { return idTipoUnidad; } set { idTipoUnidad = value; TipoUnidad = value == 1 ? "Microbús" : "Taxi"; } }
        public string Area { get; set; }
        public int IdArea { get; set; }
        public string CentroCosto { get; set; }
        public string OrdenEstadistica { get; set; }
        //Segundos datos
        public string Destino1 { get; set; }
        public string Destino2 { get; set; }
        public string Destino3 { get; set; }
        public string FechaSalida { get; set; } //fecha y hora
        public string FechaLlegada { get; set; } //fecha y hora
        public string Conductor { get; set; }
        public decimal MontoVale { get; set; }
        public string NoVehiculo { get; set; }
    }

    public class ValeTaxiImprimir
    {
        public int idTipoUnidad;
        public string NoVale { get; set; }
        public string Empresa { get; set; }
        public string Pasajero { get; set; }
        public string Desde { get; set; }
        public string FechaGeneracion { get; set; }
        public int IdTipoUnidad { get { return idTipoUnidad; } set { idTipoUnidad = value;  TipoUnidad = value == 1 ? "Microbús" : "Taxi"; } }
        public string TipoUnidad { get; set; }
        public string Area { get; set; }
        public string CentroCosto { get; set; }
        public string OrdenEstadistica { get; set; }
        public string FechaVencimiento { get; set; }
    }

    public class ValeTaxiDAL
    {
        public IList<ValeTaxi> ObtenerValeTaxi(int IdVale)
        {
            IList<ValeTaxi> vales;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using(ISession sesion = nHB.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValeTaxi :idVale")
                .SetParameter("idVale", IdVale)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeTaxi)))
                .List<ValeTaxi>();
            }
            return vales;
        }

        public int InsertarValeTaxi(string IdEmpresa, string Pasajero, string Desde, int IdTipoUnidad, int IdArea, string CentroCosto, string OrdenEstadistica, int userid)
        {
            int result = 0;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using (ISession sesion = nHB.OpenSession())
            {
                result = sesion.CreateSQLQuery("exec MVC_CTInsertarValeTaxi :idEmpresa, :pasajero, :desde, :idArea, :centroCosto, :ordenEstadistica, :idTipoUnidad, :userid")
                .SetParameter("idEmpresa", IdEmpresa)
                .SetParameter("pasajero", Pasajero)
                .SetParameter("desde", Desde)
                .SetParameter("idArea", IdArea)
                .SetParameter("centroCosto", CentroCosto)
                .SetParameter("ordenEstadistica", OrdenEstadistica)
                .SetParameter("idTipoUnidad", IdTipoUnidad)
                .SetParameter("userid", userid)
                .List<int>()[0];
            }
            return result;
        }

        public IList<ValeTaxiImprimir> ObtenerValeTaxiImprimir(int idvale)
        {
            IList<ValeTaxiImprimir> vales;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using (ISession sesion = nHB.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValeTaxiImprimir :idvale")
                .SetParameter("idvale", idvale)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeTaxiImprimir)))
                .List<ValeTaxiImprimir>();
            }
            return vales;
        }

        public int LiquidarValeTaxi(string desde, int IdVale, string Destino1, string Destino2, string Destino3, string FechaSalida, string FechaLlegada, string NoVehiculo, string Conductor, double MontoVale)
        {
            int resultado = -1;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using(ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_LiquidarValeTaxi :desde, :IdVale, :Destino1, :Destino2, :Destino3, :FechaSalida, :FechaLlegada, :NoVehiculo, :Conductor, :MontoVale")
                .SetParameter("desde", desde)
                .SetParameter("IdVale", IdVale)
                .SetParameter("Destino1", Destino1)
                .SetParameter("Destino2", Destino2)
                .SetParameter("Destino3", Destino3)
                .SetParameter("FechaSalida", FechaSalida)
                .SetParameter("FechaLlegada", FechaLlegada)
                .SetParameter("NoVehiculo", NoVehiculo)
                .SetParameter("Conductor", Conductor)
                .SetParameter("MontoVale", MontoVale)
                .List<int>()[0];
            }
            return resultado;
        }

        public IList<ValeTaxi> ObtenerValeTaxiLiquidado(int IdVale)
        {
            IList<ValeTaxi> vale;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using (ISession sesion = nHB.OpenSession())
            {
                vale = sesion.CreateSQLQuery("exec MVC_ObtenerValeTaxiLiquidado :IdVale")
                .SetParameter("IdVale", IdVale)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeTaxi)))
                .List<ValeTaxi>();
            }
            return vale;
        }
    }
}