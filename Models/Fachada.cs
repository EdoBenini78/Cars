using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CARS.Models
{
    public class Fachada
    {
        DbCARS db = new DbCARS();
        #region Usuario
        public Usuario GetUsuarioBYDbId(long id)
        {
            Usuario pUsuario = db.DbUsuarios.Find(id);
            return pUsuario;
        }

        public Usuario ValidarLogIn(string aPass, string aMail)
        {
            Usuario pUsuario = db.DbUsuarios.Where(us => us.Mail == aMail && us.Contrasenia == aPass).FirstOrDefault();
            return pUsuario;
        }

        public bool UpdateUsuario(Usuario aUsuario)
        {
            Usuario pUsuario = GetUsuarioBYDbId(aUsuario.Id);
            if (pUsuario != null)
            {
                pUsuario = aUsuario;
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public bool VerificarMailExistente(string aMail)
        {
            Usuario pUsuario = db.DbUsuarios.Where(us => us.Mail == aMail).FirstOrDefault();
            return pUsuario != null ? true : false;
        }

        public bool InsertarUsuario(Usuario aUsuario)
        {
            if (VerificarMailExistente(aUsuario.Mail))
            {
                db.DbUsuarios.Add(aUsuario);
                return true;
            }
            return false;
        }
        #endregion

        #region Vehiculo
        public Vehiculo GetVehiculoByDbId(long id)
        {
            Vehiculo pVehiculo = db.DbVehiculos.Find(id);
            return pVehiculo;
        }

        public List<Usuario> GetListaChoferesParaVehiculo(long idVehiculo)
        {
            List<Usuario> usuarios = db.DbUsuarios.Where(u => u.Tipo == TipoUsuario.Chofer && u.Activo == true).ToList();
            List<VehiculoChofer> choferes = db.DbVehiculoChofer.Where(v => v.Activo == true).ToList();
            foreach (var item in choferes)
            {
                if (item.Vehiculo.Id == idVehiculo)
                {
                    usuarios.Remove(item.Chofer);
                }
            }
            return usuarios;
        }

        public Vehiculo GetVehiculoByMatricula(string matricula)
        {
            Vehiculo pVehiculo = db.DbVehiculos.Where(v => v.Matricula == matricula).FirstOrDefault();
            return pVehiculo;
        }

        public Vehiculo GetVehiculoByChofer(long aUserId)
        {
            VehiculoChofer pVehiculoChofer = db.DbVehiculoChofer.Include("Vehiculo").Where(vc => vc.Chofer.Id == aUserId).FirstOrDefault();
            if (pVehiculoChofer != null)
            {
                return pVehiculoChofer.Vehiculo;
            }
            return null;
        }

        public bool UpdateVehiculo(Vehiculo aVehiculo)
        {
            Vehiculo pVehiculo = GetVehiculoByDbId(aVehiculo.Id);
            if (pVehiculo != null)
            {
                pVehiculo = aVehiculo;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AgregarChoferaVehiculo(Usuario aChofer, Vehiculo aVehiculo)
        {
            Usuario pChofer = GetUsuarioBYDbId(aChofer.Id);
            Vehiculo pVehiculo = GetVehiculoByDbId(aVehiculo.Id);
            if (aVehiculo != null && aChofer != null)
            {
                VehiculoChofer vehiculoChofer = new VehiculoChofer(pChofer, pVehiculo);
                db.DbVehiculoChofer.Add(vehiculoChofer);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ControlVehiculoExistenes(Vehiculo aVehiculo)
        {
            Vehiculo pVechiculoChasis = db.DbVehiculos.Where(v => v.Chasis == aVehiculo.Chasis).FirstOrDefault();
            Vehiculo pVechiculoMatricula = db.DbVehiculos.Where(v => v.Matricula == aVehiculo.Matricula).FirstOrDefault();
            Vehiculo pVechiculoMotor = db.DbVehiculos.Where(v => v.Motor == aVehiculo.Motor).FirstOrDefault();
            Vehiculo pVechiculoPadron = db.DbVehiculos.Where(v => v.Padron == aVehiculo.Padron).FirstOrDefault();
            Vehiculo pVechiculoNumeroUnidad = db.DbVehiculos.Where(v => v.NumeroUnidad == aVehiculo.NumeroUnidad).FirstOrDefault();

            if (pVechiculoChasis != null && pVechiculoMatricula != null && pVechiculoMotor != null && pVechiculoPadron != null && pVechiculoNumeroUnidad != null)
            {
                return true;
            }
            return false;
        }

        public bool AgregarVehiculo(Vehiculo aVehiculo)
        {
            if (ControlVehiculoExistenes(aVehiculo))
            {
                db.DbVehiculos.Add(aVehiculo);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Localidad
        public Localidad GetLocalidadByDbId(long id)
        {
            Localidad pLocalidad = db.DbLocalidades.Find(id);
            return pLocalidad;
        }

        public bool AgregarLocalidad(Localidad aLocalidad)
        {
            Localidad pLocalidad = db.DbLocalidades.Where(l => l.Departamento == aLocalidad.Departamento && l.Ciudad == aLocalidad.Ciudad).FirstOrDefault();
            if (pLocalidad == null)
            {
                db.DbLocalidades.Add(pLocalidad);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Taller
        public Taller GetTallerByDbId(long id)
        {
            Taller pTaller = db.DbTalleres.Find(id);
            return pTaller;
        }

        public bool UpdateTaller(Taller aTaller)
        {
            Taller pTaller = GetTallerByDbId(aTaller.Id);
            if (pTaller != null)
            {
                pTaller = aTaller;
                db.SaveChanges();
                return true;
            }
            return false;
        }


        #endregion

        #region Incidencia
        public Incidencia GetIncidenciaByDbId(long id)
        {
            Incidencia aIncidencia = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Id == id).FirstOrDefault();
            return aIncidencia;
        }

        internal dynamic GetListaIncidenciasChofer(long aUserId, EstadoIncidencia estado)
        {
            Vehiculo vehiculoChofer = GetVehiculoByChofer(aUserId);
            List<Incidencia> incidencias = null;
            if (vehiculoChofer != null)
            {
                incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.Vehiculo.Id == vehiculoChofer.Id).ToList();

            }
            else
            {
                //mandar error
            }
            return incidencias;
        }

        internal dynamic GetListaIncidencias(EstadoIncidencia estado)
        {
            List<Incidencia> incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado).ToList();
            return incidencias;
        }

        public bool AgregarIncidencia(DateTime fechaSugerida, long pKm, string pDireccion, string pMatricula, string pComentario, long pUsuario, double lng, double lat)
        {
            Vehiculo aVehiculo = db.DbVehiculos.Where(v => v.Matricula == pMatricula).FirstOrDefault();
            Usuario aUsuario = db.DbUsuarios.Find(pUsuario);
            Incidencia aIncidencia = new Incidencia(fechaSugerida, pKm, pDireccion, aVehiculo, pComentario, aUsuario,lng,lat);
            db.DbIncidencias.Add(aIncidencia);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        internal dynamic ListarIncidencia(long pUserId)
        {
            List<Incidencia> incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Usuario.Id == pUserId).ToList();
            return incidencias;
        }


        internal dynamic GetIncidenciasReporte(EstadoIncidencia estado, DateTime? fechaInicio, DateTime? fechaFin, Vehiculo vehiculo)
        {
            List<Incidencia> incidencias = new List<Incidencia>();

            if (vehiculo == null)
            {
                if (fechaInicio == null && fechaFin == null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado).ToList();
                }
                if (fechaInicio != null && fechaFin == null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaInicio >= fechaInicio).ToList();
                }
                if (fechaInicio == null && fechaFin != null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaFin <= fechaFin).ToList();
                }
                if (fechaInicio != null && fechaFin != null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaInicio>=fechaInicio && i.FechaFin <= fechaFin).ToList();
                }
            }
            else
            {
                if (fechaInicio == null && fechaFin == null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.Vehiculo==vehiculo).ToList();

                }
                if (fechaInicio != null && fechaFin == null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaInicio >= fechaInicio && i.Vehiculo == vehiculo).ToList();
                }
                if (fechaInicio == null && fechaFin != null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaFin <= fechaFin && i.Vehiculo == vehiculo).ToList();
                }
                else if (fechaInicio != null && fechaFin != null)
                {
                    incidencias = db.DbIncidencias.Include("Vehiculo").Include("Usuario").Where(i => i.Estado == estado && i.FechaInicio >= fechaInicio && i.FechaFin <= fechaFin && i.Vehiculo == vehiculo).ToList();
                }
            }
            
                                           
            return incidencias;
        }

        internal dynamic GetServiciosIncidencia(long idIncidencia)
        {
            List<ServicioIncidencia> serviciosIncidencia = db.DbServicioDeIncidencia.Include("Servicio").Include("Incidencia").Where(i => i.Incidencia.Id == idIncidencia).ToList();

            List<Servicio> servicios = new List<Servicio>();

            foreach (ServicioIncidencia si in serviciosIncidencia)
            {
                servicios.Add(GetServicioById(si.Servicio.Id));
            }
            
           return servicios;
        }

       

        internal void AgregarIncidencia()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Servicio
        public Servicio GetServicioById(long id)
        {
            return db.DbServicios.Include("Vehiculo").Include("Taller").Where(s => s.Id == id).FirstOrDefault();
        }

        #endregion
        public double CalcularDistancia(Incidencia aIncidencia, Taller aTaller)
        {
            double lng = Math.Pow((aIncidencia.Longitud - aTaller.Longitud), 2);
            double lat = Math.Pow((aIncidencia.Latitud - aTaller.Latitud), 2);

            return Math.Sqrt(lng + lat);
        }

        public List<Taller> GetTalleresDistanciaOk(Incidencia aIncidencia)
        {
            var talleres = db.DbTalleres.ToList();
            List<Taller> talleresOk = new List<Taller>();

            foreach (var item in talleres)
            {
                if (CalcularDistancia(aIncidencia,item) < 1.30)
                {
                    talleresOk.Add(item);
                }
            }

            return talleresOk;
        }

    }
}