using CARS.Interfaces;

namespace CARS.Models
{
    public class Localidad: IEntity
    {
        
        #region Properties
        public long Id { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        #endregion


        #region Constructor
        public Localidad(string departamento, string ciudad)
        {
            Departamento = departamento;
            Ciudad = ciudad;
        }

        public Localidad()
        {
            
        }
        #endregion
    }
}