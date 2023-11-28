using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;
using System;
using Environment = NHibernate.Cfg.Environment;

namespace CombustibleVales.Models.NHibernate.Connections
{
    public class NHibernateSession
    {
        public ISession OpenSession()
        {
            var cn = new conectionData();
            var data = cn.GetBD();
            
            ISessionFactory sessionFactory;
            string connectionString = "";
            RegistryKey key = Registry.LocalMachine.OpenSubKey("software\\sms\\CombustibleAES");
            if(key == null)
            {
                throw new System.InvalidOperationException("No se ha encontrado el registro software/sms/CombustibleAES");
            }
            else
            {
                //Object objConnection = key.GetValue("CadenaConexionDes");
                Object objConnection = key.GetValue("CadenaConexion");
                key.Close();

                if (objConnection == null)
                {
                    throw new System.InvalidOperationException("No se ha definido la variable CadenaConexion en el registro software software/sms/CombustibleAES por favor verificar");                                        
                }
                else
                {
                    connectionString = objConnection.ToString();
                }
            }

#if des
            connectionString = @"Data Source = 10.0.0.14; Initial Catalog = ControlCombustibleAES_20221024; User ID = Mantenimiento; Password = mttofleet";
#endif


            var cfg = new Configuration().AddProperties(new Dictionary<string, string> {
                    {Environment.ConnectionDriver, typeof (Sql2008ClientDriver).FullName},
                    {Environment.Dialect, typeof (MsSql2012Dialect).FullName},
                    {Environment.ConnectionProvider, typeof (DriverConnectionProvider).FullName},
                   
                    {Environment.ConnectionString, connectionString}, }).AddAssembly(Assembly.GetExecutingAssembly());
            sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
    public class NHibernateLoginSession
    {
        public ISession OpenSession()
        {
            ISessionFactory sessionFactory;

            string connectionString = "";
            RegistryKey key = Registry.LocalMachine.OpenSubKey("software\\sms\\CombustibleAES");
            if (key == null)
            {
                throw new System.InvalidOperationException("No se ha encontrado el registro software/sms/CombustibleAES");
            }
            else
            {
                Object objConnection = key.GetValue("CadenaConexion");
                //Object objConnection = key.GetValue("CadenaConexionDes");
                key.Close();

                if (objConnection == null)
                {
                    throw new System.InvalidOperationException("No se ha definido la variable CadenaConexion en el registro software software/sms/CombustibleAES por favor verificar");
                }
                else
                {
                    connectionString = objConnection.ToString();
                }
            }

#if des
            connectionString = @"Data Source = 10.0.0.14; Initial Catalog = ControlCombustibleAES_20221024; User ID = Mantenimiento; Password = mttofleet";
#endif

            var cfg = new Configuration().AddProperties(new Dictionary<string, string>
            {
                {Environment.ConnectionDriver,typeof(Sql2008ClientDriver).FullName},
                {Environment.Dialect,typeof(MsSql2012Dialect).FullName},
                {Environment.ConnectionProvider,typeof (DriverConnectionProvider).FullName},
                {Environment.ConnectionString,connectionString},
            }).AddAssembly(Assembly.GetExecutingAssembly());
            sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
    public class conectionData
    {
        string dtSource;

        public string DtSource
        {
            get { return dtSource; }
            set { dtSource = value; }
        }
        string catalogo;

        public string Catalogo
        {
            get { return catalogo; }
            set { catalogo = value; }
        }
        string usrSQL;

        public string UsrSQL
        {
            get { return usrSQL; }
            set { usrSQL = value; }
        }
        string passSQL;

        public string PassSQL
        {
            get { return passSQL; }
            set { passSQL = value; }
        }

        public conectionData GetBD()
        {
            string CadenaUserSQL = System.Web.HttpContext.Current.Session["cadenaUserSQL"] as string;
            string[] datos = CadenaUserSQL.Split('|');
            return new conectionData() { DtSource = datos[0], Catalogo = datos[1], UsrSQL = datos[2], PassSQL = datos[3] };
        }
    }
}