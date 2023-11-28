using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class ActualizarGasDAL
    {        
        public int ActualizarVale(int iValeID, int iEstacionId)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery(@"exec MVC_CGActualizarInfoValeES :Vale,:EstacionId")
               .SetParameter("Vale", iValeID)
               .SetParameter("EstacionId", iEstacionId)
               .List<int>()
               .ToList().ElementAt(0);
            }

            return resultado;
        }
    }
}