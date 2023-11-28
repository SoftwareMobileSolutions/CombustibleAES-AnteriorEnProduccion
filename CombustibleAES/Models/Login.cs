using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;

namespace CombustibleAES.Models
{
    public class LogIn
    {
        #region variables
        public int userid { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }
        public int usertypeid { get; set; }
        public int gasolineraid { get; set; }            
        #endregion
        
    }
    public class LogInDal
    {
        public IList<LogIn> CheckSession(string username, string contra)
        {
            IList<LogIn> login;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                login = session.CreateSQLQuery("exec MVC_CGValidarUser :userLogin, :pass ")
                .SetParameter("userLogin", username)
                .SetParameter("pass", contra)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(LogIn)))
                .List<LogIn>();                
            }
            return login;
        }

        public IList<LogIn> CheckSessionByLastUpdate(string iUserName, string iPassword)
        {
            IList<LogIn> login;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                login = session.CreateSQLQuery("exec SP_ValidatePasswordExpiration :userName, :password ")
                .SetParameter("userName", iUserName)
                .SetParameter("password", iPassword)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(LogIn)))
                .List<LogIn>();
            }
            return login;
        }

        public int UpdatePasswordByExpiration(string iUserName, string iPassword, string iNewPassword)
        {
            int answer = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                answer = session.CreateSQLQuery("exec SP_SecurityPasswordUpdate :userName, :password, :newPass")
                    .SetParameter("userName", iUserName)
                    .SetParameter("password", iPassword)
                    .SetParameter("newPass", iNewPassword)
                    .List<int>().ElementAt(0);
            }

                return answer;
        }


        public int recoveryPass(string strUser, string strEmail, string strRandomPass)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec SP_PasswordRecovery :login, :email, :randomPass")
                    .SetParameter("login", strUser)
                    .SetParameter("email", strEmail)
                    .SetParameter("randomPass", strRandomPass)
                    .List<int>()
                    .ToList().ElementAt(0);
            }
            return resultado;
        }
    }
}