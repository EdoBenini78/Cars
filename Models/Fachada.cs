﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool UpdateUsuario (Usuario aUsuario)
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

        public bool InsertarUsuario (Usuario aUsuario)
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
                //aVehiculo.Choferes.Add(pChofer);
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
            Incidencia aIncidencia = db.DbIncidencias.Find(id);
            return aIncidencia;
        }

        internal dynamic GetListaIncidencias(long aUserId)
        {
            Usuario us = GetUsuarioBYDbId(aUserId);
            List<Incidencia> incidencias = db.DbIncidencias.Where(i => i.Estado == EstadoIncidencia.Procesando && i.Usuario.Id == us.Id).ToList();
            return incidencias;
        }

        public bool AgregarIncidencia(DateTime fechaSugerida, long pKm, string pDireccion, string pMatricula, string pComentario, long pUsuario)
        {
            Vehiculo aVehiculo = db.DbVehiculos.Where(v => v.Matricula == pMatricula).FirstOrDefault();
            Usuario aUsuario = db.DbUsuarios.Find(pUsuario);
            Incidencia aIncidencia = new Incidencia(fechaSugerida, pKm, pDireccion, aVehiculo, pComentario, aUsuario);
            db.DbIncidencias.Add(aIncidencia);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        internal dynamic ListarIncidencia(long pUserId)
        {
            List<Incidencia> incidencias = db.DbIncidencias.Where(i => i.Usuario.Id == pUserId).ToList();
            return incidencias;
        }

        internal void AgregarIncidencia()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}