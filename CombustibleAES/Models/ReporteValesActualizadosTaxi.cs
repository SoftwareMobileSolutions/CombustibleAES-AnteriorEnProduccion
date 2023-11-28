using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class ReporteValesActualizadosTaxi
    {
        public int IdVale { get; set; }
        public string Area { get; set; }
        public string Empresa { get; set; }
        public string Pasajero { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string CentroCosto { get; set; }
        public string Orden { get; set; }
        public decimal Monto { get; set; }
        public string Usuario { get; set; }
    }

    public class ReporteValesActualizadosTaxiDAL
    {
        public IList<ReporteValesActualizadosTaxi> ObtenerValesTaxiCF(string fechaInicio, string fechaFin)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<ReporteValesActualizadosTaxi> vales;
            using (ISession sesion = nHB.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValesTaxiCF :fechaInicio, :fechaFin")
                .SetParameter("fechaInicio", fechaInicio)
                .SetParameter("fechaFin", fechaFin)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReporteValesActualizadosTaxi)))
                .List<ReporteValesActualizadosTaxi>();
            }
            return vales;
        }

        public int ActualizarCreditoFiscal(int idVale, string fechaCF, string CF)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ActualizarCFValeTaxi :idVale, :fechaCF, :CF")
                .SetParameter("idVale", idVale)
                .SetParameter("fechaCF", fechaCF)
                .SetParameter("CF", CF)
                .List<int>()[0];
            }
            return resultado;
        }
    }
}