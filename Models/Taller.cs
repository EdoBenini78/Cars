﻿using CARS.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Taller: IEntity
    {
        #region Properties
        public long Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public long Rut { get; set; }
        [DisplayName("Nombre Contacto"), Required]
        public string NombreContacto { get; set; }
        [Required] public int Telefono{ get; set; }
        public bool Activo { get; set; }
        [DisplayName("Fecha de Ingreso"), DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        [Required] public string Direccion { get; set; }
        #endregion

        #region Constructor
        public Taller(string nombre, long rut, string nombreContacto, int telefono, long x, long y, string direccion)
        {
            Nombre = nombre;
            Rut = rut;
            NombreContacto = nombreContacto;
            Telefono = telefono;
            Activo = true;
            FechaIngreso = DateTime.Today;
            X = x;
            Y = y;
            Direccion = direccion;
        }

        public Taller()
        {
            Activo = true;
            FechaIngreso = DateTime.Today;
        }
        #endregion
    }
}