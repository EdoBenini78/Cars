using CARS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Vehiculo: IEntity
    {
        #region Properties
        public long Id { get; set; }
        [Required]public string Matricula { get; set; }
        [Required] public double Kilometros{ get; set; }
        [DisplayName("Fecha de compra"), DataType(DataType.Date),Required]
        public DateTime FechaDeCompra { get; set; }
        [DisplayName("Unidad")]
        [Required] public int NumeroUnidad { get; set; }
        [Required] public string Marca { get; set; }
        [Required] public string Modelo { get; set; }
        [DisplayName("Año"),Required]
        public int Anio { get; set; }
        [Required] public string Motor { get; set; }
        [Required] public string Chasis { get; set; }
        [Required] public long Padron { get; set; }
        [Required] public TipoTraccion Traccion{ get; set; }
        [Required] public string Combustible { get; set; }
        public Localidad Localidad { get; set; }
       
        public bool Activo { get; set; }
        [DisplayName("Fecha de Ingreso"), DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        //public List<Usuario> Choferes { get; set; }
        #endregion

        #region Constructor
        public Vehiculo(string matricula, double kilometros, DateTime fechaDeCompra, int numeroUnidad, string marca, string modelo, int anio, string motor, string chasis, long padron, TipoTraccion traccion, string combustible, Localidad localidad, DateTime fechaIngreso, List<Usuario> choferes)
        {
            Matricula = matricula;
            Kilometros = kilometros;
            FechaDeCompra = fechaDeCompra;
            NumeroUnidad = numeroUnidad;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Motor = motor;
            Chasis = chasis;
            Padron = padron;
            Traccion = traccion;
            Combustible = combustible;
            Localidad = localidad;
            Activo = true;
            FechaIngreso = fechaIngreso;
            //Choferes = choferes;
        }

        public Vehiculo()
        {
            //Choferes = new List<Usuario>();
        }
        #endregion

        #region Metodos
        public string MiNombre()
        {
            return this.Matricula;
        }
        #endregion
    }
}