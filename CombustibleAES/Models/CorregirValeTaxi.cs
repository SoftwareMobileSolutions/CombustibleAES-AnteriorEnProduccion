using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Transform;
using CombustibleVales.Models.NHibernate.Connections;

namespace CombustibleAES.Models
{
    public class CorregirValeTaxi
    {
        int idTipoUnidad;

        //public bool EnableAnular { get; set; }
        public int UserTypeId { get; set; }

        public int NoVale { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string CreditoFiscal { get; set; }
        public string ComentarioAnulado { get; set; }
        public string FechaCredito { get; set; }
        public string FechaEstado { get; set; }
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

    public class EstadosValeTaxi
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }

    public class EstadosValeTaxiDAL{
        public IList<EstadosValeTaxi> ObtenerEstadosValesTaxi(int usertypeid, int estadoId)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<EstadosValeTaxi> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerEstadosValesTaxi :usertypeid, :estadoid")
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("estadoid", estadoId)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(EstadosValeTaxi)))
                .List<EstadosValeTaxi>();
            }
            return resultado;
        }
    }

    public class CorregirValeTaxiDAL
    {
        public IList<CorregirValeTaxi> ObtenerValeTaxiCorregir(int idVale, int usertypeid, int userid)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<CorregirValeTaxi> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerValeTaxicorregir :idVale, :usertypeid, :userid")
                .SetParameter("idVale", idVale)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("userid", userid)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CorregirValeTaxi)))
                .List<CorregirValeTaxi>();
            }
            return resultado;
        }

        public int CorregirValeTaxi(int idVale, int idEstado, string hacia, string hacia2, string hacia3, string fechaSalida, string fechaLlegada, string conductor, double monto, string noVehiculo, string comentarioAnulado, string creditoFiscal, string fechaCredito, int userid, string fechaEstado, int usertypeid)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CorregirValeTaxi :idVale, :idEstado, :hacia, :hacia2, :hacia3, :fechaSalida, :fechaLlegada, :conductor, :monto, :noVehiculo, :comentarioAnulado, :cf, :fechaCredito, :userid, :fechaEstado, :usertypeid")
                .SetParameter("idVale", idVale)
                .SetParameter("idEstado", idEstado)
                .SetParameter("hacia", hacia)
                .SetParameter("hacia2", hacia2)
                .SetParameter("hacia3", hacia3)
                .SetParameter("fechaSalida", fechaSalida)
                .SetParameter("fechaLlegada", fechaLlegada)
                .SetParameter("conductor", conductor)
                .SetParameter("monto", monto)
                .SetParameter("noVehiculo", noVehiculo)
                .SetParameter("comentarioAnulado", comentarioAnulado)
                .SetParameter("cf", creditoFiscal)
                .SetParameter("fechaCredito", fechaCredito)
                .SetParameter("userid", userid)
                .SetParameter("fechaEstado", fechaEstado)
                .SetParameter("usertypeid", usertypeid)
                .List<int>()[0];
            }
            return resultado;
        }
    }
}