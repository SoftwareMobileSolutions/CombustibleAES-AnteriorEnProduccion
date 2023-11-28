using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class CambiarEstado
    {
        public int NumeroVale { get; set; }
        public string EstadoVale { get; set; }
        public int IdEstado { get; set; }
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
        public string CreditoFiscal { get; set; }
        public string EstacionServicio { get; set; }
    }

    public class FiltrosCambiarEstado
    {
        public string Nombre { get; set; }
        public int IdFiltro { get; set; }
    }

    public class CambiarEstadoDAL
    {
        //[MVC_ObtenerEstadosCambiar]
        public IList<Estados> ObtenerEstados()
        {
            IList<Estados> estados;
            NHibernateLoginSession nHibernate = new NHibernateLoginSession();
            using (ISession sesion = nHibernate.OpenSession())
            {
                estados = sesion.CreateSQLQuery("exec MVC_ObtenerEstadosCambiar")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Estados)))
                .List<Estados>().ToList();
            }
            return estados;
        }

        public IList<CambiarEstado> ObtenerValesEstado(int idVale, string numEquipo, string placa, string fechaInicio, string fechaFin, int idUsuario, int filtro, int usertypeid, int userid)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<CambiarEstado> vales;
            using (ISession sesion = nHB.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValesEstado :idVale, :numEquipo, :placa, :fechaInicio, :fechaFin, :idUsuario, :filtro, :usertypeid, :userid")
                .SetParameter("idVale", idVale)
                .SetParameter("numEquipo", numEquipo)
                .SetParameter("placa", placa)
                .SetParameter("fechaInicio", fechaInicio)
                .SetParameter("fechaFin", fechaFin)
                .SetParameter("idUsuario", idUsuario)
                .SetParameter("filtro", filtro)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("userid", userid)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CambiarEstado)))
                .List<CambiarEstado>();
            }
            return vales;
        }

        public int ActualizarEstadoVale(int valeId, int estadoId, int userId, int usertypeid, string comentarioAnulado)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int respuesta;
            using (ISession sesion = nHB.OpenSession())
            {
                respuesta = sesion.CreateSQLQuery("exec MVC_CambiarEstado :valeId, :estadoId, :userId, :usertypeid, :comentarioAnulado")
                .SetParameter("valeId", valeId)
                .SetParameter("estadoId", estadoId)
                .SetParameter("userId", userId)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("comentarioAnulado", comentarioAnulado)
                .List<int>()[0];
            }
            return respuesta;
        }
    }
}