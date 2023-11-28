using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombustibleVales.Models;

namespace CombustibleAES.Models
{
    public class Tree
    {
        #region variables
        private int devicetypeid;
        private string devicetype;

        public virtual int Devicetypeid
        {
            get { return devicetypeid; }
            set { devicetypeid = value; }
        }

        public virtual string Devicetype
        {
            get { return devicetype; }
            set { devicetype = value; }
        }


        private int subfleetPool;

        public string Device
        {
            get
            {
                return this.device;
            }
            set
            {
                this.device = value;
            }
        }

        public virtual int SubfleetPool
        {
            get { return subfleetPool; }
            set { subfleetPool = value; }
        }

        private int nivel;
        private string cod;
        private string name;
        private int valorid; //Es el moduleid que devuelve el procedimiento  Fleets_Obtener_PermisosTree que es usado para los permisos
        private string parent;
        private string grandparent;
        //Menu | Panel
        private string urlpage;
        private string urlimagen;
        private string icon;
        private string description;
        //Detalles de los vehiculos + motorista
        private int? codeprofileid;
        private int? mobiletypeid;
        private int? deviceid;
        private string make;
        private string model;
        private int yearmade;
        private string plate;
        private string vin;
        private double? capacity;
        private double? KmxGalon1;
        private int? status;
        private int? operadorid;
        private int? ioconfig;
        private string NumeroTC;
        private string NIT;
        private string Clase;
        private string Tipo;
        private string Motor;
        private string Chasis;
        private int combustibleid;
        private int? CommandProfileid;
        private string TipoPlan;
        private int sensorid;
        private double GalonesXHora;
        private int SensorCombustible;
        private int? Tipo_Vehiculo;
        private int? MobileEstadoID;
        private string TipoMotor;
        private int motoristaid;
        private string nombremotorista;
        private string lic_num;
        private byte? claselicid;
        private string activo;
        private string IMEI;
        private string device;
        #endregion

        #region encapsulamiento
        public virtual int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public virtual string Cod
        {
            get { return cod; }
            set { cod = value; }
        }
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual int Valorid
        {
            get { return valorid; }
            set { valorid = value; }
        }
        public virtual string Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public virtual string Grandparent
        {
            get { return grandparent; }
            set { grandparent = value; }
        }
        public virtual string Urlpage
        {
            get { return urlpage; }
            set { urlpage = value; }
        }
        public virtual string Urlimagen
        {
            get { return urlimagen; }
            set { urlimagen = value; }
        }
        public virtual string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }
        //Detalles de los vehiculos + motorista
        public virtual int? Codeprofileid
        {
            get { return codeprofileid; }
            set { codeprofileid = value; }
        }
        public virtual int? Mobiletypeid
        {
            get { return mobiletypeid; }
            set { mobiletypeid = value; }
        }
        public virtual int? Deviceid
        {
            get { return deviceid; }
            set { deviceid = value; }
        }
        public virtual string Make
        {
            get { return make; }
            set { make = value; }
        }
        public virtual string Model
        {
            get { return model; }
            set { model = value; }
        }
        public virtual int Yearmade
        {
            get { return yearmade; }
            set { yearmade = value; }
        }
        public virtual string Plate
        {
            get { return plate; }
            set { plate = value; }
        }
        public virtual string Vin
        {
            get { return vin; }
            set { vin = value; }
        }
        public virtual double? Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        public virtual double? KmxGalon11
        {
            get { return KmxGalon1; }
            set { KmxGalon1 = value; }
        }
        public virtual int? Status
        {
            get { return status; }
            set { status = value; }
        }
        public virtual int? Operadorid
        {
            get { return operadorid; }
            set { operadorid = value; }
        }
        public virtual int? Ioconfig
        {
            get { return ioconfig; }
            set { ioconfig = value; }
        }
        public virtual string NumeroTC1
        {
            get { return NumeroTC; }
            set { NumeroTC = value; }
        }
        public virtual string NIT1
        {
            get { return NIT; }
            set { NIT = value; }
        }
        public virtual string Clase1
        {
            get { return Clase; }
            set { Clase = value; }
        }
        public virtual string Tipo1
        {
            get { return Tipo; }
            set { Tipo = value; }
        }
        public virtual string Motor1
        {
            get { return Motor; }
            set { Motor = value; }
        }
        public virtual string Chasis1
        {
            get { return Chasis; }
            set { Chasis = value; }
        }
        public virtual int Combustibleid
        {
            get { return combustibleid; }
            set { combustibleid = value; }
        }
        public virtual int? CommandProfileid1
        {
            get { return CommandProfileid; }
            set { CommandProfileid = value; }
        }
        public virtual string TipoPlan1
        {
            get { return TipoPlan; }
            set { TipoPlan = value; }
        }
        public virtual int Sensorid
        {
            get { return sensorid; }
            set { sensorid = value; }
        }
        public virtual double GalonesXHora1
        {
            get { return GalonesXHora; }
            set { GalonesXHora = value; }
        }
        public virtual int SensorCombustible1
        {
            get { return SensorCombustible; }
            set { SensorCombustible = value; }
        }
        public virtual int? Tipo_Vehiculo1
        {
            get { return Tipo_Vehiculo; }
            set { Tipo_Vehiculo = value; }
        }
        public virtual int? MobileEstadoID1
        {
            get { return MobileEstadoID; }
            set { MobileEstadoID = value; }
        }
        public virtual string TipoMotor1
        {
            get { return TipoMotor; }
            set { TipoMotor = value; }
        }
        public virtual int Motoristaid
        {
            get { return motoristaid; }
            set { motoristaid = value; }
        }
        public virtual string Nombremotorista
        {
            get { return nombremotorista; }
            set { nombremotorista = value; }
        }
        public virtual string Lic_num
        {
            get { return lic_num; }
            set { lic_num = value; }
        }
        public virtual byte? Claselicid
        {
            get { return claselicid; }
            set { claselicid = value; }
        }
        public virtual string Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        public virtual string IMEI1
        {
            get { return IMEI; }
            set { IMEI = value; }
        }
        #endregion
    }
    public class Categoria
    {
        public virtual string CategoryName { get; set; }
        public virtual string codigo { get; set; }
        public virtual string parent { get; set; }
        public virtual string grandparent { get; set; }
        public virtual string urlpage { get; set; }
        public virtual string urlimagen { get; set; }
        public virtual string icon { get; set; }
        public virtual string description { get; set; }
        public virtual int valorid { get; set; }
        public virtual List<SubCategoria> SubCategories { get; set; }
    }
    public class SubCategoria
    {
        public virtual string SubCategoryName { get; set; }
        public virtual string codigo { get; set; }
        public virtual string parent { get; set; }
        public virtual string grandparent { get; set; }
        public virtual string urlpage { get; set; }
        public virtual string urlimagen { get; set; }
        public virtual string icon { get; set; }
        public virtual string description { get; set; }
        public virtual int valorid { get; set; }
        public virtual List<SubSubcategoria> SubCategories2 { get; set; }
    }
    public class SubSubcategoria
    {
        public virtual string codigo { get; set; }
        public virtual string parent { get; set; }
        public virtual string grandparent { get; set; }
        public virtual string urlpage { get; set; }
        public virtual string urlimagen { get; set; }
        public virtual string icon { get; set; }
        public virtual string description { get; set; }
        public virtual int valorid { get; set; }
        public virtual string SubCategoryName2 { get; set; }
    }
}