using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Transform;
using CombustibleVales.Models.NHibernate.Connections;

namespace CombustibleAES.Models
{
    public class ReporteValesActualizados
    {
        public int NumeroVale { get; set; }
        public string SubFlota { get; set; }
        public string Alias { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string Usuario { get; set; }
        public string FechaGeneracion { get; set; }
        public string FechaActualizacion { get; set; }
        public string NoEmpleado { get; set; }
        public string TipoCombustible { get; set; }
        public string CentroCosto { get; set; }
        public int Odometro { get; set; }
        public decimal CantidadGalones { get; set; }
        public decimal TotalPrecio { get; set; }
    }

    public class FiltrosReporteValesActualizados
    {
        public string Nombre { get; set; }
        public int IdFiltro { get; set; }
    }

    public class ReporteValesActualizadosDAL
    {
        public IList<ReporteValesActualizados> ObtenerValesSinCF(int idVale, string numEquipo, string placa, string fechaInicio, string fechaFin, int idUsuario, int filtro, int usertypeid, int userid)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<ReporteValesActualizados> vales;
            using (ISession sesion = nHB.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValesSinCF :idVale, :numEquipo, :placa, :fechaInicio, :fechaFin, :idUsuario, :filtro, :usertypeid, :userid")
                .SetParameter("idVale", idVale)
                .SetParameter("numEquipo", numEquipo)
                .SetParameter("placa", placa)
                .SetParameter("fechaInicio", fechaInicio)
                .SetParameter("fechaFin", fechaFin)
                .SetParameter("idUsuario", idUsuario)
                .SetParameter("filtro", filtro)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("userid", userid)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReporteValesActualizados)))
                .List<ReporteValesActualizados>();
            }
            return vales;
        }

        public int ActualizarCreditoFiscal(int idVale, string CF, string fechaCF)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CGActualizarFactura :idVale, :CF, :fechaCF")
                .SetParameter("idVale", idVale)
                .SetParameter("CF", CF)
                .SetParameter("fechaCF", fechaCF)
                .List<int>()[0];
            }
            return resultado;
        }
    }
}