using CARS.Interfaces;
using System;

namespace CARS.Models
{
    public class Taller: IEntity
    {
        #region Propertys
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long Rut { get; set; }
        public string NombreContacto { get; set; }
        public int Telefono{ get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public string Direccion { get; set; }
        #endregion

        #region Constructor
        public Taller(string nombre, long rut, string nombreContacto, int telefono, DateTime fechaIngreso, long x, long y, string direccion)
        {
            Nombre = nombre;
            Rut = rut;
            NombreContacto = nombreContacto;
            Telefono = telefono;
            Activo = true;
            FechaIngreso = fechaIngreso;
            X = x;
            Y = y;
            Direccion = direccion;
        }

        public Taller()
        {
            Activo = true;
        }
        #endregion
    }
}