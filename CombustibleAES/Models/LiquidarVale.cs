using System;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombustibleAES.Models
{
    public class LiquidarVale
    {
        public int NumeroVale { get; set; }
        public string Alias { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string FechaGeneracion { get; set; }
        public string NoEmpleado { get; set; }
        public string TipoCombustible { get; set; }
        public string CentroCosto { get; set; }
        public int Odometro { get; set; }
        public decimal CantidadGalones { get; set; }
        public decimal PrecioGalon { get; set; }
        public decimal TotalPrecio { get; set; }
        public string NombreEmpleado { get; set; }
        public int OdometroAnt { get; set; }
        public int mobileid { get; set; }
        public string EstacionServicio { get; set; }
    }

    public class LiquidarValeDAL
    {
        public IList<LiquidarVale> ObtenerValeMostrar(int inumvale)
        {
            IList<LiquidarVale> valeliquidar;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                valeliquidar = session.CreateSQLQuery("exec MVC_CGConsultarValeNum :NumVale")
                .SetParameter("NumVale", inumvale)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(LiquidarVale)))
                .List<LiquidarVale>()
                .ToList();
            }
            return valeliquidar;
        }

        public int ActualizarVale(int iValeID, int iOdometro, double dGalones, double dPrecio, int iEstacionId, int iUserID, double dPrecioGalon)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery(@"exec MVC_LiquidarVale_CombustibleAES :Vale,:Odometro,:Galones,:Precio,:EstacionId,:UsuerId,:PrecioGalon")
               .SetParameter("Vale", iValeID)
               .SetParameter("Odometro", iOdometro)
               .SetParameter("Galones", dGalones)
               .SetParameter("Precio", dPrecio)
               .SetParameter("EstacionId", iEstacionId)
               .SetParameter("UsuerId", iUserID)
               .SetParameter("PrecioGalon", dPrecioGalon)               
               .List<int>()
               .ToList().ElementAt(0);
            }
            return resultado;
        }

        /// <summary>
        /// Se encarga de obtener el vale numero "NumVale" con la información actualizada
        /// </summary>
        /// <param name="NumVale">Número de vale del cual se quiere la información</param>
        /// <returns></returns>
        public IList<LiquidarVale> ObtenerValeActualizado(int idvale)
        {
            IList<LiquidarVale> valeliquidar;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                valeliquidar = session.CreateSQLQuery("exec SP_CGMostrarValeActualizado :NumVale")
                .SetParameter("NumVale", idvale)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(LiquidarVale)))
                .List<LiquidarVale>()
                .ToList();
            }
            return valeliquidar;
        }
    }
}
