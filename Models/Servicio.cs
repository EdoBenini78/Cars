using CARS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Servicio: IEntity
    {
        #region Properties
        public long Id { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Taller Taller { get; set; }
        public TipoServicio Tipo{ get; set; }
        [DisplayName("Fecha de Agenda"), DataType(DataType.DateTime)]
        public DateTime FechaSugerida { get; set; }
        [DisplayName("Fecha de Entrada"), DataType(DataType.DateTime)]
        public DateTime? FechaEntrada { get; set; }
        [DisplayName("Fecha de Salida"), DataType(DataType.DateTime)]
        public DateTime? FechaSalida { get; set; }
        public TipoEstado Estado{ get; set; }
        public List<Factura> Facturas { get; set; }
        public string Descripcion { get; set; }
        [DisplayName("Número de Orden")]
        public string NumeroOrden { get; set; }
        #endregion

        #region Constructor
        public Servicio(Vehiculo vehiculo, Taller taller, TipoServicio tipo, DateTime fechaSugerida, DateTime fechaEntrada, DateTime fechaSalida, TipoEstado estado, List<Factura> facturas)
        {
            Vehiculo = vehiculo;
            Taller = taller;
            Tipo = tipo;
            FechaSugerida = fechaSugerida;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            Estado = estado;
            Facturas = facturas;
        }

        public Servicio()
        {
            Facturas = new List<Factura>();
        }
        #endregion
    }
}