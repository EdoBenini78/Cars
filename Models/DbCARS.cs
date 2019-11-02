using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CARS.Models
{
    public class DbCARS: DbContext
    {
        public DbSet<Usuario> DbUsuarios { get; set; }
        public DbSet<Vehiculo> DbVehiculos{ get; set; }
        public DbSet<Localidad> DbLocalidades { get; set; }
        public DbSet<Taller> DbTalleres { get; set; }
        public DbSet<Incidencia> DbInsidencias{ get; set; }
        public DbSet<Servicio> DbServicios{ get; set; }
        public DbSet<Factura> DbFacturas{ get; set; }
        public DbSet<ServicioIncidencia> DbServicioDeIncidencia { get; set; }
    }
}