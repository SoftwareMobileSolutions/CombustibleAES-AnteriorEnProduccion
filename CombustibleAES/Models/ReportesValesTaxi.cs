using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Transform;
using CombustibleVales.Models.NHibernate.Connections;

namespace CombustibleAES.Models
{
    public class ReportesValesTaxi
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
        public string FechaGeneracion { get; set; }
        public string FechaCF { get; set; }
        public string UsuarioGeneracion { get; set; }
    }

    public class ReportesValesTaxiDAL
    {
        public IList<ReportesValesTaxi> ObtenerReporteGeneralTaxi(int userid, int usertypeid, string strFechaInicio, string strFechaFin, int numVale, string creditoFiscal, int filtro)
        {
            IList<ReportesValesTaxi> resultado;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ObtenerReporteGeneralValesTaxi :userid, :usertypeid, :FechaIni, :FechaFin, :numVale, :creditoFiscal, :filtro")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeReporte)))
                .SetParameter("userid", userid)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("FechaIni", strFechaInicio)
                .SetParameter("FechaFin", strFechaFin)
                .SetParameter("numVale", numVale)
                .SetParameter("creditoFiscal", creditoFiscal)
                .SetParameter("filtro", filtro)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReportesValesTaxi)))
                .List<ReportesValesTaxi>();
            }
            return resultado;
        }
    }
}