namespace CARS.Models
{
    public class ServicioIncidencia
    {

        #region Propiedades
        public long Id { get; set; }
        public Servicio Servicio { get; set; }
        public Incidencia Incidencia { get; set; }
        #endregion

        #region Constructores
        public ServicioIncidencia()
        {
        }

        public ServicioIncidencia(Servicio servicio, Incidencia incidencia)
        {
            Servicio = servicio;
            Incidencia = incidencia;
        }
        #endregion
    }
}