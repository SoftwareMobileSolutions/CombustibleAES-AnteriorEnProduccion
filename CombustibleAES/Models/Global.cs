using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombustibleAES.Models
{
    public class Empresa
    {
        public int empresaid { get; set; }
        public string empresa { get; set; }
    }

    public class Equipo
    {
        public int mobileid { get; set; }
        public string name { get; set; }
    }

    public class Combustible
    {
        public int combustibleid { get; set; }
        public string nombre { get; set; }
    }

    public class Gasolineras
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Estados
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }

    public class Years_Months
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class TipoTaxi
    {
        public int IdTipo { get; set; }
        public string Tipo { get; set; }
    }

    public class Area
    {
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
    }

    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
    }
    
    public class GlobalDAL
    {
        public IList<Empresa> ObtenerEmpresasAES()
        {
            IList<Empresa> empresa;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                empresa = session.CreateSQLQuery("exec MVC_ObtenerCompaniasAES")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Empresa)))
                   .List<Empresa>()
                   .ToList();
            }
            return empresa;
        }

        public IList<Equipo> ObtenerEquiposFlota(int iEmpresaID)
        {
            IList<Equipo> equipos;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                equipos = session.CreateSQLQuery("exec SP_CGObtenerEquiposAES :EmpresaID")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Equipo)))
                    .SetParameter("EmpresaID", iEmpresaID)
                   .List<Equipo>()
                   .ToList();
            }
            return equipos;
        }

        public IList<Combustible> ObtenerTiposCombustible()
        {
            IList<Combustible> tiposcombustible;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                tiposcombustible = session.CreateSQLQuery("exec SP_CGObtenerTipoCombustible")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Combustible)))
                   .List<Combustible>()
                   .ToList();
            }
            return tiposcombustible;
        }

        public IList<Gasolineras> ObtenerGasolineras(int iUserid,int iUsertypeid)
        {
            IList<Gasolineras> gasolineras;
            NHibernateLoginSession nHsession = new NHibernateLoginSession();
            using (ISession session = nHsession.OpenSession())
            {
                gasolineras = session.CreateSQLQuery("exec SP_CGObtenerCentroServicioCombustible :Userid,:UserType")
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Gasolineras)))
                    .SetParameter("Userid",iUserid)
                    .SetParameter("UserType",iUsertypeid)
                   .List<Gasolineras>()
                   .ToList();
            }
            return gasolineras;
        }

        public string ObtenerPlaca(int imobileid)
        {
            string strPlaca = "";
            NHibernateLoginSession NHLSession = new NHibernateLoginSession();
            using (ISession session = NHLSession.OpenSession())
            {
                strPlaca = session.CreateSQLQuery("exec MVC_ObtenerPlaca  :mobileid")
               .SetParameter("mobileid", imobileid)
               .List<string>()
               .ToList().ElementAt(0);
            }
            return strPlaca;
        }

        public IList<Estados> ObtenerEstados(int usertypeid, int estadoId)
        {
            IList<Estados> estados;
            NHibernateLoginSession nHibernate = new NHibernateLoginSession();
            using (ISession sesion = nHibernate.OpenSession())
            {
                estados = sesion.CreateSQLQuery("exec MVC_ObtenerEstados :usertypeid, :estadoId")
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("estadoId", estadoId)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Estados)))
                .List<Estados>().ToList();
            }
            return estados;
        }

        public IList<Area> ObtenerAreas()
        {
            IList<Area> areas;
            NHibernateLoginSession nHBLogin = new NHibernateLoginSession();
            using (ISession sesion = nHBLogin.OpenSession())
            {
                areas = sesion.CreateSQLQuery("exec MVC_CTObtenerAreasTaxi")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Area)))
                .List<Area>();
            }
            return areas;
        }

        public IList<Usuarios> ObtenerUsuarios()
        {
            IList<Usuarios> users;
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            using(ISession sesion = nHB.OpenSession())
            {
                users = sesion.CreateSQLQuery("exec MVC_ObtenerUsuariosList")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Usuarios)))
                .List<Usuarios>();
            }
            return users;
        }

    }
}