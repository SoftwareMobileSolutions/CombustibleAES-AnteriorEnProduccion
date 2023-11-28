using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System.Collections.Generic;
using System.Linq;

namespace CombustibleAES.Models
{
    public class Usuario
    {
        public int userid { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int usertypeid { get; set; }
        public string tipousuario { get; set; }
        public string activo { get; set; }
        public int activoid { get; set; }
        public string email { get; set; }
        public int gasolineraID { get; set; }
    }

    public class UserDal
    {
        public IList<Usuario> ObtenerUsuarios()
        {
            IList<Usuario> Usuarios;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                Usuarios = session.CreateSQLQuery("exec MVC_MostrarUsuarios")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Usuario)))
                    .List<Usuario>()
                    .ToList();
            }
            return Usuarios;
        }
        
        public IList<Perfil> ObtenerPerfiles()
        {
            IList<Perfil> Perfiles;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                Perfiles = session.CreateSQLQuery("exec MVC_ObtenerTiposUsuarios")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Perfil)))
                    .List<Perfil>()
                    .ToList();
            }
            return Perfiles;
        }

        public IList<Gasolinera> ObtenerGasolineras()
        {
            IList<Gasolinera> Gasolineras;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                Gasolineras = session.CreateSQLQuery("exec MVC_ObtenerGasolineras")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Gasolinera)))
                    .List<Gasolinera>()
                    .ToList();                    
            }
            return Gasolineras;
        }

        public int InsertarUsuario(string strUsuario,string strNombre,string strApellido,string strContrasenia,string strEmail,int iUsertypeID,int iGasolineraID)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_IngresarUsuario  :usuario, :nombre, :apellido, :contrasenia, :email, :usertypeid, :gasolineraID")
               .SetParameter("usuario", strUsuario)
               .SetParameter("nombre", strNombre)
               .SetParameter("apellido", strApellido)
               .SetParameter("contrasenia", strContrasenia)
               .SetParameter("email", strEmail)
               .SetParameter("usertypeid", iUsertypeID)
               .SetParameter("gasolineraID", iGasolineraID)               
               .List<int>()
               .ToList().ElementAt(0);
            }
            return resultado;
        }

        public int ModificarUsuario(int iUserid,string strNombre,string strApellido,string strContrasenia,string strEmail,int iUsertypeid,int iActivo,int iGasolineraID,int iActualizarContra)
        {
            int resultado = 0;
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                resultado = session.CreateSQLQuery("exec MVC_ActualizarUsuario  :userid, :nombre, :apellido, :contrasenia, :email, :usertypeid, :activo, :gasolineraID, :actualizarcontra")
               .SetParameter("userid", iUserid)
               .SetParameter("nombre", strNombre)
               .SetParameter("apellido", strApellido)
               .SetParameter("contrasenia", strContrasenia)
               .SetParameter("email", strEmail)
               .SetParameter("usertypeid", iUsertypeid)
               .SetParameter("activo", iActivo)
               .SetParameter("gasolineraID", iGasolineraID)
               .SetParameter("actualizarcontra", iActualizarContra)
               .List<int>()
               .ToList().ElementAt(0);
            }
            return 1;
        }

    }
}