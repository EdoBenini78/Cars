using CARS.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace CARS.Models
{
    public class Incidencia: IEntity
    {
        #region Properties
        public long Id { get; set; }
        public Usuario Usuario { get; set; }
        public EstadoIncidencia Estado { get; set; }
        [DisplayName("Fecha de Inicio"), DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [DisplayName("Fecha de Fin"), DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }
        [DisplayName("Fecha de Sugerida"), DataType(DataType.Date)]
        public DateTime FechaSugerida { get; set; }
        public string Descripcion { get; set; }
        [DisplayName("Direccion Sugerida")]
        public string DireccionSugerida { get; set; }
        public long Kilometraje { get; set; }
        public Vehiculo Vehiculo { get; set; }
        #endregion


        #region Constructor
        public Incidencia()
        {
            FechaInicio = DateTime.Now;
            Estado = EstadoIncidencia.Procesando;
        }

        public Incidencia(Usuario usuario, EstadoIncidencia estado, DateTime fechaInicio, DateTime fechaFin, DateTime fechaSugerida, string descripcion, string direccionSugerida, long kilometraje, Vehiculo vehiculo)
        {
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

        public Incidencia(DateTime fechaSugerida, long pKm, string pDireccion, Vehiculo aVehiculo, string pComentario, Usuario aUsuario)
        {
            FechaSugerida = fechaSugerida;
            Kilometraje = pKm;
            DireccionSugerida = pDireccion;
            Vehiculo = aVehiculo;
            Descripcion = pComentario;
            FechaInicio = DateTime.Today;
            Estado = EstadoIncidencia.Procesando;
            Usuario = aUsuario;
        }

        #endregion
    }
}