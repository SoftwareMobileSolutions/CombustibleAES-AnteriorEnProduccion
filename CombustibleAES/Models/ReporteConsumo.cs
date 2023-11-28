using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Transform;
using CombustibleVales.Models.NHibernate.Connections;

namespace CombustibleAES.Models
{
    public class ReporteConsumo
    {
        public string CCF { get; set; }
        public string Proveedor { get; set; }
        public string NSAP { get; set; }
        public string Razonsocial { get; set; }
        public string Subflota { get; set; }
        public string CentroCosto { get; set; }
        public string Orden { get; set; }
        public string PEP { get; set; }
        public string Alias { get; set; }
        public string Placa { get; set; }
        public string Odometro { get; set; }
        public string NumeroVale { get; set; }
        public string CantidadGalones { get; set; }
        public string TotalPrecio { get; set; }
        public decimal FovialPlusSubsidio { get; set; }
        public decimal IVA { get; set; }
        public decimal Gasto { get; set; }
        public decimal PrecioxGalon { get; set; }
        public string Empresa { get; set; }
        public string FechaGeneracion { get; set; }
        public string NoEmpleado { get; set; }
        public string Empleado { get; set; }
        public string Usuario { get; set; }
        public string TipoCombustible { get; set; }
        public string EstadoVale { get; set; }
        public string FechaCierre { get; set; }
        public string FechaIngresoNumCF { get; set; }

    }

    public class FiltroReporteConsumo
    {
        public int FiltroId { get; set; }
        public string Filtro { get; set; }
    }

    public class ReporteConsumoDAL
    {
        public IList<ReporteConsumo> ObtenerReporteConsumo(int userid, int usertypeid, int filtro, string fechaInicio, string fechaFin)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<ReporteConsumo> registros;
            using (ISession sesion = nHB.OpenSession())
            {
                registros = sesion.CreateSQLQuery("exec SP_CGObtenerReporteConsumoVales :userid, :usertypeid, :filtro, :fechaInicio, :fechaFin")
                .SetParameter("userid", userid)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("filtro", filtro)
                .SetParameter("fechaInicio", fechaInicio)
                .SetParameter("fechaFin", fechaFin)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ReporteConsumo)))
                .List<ReporteConsumo>();
            }
            return registros;
        }
    }

}