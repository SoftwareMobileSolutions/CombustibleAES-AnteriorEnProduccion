using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class CambiarEstadoTaxi
    {
        public string CreditoFiscal { get; set; }
        public int NoVale { get; set; }
        public string Area { get; set; }
        public string Empresa { get; set; }
        public string Pasajero { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string CentroCosto { get; set; }
        public string OrdenEstadistica { get; set; }
        public string Sociedad { get; set; }
        public string Cuenta { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
        public string FechaGeneracion { get; set; }
        public string FechaCF { get; set; }
        public string UsuarioGeneracion { get; set; }
    }

    public class EstadosCambiarTaxi
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }

    public class CambiarEstadoTaxiDAL
    {
        public int CambiarEstadoValeTaxi(int valeId, int estadoId, int userId, int usertypeid, string comentarioAnulado)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int respuesta;
            using (ISession sesion = nHB.OpenSession())
            {
                respuesta = sesion.CreateSQLQuery("exec MVC_CambiarEstadoTaxi :valeId, :estadoId, :userId, :usertypeid, :comentarioAnulado")
                .SetParameter("valeId", valeId)
                .SetParameter("estadoId", estadoId)
                .SetParameter("userId", userId)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("comentarioAnulado", comentarioAnulado)
                .List<int>()[0];
            }
            return respuesta;
        }

        public IList<CambiarEstadoTaxi> ObtenerValesEstadoTaxi(int userid, int usertypeid, string strFechaInicio, string strFechaFin, int numVale, string creditoFiscal, int filtro)
        {
            IList<CambiarEstadoTaxi> resultado;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ObtenerValesEstadoTaxi :userid, :usertypeid, :FechaIni, :FechaFin, :numVale, :creditoFiscal, :filtro")
                .SetParameter("userid", userid)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("FechaIni", strFechaInicio)
                .SetParameter("FechaFin", strFechaFin)
                .SetParameter("numVale", numVale)
                .SetParameter("creditoFiscal", creditoFiscal)
                .SetParameter("filtro", filtro)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CambiarEstadoTaxi)))
                .List<CambiarEstadoTaxi>();
            }
            return resultado;
        }

        //[MVC_ObtenerEstadosCambiarTaxi]
        public IList<EstadosCambiarTaxi> ObtenerEstadosCambiarTaxi()
        {
            IList<EstadosCambiarTaxi> resultado;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ObtenerEstadosCambiarTaxi")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(EstadosCambiarTaxi)))
                .List<EstadosCambiarTaxi>();
            }
            return resultado;
        }
    }
}