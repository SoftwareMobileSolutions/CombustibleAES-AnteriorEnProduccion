using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class CentroServicio
    {
        public int Gasolineraid { get; set; }
        public string Codigo { get; set; }
        public string CServicio { get; set; }
        public string Ciudad { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string NIT { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

    }
    public class CentroServicioDAL
    {
        public IList<CentroServicio> ObtenerCentrosServicio()
        {
            IList<CentroServicio> CServicios;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                CServicios = session.CreateSQLQuery("execute MVC_ObtenerCentrosServicios")                
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CentroServicio)))
                    .List<CentroServicio>()
                    .ToList();
            }
            return CServicios;
        }

        public int InsertarCentroServicio(string strCentroServicio,string strDireccion,string strNIT,string strEmail,string strCodigo,string strCiudad,
            string strContacto,string strTelefono)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_IngresarCentroServicio :CentroServicio, :Direccion, :NIT, :Email, :Codigo, :Ciudad, :Contacto,:Telefono")
               .SetParameter("CentroServicio", strCentroServicio)
               .SetParameter("Direccion", strDireccion)
               .SetParameter("NIT", strNIT)
               .SetParameter("Email", strEmail)
               .SetParameter("Codigo", strCodigo)
               .SetParameter("Ciudad", strCiudad)
               .SetParameter("Contacto", strContacto)
               .SetParameter("Telefono", strTelefono)
               .List<int>()
               .ToList().ElementAt(0);
            }
            return resultado;
        }


        public int ActualizarCentroServicio(int iGasolineraid,string strCentroServicio, string strDireccion, string strNIT, string strEmail, string strCodigo, 
            string strCiudad, string strContacto, string strTelefono)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ActualizarCentroServicio :Gasolineraid, :CentroServicio, :Direccion, :NIT, :Email, :Codigo, :Ciudad, :Contacto,:Telefono")
               .SetParameter("Gasolineraid",iGasolineraid)
               .SetParameter("CentroServicio", strCentroServicio)
               .SetParameter("Direccion", strDireccion)
               .SetParameter("NIT", strNIT)
               .SetParameter("Email", strEmail)
               .SetParameter("Codigo", strCodigo)
               .SetParameter("Ciudad", strCiudad)
               .SetParameter("Contacto", strContacto)
               .SetParameter("Telefono", strTelefono)
               .List<int>()
               .ToList().ElementAt(0);
            }
            return resultado;
        }



    }
}