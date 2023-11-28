using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Transform;
using CombustibleVales.Models.NHibernate.Connections;

namespace CombustibleAES.Models
{
    public class AdministracionVehiculos
    {
        public int Empresaid { get; set; }
        public string Empresa { get; set; }
        public int Mobileid { get; set; }
        public string Equipo { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Year { get; set; }
        public double KmxGalon1 { get; set; }
        public int Status { get; set; }
    }

    public class EstadosVehiculos
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }

    public class EstadosVehiculosDAL
    {
        public IList<EstadosVehiculos> ObtenerEstados()
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<EstadosVehiculos> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_ObtenerEstadosMobile")
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(EstadosVehiculos)))
                .List<EstadosVehiculos>();
            }
            return resultado;
        }
    }

    public class AdministracionVehiculosDAL
    {
        public IList<AdministracionVehiculos> ObtenerVehiculo(int idVehiculo)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            IList<AdministracionVehiculos> resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CGObtenerVehiculo :idVehiculo") // Se cambia el llamado del procedimiento a la versión MVC para 
                                                                                            // conservar compatibilidad con sistema anterior.
                .SetParameter("idVehiculo", idVehiculo)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(AdministracionVehiculos)))
                .List<AdministracionVehiculos>();
            }
            return resultado;
        }

        public int ActualizarMobile(int mobileid, int empresaid, string alias, string placa, string make, string model, int yearmade, double kmxGalon, int status)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                // Se cambia el llamado del procedimiento a la versión MV conservar compatibilidad con sistema anterior.
                resultado = sesion.CreateSQLQuery("exec MVC_CGActualizarMobile :mobileid, :empresaid, :alias, :placa, :make, :model, :yearmade, :kmxGalon, :status")
                .SetParameter("mobileid", mobileid)
                .SetParameter("empresaid", empresaid)
                .SetParameter("alias", alias)
                .SetParameter("placa", placa)
                .SetParameter("make", make)
                .SetParameter("model", model)
                .SetParameter("yearmade", yearmade)
                .SetParameter("kmxGalon", kmxGalon)
                .SetParameter("status", status)
                .List<int>()[0];
            }
            return resultado;
        }

        public int InsertarMobile(int empresaid, string alias, string placa, string make, string model, int yearmade, double kmxGalon)
        {
            NHibernateLoginSession nHB = new NHibernateLoginSession();
            int resultado;
            using (ISession sesion = nHB.OpenSession())
            {
                // Se cambia el llamado del procedimiento a la versión MV conservar compatibilidad con sistema anterior.
                resultado = sesion.CreateSQLQuery("exec MVC_CGInsertarMobile :empresaid, :alias, :placa, :make, :model, :yearmade, :kmxGalon")
                .SetParameter("empresaid", empresaid)
                .SetParameter("alias", alias)
                .SetParameter("placa", placa)
                .SetParameter("make", make)
                .SetParameter("model", model)
                .SetParameter("yearmade", yearmade)
                .SetParameter("kmxGalon", kmxGalon)
                .List<int>()[0];
            }
            return resultado;
        }
    }

}