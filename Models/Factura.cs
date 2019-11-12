using CARS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Factura: IEntity
    {
        #region Properties
        public long Id { get; set; }
        [Required]
        public double Monto { get; set; }
        [Required]
        public long Numero { get; set; }

        #endregion


        #region Constructores
        public Factura(double monto, long numero)
        {
            Monto = monto;
            Numero = numero;
        }

        public Factura()
        {
            
        }
        #endregion


    }
}