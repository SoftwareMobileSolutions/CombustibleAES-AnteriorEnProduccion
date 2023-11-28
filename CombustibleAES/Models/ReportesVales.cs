using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class ValeReporte
    {
        public string CreditoFiscal { get; set; }
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
        public string PrecioxGalon { get; set; }
        public string TotalPrecio { get; set; }
        public string Empresa { get; set; }
        public string FechaGeneracion { get; set; }
        public string NoEmpleado { get; set; }
        public string Empleado { get; set; }
        public string Usuario { get; set; }
        public string TipoCombustible { get; set; }
        public string EstadoVale { get; set; }
        public string Gasolinera { get; set; }
        public string FechaCierre { get; set; }
        public string FechaIngresoNumCF { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public string Tarjeta { get; set; }
        public decimal ValorVale { get; set; }
    }

    public class ReporteslDAL
    {
        public IList<ValeReporte> ObtenerReporteGeneral(int iuserid, int iusertypeid, DateTime dtFechaIni, DateTime dtFechaFin, int numVale, string creditoFiscal, int filtro)
        {
            IList<ValeReporte> general;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                // Se creo otro procedimiento almacenado porque ya existia uno.
                general = session.CreateSQLQuery("exec MVC_CGObtenerReporteGeneralVales_1 :userid,:usertypeid,:FechaIni,:FechaFin, :numVale, :creditoFiscal, :filtro")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeReporte)))
                    .SetParameter("userid", iuserid)
                    .SetParameter("usertypeid", iusertypeid)
                    .SetParameter("FechaIni", dtFechaIni.ToString("yyyyMMdd"))
                    .SetParameter("FechaFin",dtFechaFin.ToString("yyyyMMdd"))                    
                    .SetParameter("numVale", numVale)
                    .SetParameter("creditoFiscal", creditoFiscal)
                    .SetParameter("filtro", filtro)
                   .List<ValeReporte>()
                   .ToList();
            }
            return general;
        }
    }
}