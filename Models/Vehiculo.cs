using CARS.Interfaces;
using System;
using System.Collections.Generic;

namespace CARS.Models
{
    public class Vehiculo: IEntity
    {
        #region Propertys
        public long Id { get; set; }
        public string Matricula { get; set; }
        public double Kilometros{ get; set; }
        public DateTime FechaDeCompra { get; set; }
        public int NumeroUnidad { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Motor { get; set; }
        public string Chasis { get; set; }
        public long Padron { get; set; }
        public TipoTraccion Traccion{ get; set; }
        public string Combustible { get; set; }
        public Localidad Localidad { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<Usuario> Choferes { get; set; }
        #endregion

        #region Constructor
        public Vehiculo(string matricula, double kilometros, DateTime fechaDeCompra, int numeroUnidad, string marca, string modelo, int anio, string motor, string chasis, long padron, TipoTraccion traccion, string combustible, Localidad localidad, DateTime fechaDeIngreso, List<Usuario> choferes)
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
            FechaIngreso = fechaDeIngreso;
            Choferes = choferes;
        }

        public Vehiculo()
        {
            Choferes = new List<Usuario>();
        }
        #endregion
    }
}