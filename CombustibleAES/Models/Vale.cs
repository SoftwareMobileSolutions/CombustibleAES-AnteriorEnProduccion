using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombustibleAES.Models
{
    

    public class ValeMostrar
    {
        public int NumeroVale { get; set; }
        public string Alias { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string FechaGeneracion { get; set; }
        public string NoEmpleado { get; set; }
        public string Empleado { get; set; }
        public string TipoCombustible { get; set; }
        public decimal Saldo { get; set; }
    }

    // Información del vale que se va imprimir.
    public class ValeImprimir
    {
        public string NumeroVale { get; set; }
        public string Alias { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string FechaGeneracion { get; set; }
        public string NumEmpleado { get; set; }
        public string TipoCombustible { get; set; }
        public string InvoCC { get; set; }
        public string NumOrden { get; set; }
        public string UserAutorizacion { get; set; }
        public string NombreEmpleado { get; set; }
        public string NumInversion { get; set; }
        public string FechaVencimiento { get; set; }
    }

    public class PEP
    {
        public string Pep;
        public int Posicion;
    }

    public class PEPDAL
    {
        public IList<PEP> ObtenerPEPList()
        {
            IList<PEP> resultado;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ObtenerPEPList")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(PEP)))
                .List<PEP>();
            }
            return resultado;
        }
    }

    public class ValeDAL {
        // Colocar la lógica para obtener el vale ingresado.

        public int InsertarVale(int iCompanyid,int iRentado,int iTipoCosto,int itipoCombustibleID,string strFechaGeneracion,int iUserID,string strNombreEmpleado,
                                string strNumEmpleado,string strCentroCosto,string strNumOrden, int iMobileID,string strPlaca,string strAlias, string strInversion, double Saldo)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            strFechaGeneracion = DateTime.ParseExact(strFechaGeneracion,"dd/MM/yyyy",null).ToString("yyyyMMdd HH:mm:ss");
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery(@"exec MVC_InsertarValeGenerado  :Companyid, :Rentado, :TipoCosto, :TipoCombustibleID, :FechaGeneracion,
                                                    :UserID, :NombreEmpleado, :NumEmpleado, :CentroCosto, :NumOrden, :mobileid, :Placa, :Alias, :Inversion, :Saldo")
               .SetParameter("Companyid", iCompanyid)
               .SetParameter("Rentado", iRentado)
               .SetParameter("TipoCosto", iTipoCosto)
               .SetParameter("TipoCombustibleID", itipoCombustibleID)
               .SetParameter("FechaGeneracion", strFechaGeneracion)
               .SetParameter("UserID", iUserID)
               .SetParameter("NombreEmpleado", strNombreEmpleado)
               .SetParameter("NumEmpleado", strNumEmpleado)
               .SetParameter("CentroCosto", strCentroCosto)
               .SetParameter("NumOrden", strNumOrden)
               .SetParameter("mobileid", iMobileID)
               .SetParameter("Placa", strPlaca)
               .SetParameter("Alias", strAlias)
               .SetParameter("Inversion", strInversion)
               .SetParameter("Saldo", Saldo)
               .List<int>()
               .ToList().ElementAt(0);
            }
            return resultado;
        }

        public IList<ValeMostrar> ObtenerValeMostrar(int numvale)
        {
            IList<ValeMostrar> valeimprimir;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                valeimprimir = session.CreateSQLQuery("exec MVC_MostrarValeGenerado :numVale")
                    .SetParameter("numVale", numvale)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeMostrar)))
                   .List<ValeMostrar>()
                   .ToList();
            }            
            return valeimprimir;
        }

        public IList<ValeImprimir> ObtenerValeImprimir(int numVale, int userid)
        {
            IList<ValeImprimir> valeimprimir;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                valeimprimir = session.CreateSQLQuery("exec MVC_ObtenerValeImprimir :ValeId, :userid")
                    .SetParameter("ValeId", numVale)
                    .SetParameter("userid", userid)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ValeImprimir)))
                   .List<ValeImprimir>()
                   .ToList();
            }
            return valeimprimir;
        }
    }


    

}