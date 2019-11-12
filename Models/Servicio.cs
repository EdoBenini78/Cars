using CARS.Interfaces;
using System;
using System.Collections.Generic;

namespace CARS.Models
{
    public class Servicio: IEntity
    {
        #region Properties
        public long Id { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Taller Taller { get; set; }
        public TipoServicio Tipo{ get; set; }
        public DateTime FechaSugerida { get; set; }
        public DateTime FechaEntrata { get; set; }
        public DateTime FechaSalida { get; set; }
        public TipoEstado Estado{ get; set; }
        public List<Factura> Facturas { get; set; }
        #endregion

        #region Constructor
        public Servicio(Vehiculo vehiculo, Taller taller, TipoServicio tipo, DateTime fechaSugerida, DateTime fechaEntrata, DateTime fechaSalida, TipoEstado estado, List<Factura> facturas)
        {
            Vehiculo = vehiculo;
            Taller = taller;
            Tipo = tipo;
            FechaSugerida = fechaSugerida;
            FechaEntrata = fechaEntrata;
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