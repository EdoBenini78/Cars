using CARS.Interfaces;
using System;

namespace CARS
{
    public class Usuario: IEntity
    {
        #region Propertys
        public long Id { get; set; }
        public string Mail { get; set; }
        public string Contrasenia { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public TipoUsuario Tipo { get; set; }
        #endregion

        #region Constructor
        public Usuario(string mail, string contrasenia, DateTime fechaIngreso, TipoUsuario tipo)
        {
            Mail = mail;
            Contrasenia = contrasenia;
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