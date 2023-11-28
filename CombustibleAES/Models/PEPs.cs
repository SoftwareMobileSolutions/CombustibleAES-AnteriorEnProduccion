using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class PEPs
    {
        public string PEP { get; set; }
        public string NombreProyecto { get ; set; }
        public string Responsabe { get; set; }
        public string Estado { get; set; }
    }

    public class PEPsDAL
    {
        public IList<PEPs> InsertarPEP(string pep, string proyecto, string responsable, string estadopep)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<PEPs> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CGInsertarPEP :pep, :proyecto, :responsable, :estadopep")
                .SetParameter("pep", pep)
                .SetParameter("proyecto", proyecto)
                .SetParameter("responsable", responsable)
                .SetParameter("estadopep", estadopep)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(PEPs)))
                .List<PEPs>();
            }
            return resultado;
        }

        public IList<PEPs> ObtenerListaPEPs(int inicio, int fin)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<PEPs> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CGInsertarPEP :inicio, :fin")
                .SetParameter("inicio", inicio)
                .SetParameter("fin", fin)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(PEPs)))
                .List<PEPs>();
            }
            return resultado;
        }
    }
}