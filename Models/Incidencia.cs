using CARS.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CARS.Models
{
    public class Incidencia: IEntity
    {
        
        #region Properties
        public long Id { get; set; }
        public List<Servicio> Servicios { get; set; }
        public Usuario Usuario { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaSugerida { get; set; }
        public string Descripcion { get; set; }
        public string DireccionSugerida { get; set; }
        public long Kilometraje { get; set; }
        public Vehiculo Vehiculo { get; set; }
        #endregion


        #region Constructor
        public Incidencia()
        {
            Servicios = new List<Servicio>();
        }

        public Incidencia(List<Servicio> servicios, Usuario usuario, EstadoIncidencia estado, DateTime fechaInicio, DateTime fechaFin, DateTime fechaSugerida, string descripcion, string direccionSugerida, long kilometraje, Vehiculo vehiculo)
        {
            Servicios = servicios;
            Usuario = usuario;
            Estado = estado;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaSugerida = fechaSugerida;
            Descripcion = descripcion;
            DireccionSugerida = direccionSugerida;
            Kilometraje = kilometraje;
            Vehiculo = vehiculo;
        }


        #endregion
    }
}