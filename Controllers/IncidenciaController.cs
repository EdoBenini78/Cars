using CARS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CARS.Export_Excel;
using System.Web.Mvc;
using CARS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using System.Text.RegularExpressions;
using System.Net;
using ActionNameAttribute = System.Web.Mvc.ActionNameAttribute;
using CARS.Utilities;

namespace CARS.Controllers
{
    public class IncidenciaController : Controller
    {
        Fachada fachada = new Fachada();
        private DbCARS db = new DbCARS();
        
        // GET: Incidencia
        public ActionResult AgregarIncidencia()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    ViewBag.Vehiculo = fachada.GetVehiculoByChofer(long.Parse(Session["UserId"].ToString()));
                    return View();
                }
                else
                {
                    throw new MyException("Han caducado las credenciales, por favor ingreselas nuevamente");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Método para crear incidencia del usuario del vehículo

        public ActionResult InsertIncidencia(string FechaSugerida, string km, string dir,string matricula, string com, string longitud, string latitud)
        {
            try
            {
                Vehiculo aVehiculo = fachada.GetVehiculoByMatricula(matricula);
                long edo = long.Parse(Session["UserId"].ToString());
                longitud = longitud.Replace(".", ",");
                latitud = latitud.Replace(".", ",");
                if (aVehiculo != null)
                {
                    fachada.AgregarIncidencia(DateTime.Parse(FechaSugerida), long.Parse(km), dir, matricula, com, long.Parse(Session["UserId"].ToString()), double.Parse(longitud), double.Parse(latitud));
                }
                else
                {
                    throw new MyException("No se encontro el Vehiculo");
                    //return RedirectToAction("AgregarIncidencia", "Incidencia");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult ListaIncidencias()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    ViewBag.ListaIncidencia = fachada.ListarIncidencia(long.Parse(Session["UserId"].ToString()));
                    return View();
                }
                else
                {
                    throw new MyException("Han caducado las credenciales, por favor ingreselas nuevamente");
                }                  
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ReporteServicios(DateTime? fechaInicio, DateTime? fechaFin, string matricula)
        {
            try
            {
                Vehiculo v = fachada.GetVehiculoByMatricula(matricula);

                List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, fechaInicio, fechaFin, v);

                List<Servicio> listaServicios = new List<Servicio>();

                foreach (Incidencia i in incidencias)
                {
                    List<Servicio> servicios = fachada.GetServiciosIncidencia(i.Id);
                    if (servicios.Count() != 0)
                        if (servicios.Count() != 0)
                        {
                            listaServicios.AddRange(servicios);
                        }
                }

                ViewBag.BtnFiltrar = 1;
                return View(listaServicios);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public ActionResult ReporteServicios()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, DateTime.Today, DateTime.Today, null);

                    List<Servicio> listaServicios = new List<Servicio>();

                    foreach (Incidencia i in incidencias)
                    {
                        listaServicios.Add(fachada.GetServiciosIncidencia(i.Id));
                    }
                    return View(listaServicios);
                }
                else
                {
                    throw new MyException("Han caducado las credenciales, por favor ingreselas nuevamente");
                }
                //return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }

        [HttpPost]
        public void ExportToExcel([FromBody] IEnumerable<string> exportTable)
        {
            List<IExportable> listaExport = new List<IExportable>();
            List<Servicio> servicios = ParseoLista(exportTable);
            foreach (var item in servicios)
                listaExport.Add(item);
            ExportExcel export = new ExportExcel();
            string nombreAArchivo = "Export_Reporte";
            export.ExportToExcel(listaExport, this.Server, this.Response, nombreAArchivo);
        }

        private List<Servicio> ParseoLista(IEnumerable<string> exportTable)
        {
            List<Servicio> servicios = new List<Servicio>();
            foreach (var item in exportTable)
            {
                var matches = Regex.Matches(item, "[0-9]+");
                string idServicio = "";
                foreach (var num in matches)
                {
                    idServicio += num.ToString();
                }
                Servicio servicio = fachada.GetServicioById(long.Parse(idServicio.ToString()));
                servicios.Add(servicio);
            }
            
            return servicios;

        }

        // GET: Incidencias/Cancelar/5
        public ActionResult Cancelar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia i = db.DbIncidencias.Find(id);
            if (i == null)
            {
                return HttpNotFound();
            }
            return View(i);
        }

        // POST: Incidencia/Cancelar/5
        [HttpPost, ActionName("Cancelar")]
        [ValidateAntiForgeryToken]
        public ActionResult CalcelarConfirmed(long id)
        {
            //Las incidencias pendientes no tienen ningún servicio, por lo que siempre pueden cancelarse
            Incidencia i = db.DbIncidencias.Find(id);
            if (i.Estado == EstadoIncidencia.Procesando)
            {
                List<Servicio> servicios = fachada.GetServiciosIncidencia(id);
                foreach (Servicio s in servicios)
                {
                    if (s.Estado != TipoEstado.Cancelado)
                    {
                        //RETORNAR MENSAJE DE ERROR.  NO SE PUEDE CANCELAR LA INCIDENCIA SI HAY SERVICIOS PENDIENTES O FINALIZADOS
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}