using System;
using System.Collections.Generic;
using CombustibleVales.Models.NHibernate.Connections;
using NHibernate;
using NHibernate.Transform;
using System.Linq;
using System.Web;

namespace CombustibleAES.Models
{
    public class CorregirVale
    {
        //public bool EnableAnular { get; set; }
        public int UserTypeId { get; set; }
        public int NumeroVale { get; set; }
        public int IdEstadoVale { get; set; }
        public string EstadoVale { get; set; }
        public string CreditoFiscal { get; set; }
        public int Odometro { get; set; }
        public decimal CantidadGalones { get; set; }
        public decimal PrecioGalon { get; set; }
        public decimal TotalPrecio { get; set; }
        public int IdEstacionServicio { get; set; }
        public string EstacionServicio { get; set; }
        public int TipoCombustible { get; set; }
        public string ComentarioAnulado { get; set; }
        public string FechaCF { get; set; }
    }

    public class CorregirValeGrid
    {
        public int NumVale { get; set; }
        public int Odometro_ant { get; set; }
        public int Odometro { get; set; }
        public string Combustible_ant { get; set; }
        public string Combustible { get; set; }
        public decimal CantidadGalones_ant { get; set; }
        public decimal CantidadGalones { get; set; }
        public decimal TotalPrecio_ant { get; set; }
        public decimal TotalPrecio { get; set; }
        public decimal PrecioxGalon_ant { get; set; }
        public decimal PrecioxGalon { get; set; }
        public string CreditoFiscal_ant { get; set; }
        public string CreditoFiscal { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
    }

    public class CorregirValeDAL
    {
        /// <summary>
        /// Obtiene la información editable actual del vale de combustible espeficicado
        /// </summary>
        /// <param name="valeid">Id del vale</param>
        /// <returns>Objeto de clase: CorregirVale</returns>
        public List<CorregirVale> obtenerValeCorregir(int valeid, int usertypeid, int userid)
        {
            List<CorregirVale> vales;
            NHibernateLoginSession nHibernateSession = new NHibernateLoginSession();
            using (ISession sesion = nHibernateSession.OpenSession())
            {
                vales = sesion.CreateSQLQuery("exec MVC_ObtenerValeCorregir :valeid, :usertypeid, :userid")
                .SetParameter("valeid", valeid)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("userid", userid)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CorregirVale)))
                .List<CorregirVale>().ToList();
            }
            return vales;
        }

        /// <summary>
        /// Se encarga de corregir la información "editable" de un vale de combustible
        /// </summary>
        /// <param name="valeid">Id del vale</param>
        /// <param name="estado">Estado del vale</param>
        /// <param name="creditoFiscal">Nuevo crédito fiscal</param>
        /// <param name="creditoFiscal_ant">Credito fiscal anterior</param>
        /// <param name="odometro">Nuevo odometro</param>
        /// <param name="odometro_ant">Odometro anterior</param>
        /// <param name="cantGalones">Nueva cantidad de galones</param>
        /// <param name="cantGalones_ant">Cantidad de galones anterior</param>
        /// <param name="precioGalon">Nuevo precio por unidad de galon</param>
        /// <param name="precioGalon_ant">Precio por unidad de galon anterior</param>
        /// <param name="total">Nuevo precio total</param>
        /// <param name="total_ant">Precio total anterior</param>
        /// <param name="estacionServicio">Estacion de servicio</param>
        /// <param name="tipoCombustible">Tipo de combustible</param>
        /// <param name="fechaActualizacion">Fecha de actualización del vale</param>
        /// <param name="idUsuario">Id del usuario que correge el vale</param>
        /// <returns>Integer; 1 = Exito.</returns>
        public int corregirVale(int valeid, int estado, string creditoFiscal, string creditoFiscal_ant, int odometro, int odometro_ant, double cantGalones, double cantGalones_ant, double precioGalon, double precioGalon_ant, double total, double total_ant, int estacionServicio, int tipoCombustible, string fechaActualizacion, int idUsuario, string comentarioAnulado, int usertypeid, string fechaCF)
        {
            int resultado = 0;
            NHibernateLoginSession nHibernateSession = new NHibernateLoginSession();
            using (ISession sesion = nHibernateSession.OpenSession())
            {
                resultado = sesion.CreateSQLQuery("exec MVC_CorregirVale :valeid, :estado, :creditoFiscal, :creditoFiscal_ant, :odometro, :odometro_ant, :cantGalones, :cantGalones_ant, :precioGalon, :precioGalon_ant, :total , :total_ant, :estacionServicio, :tipoCombustible, :fechaActualizacion, :userid, :comentario, :usertypeid, :fechaCF")
                .SetParameter("valeid", valeid)
                .SetParameter("estado", estado)
                .SetParameter("creditoFiscal", creditoFiscal)
                .SetParameter("creditoFiscal_ant", creditoFiscal_ant)
                .SetParameter("odometro", odometro)
                .SetParameter("odometro_ant", odometro_ant)
                .SetParameter("cantGalones", cantGalones)
                .SetParameter("cantGalones_ant", cantGalones_ant)
                .SetParameter("precioGalon", precioGalon)
                .SetParameter("precioGalon_ant", precioGalon_ant)
                .SetParameter("total", total)
                .SetParameter("total_ant", total_ant)
                .SetParameter("estacionServicio", estacionServicio)
                .SetParameter("tipoCombustible", tipoCombustible)
                .SetParameter("fechaActualizacion", fechaActualizacion)
                .SetParameter("userid", idUsuario)
                .SetParameter("comentario", comentarioAnulado)
                .SetParameter("usertypeid", usertypeid)
                .SetParameter("fechaCF", fechaCF)
                .List<int>().ToList()[0];
            }
            return resultado;
        }
    }
}