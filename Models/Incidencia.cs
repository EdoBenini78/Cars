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
        [DisplayName("Fecha Inicio"), DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [DisplayName("Fecha Fin"), DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }
        [DisplayName("Fecha Sugerida"), DataType(DataType.Date)]
        public DateTime FechaSugerida { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Dirección Sugerida")]
        public string DireccionSugerida { get; set; }
        [DisplayName("Km")]
        public long Kilometraje { get; set; }
        [DisplayName("Vehículo")]
        public Vehiculo Vehiculo { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
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

        public Incidencia(DateTime fechaSugerida, long pKm, string pDireccion, Vehiculo aVehiculo, string pComentario, Usuario aUsuario, double longitud, double latitud)
        {
            FechaSugerida = fechaSugerida;
            Kilometraje = pKm;
            DireccionSugerida = pDireccion;
            Vehiculo = aVehiculo;
            Descripcion = pComentario;
            FechaInicio = DateTime.Today;
            Estado = EstadoIncidencia.Pendiente;
            Usuario = aUsuario;
            Latitud = latitud;
            Longitud = longitud;
        }

        #endregion
    }
}