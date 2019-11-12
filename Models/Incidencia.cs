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
        #endregion


        #region Constructor
        public Incidencia(List<Servicio> servicios, Usuario usuario)
        {
            Servicios = servicios;
            Usuario = usuario;
            Estado = EstadoIncidencia.EnCurso;
            FechaInicio = DateTime.Now;
        }

        public Incidencia()
        {
            Servicios = new List<Servicio>();
        }
        #endregion
    }
}