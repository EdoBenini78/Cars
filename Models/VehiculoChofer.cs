using CARS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARS.Models
{
    public class VehiculoChofer : IEntity
    {
        #region Properties
        public long Id { get; set; }
        public Usuario Chofer { get; set; }
        public Vehiculo Vehiculo { get; set; }
        #endregion

        #region Contructor
        public VehiculoChofer()
        {
        }

        public VehiculoChofer(Usuario chofer, Vehiculo vehiculo)
        {
            Chofer = chofer;
            Vehiculo = vehiculo;
        }
        #endregion
    }
}