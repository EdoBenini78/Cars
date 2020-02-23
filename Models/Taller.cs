using CARS.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Taller: IEntity,IExportable
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
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        [Required] public string Direccion { get; set; }
        #endregion

        #region Constructor
        public Taller(string nombre, long rut, string nombreContacto, int telefono, double longitud, double latitud, string direccion)
        {
            Nombre = nombre;
            Rut = rut;
            NombreContacto = nombreContacto;
            Telefono = telefono;
            Activo = true;
            FechaIngreso = DateTime.Today;
            Direccion = direccion;

            //En la web hay que fijarse si toma con coma
            if (!latitud.ToString().Contains("."))
            {
                string lat = latitud.ToString().Substring(0, 2) + "." + latitud.ToString().Substring(2);
                Latitud = double.Parse(lat);
            }
            else
            {
                Latitud = latitud;
            }

            if (!longitud.ToString().Contains("."))
            {
                string lon = longitud.ToString().Substring(0, 2) + "." + longitud.ToString().Substring(2);
                Longitud = double.Parse(lon);
            }
            else
            {
                Longitud = longitud;
            }


         
           
        }

        public Taller()
        {
            Activo = true;
            FechaIngreso = DateTime.Today;
        }
        #endregion

        #region Metodos
        public string MiNombre()
        {
            return this.Nombre;
        }
        #endregion
    }
}