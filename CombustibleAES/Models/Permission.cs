using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombustibleAES.Models
{
    public class PermissionDAL
    {
        public IList<Tree> ObtenerMenu(int usertype)
        {
            IList<Tree> permission;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {                
                permission = session.CreateSQLQuery("exec MVC_VCObtener_PermisosTree_Plan :usertypeid")               
               .SetParameter("usertypeid", usertype)
               .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Tree)))
               .List<Tree>()
               .ToList();
            }
            return permission;
        }
    }
}