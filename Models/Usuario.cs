using CARS.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CARS
{
    public class Usuario: IEntity
    {
        #region Properties
        public long Id { get; set; }
        public string Mail { get; set; }

        [DisplayName("Contraseña"),DataType(DataType.Password)]
        public string Contrasenia { get; set; }
        public bool Activo { get; set; }
        [DisplayName("Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
        public TipoUsuario Tipo { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }
        #endregion

        #region Constructor
        public Usuario(string mail, string contrasenia, string nombre, string apellido, string telefono, DateTime fechaIngreso, TipoUsuario tipo)
        {
            Mail = mail;
            Contrasenia = contrasenia;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Activo = true;
            FechaIngreso = fechaIngreso;
            Tipo = tipo;
        }

        public Usuario()
        {
         
        }
        #endregion
    }
}