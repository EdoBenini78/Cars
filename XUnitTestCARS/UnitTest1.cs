using CARS;
using CARS.Controllers;
using CARS.Models;
using System;
using Xunit;


namespace XUnitTestCARS
{
    public class UnitTest1
    {
        [Fact]
        public void CrearVehiculos()
        {
            var vehiculo = VehiculosController.("mat1", 1, DateTime.Today, 1, "marca1", "modelo1", 2020, "1111", "1111", 1111, TipoTraccion.Total, "1/8", null, DateTime.Today, null);
            var usuario = new Usuario(mail, "1111", "nombre", "apellido", "telefono", DateTime.Today, TipoUsuario.Administracion);


            Assert.NotNull(vehiculo);
            Assert.NotNull(usuario);
        }
    }
}
