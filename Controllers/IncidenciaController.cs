using CARS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CARS.Export_Excel;
using System.Web.Mvc;
using CARS.Interfaces;

namespace CARS.Controllers
{
    public class IncidenciaController : Controller
    {
        Fachada fachada = new Fachada();
        // GET: Incidencia
        public ActionResult AgregarIncidencia()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.Vehiculo = fachada.GetVehiculoByChofer(long.Parse(Session["UserId"].ToString()));
                return View();
            }
            return RedirectToAction("LogIn", "LogIn");        
        }

        //Método para crear incidencia del usuario del vehículo

        public ActionResult InsertIncidencia(string FechaSugerida, string km, string dir,string matricula, string com, string longitud, string latitud)
        {
            Vehiculo aVehiculo = fachada.GetVehiculoByMatricula(matricula);
            long edo = long.Parse(Session["UserId"].ToString());
            longitud = longitud.Replace(".",",");
            latitud = latitud.Replace(".", ",");
            if (aVehiculo != null)
            {
                fachada.AgregarIncidencia(DateTime.Parse(FechaSugerida), long.Parse(km), dir, matricula, com, long.Parse(Session["UserId"].ToString()), double.Parse(longitud), double.Parse(latitud));
            }
            else
            {
                return RedirectToAction("AgregarIncidencia", "Incidencia");
            }            
            return RedirectToAction("Index","Home");
        }

        public ActionResult ListaIncidencias()
        {
            ViewBag.ListaIncidencia = fachada.ListarIncidencia(long.Parse(Session["UserId"].ToString()));
            return View();
        }

        [HttpPost]
        public ActionResult ReporteServicios(DateTime? fechaInicio, DateTime? fechaFin, string matricula)
        {
            Vehiculo v = fachada.GetVehiculoByMatricula(matricula);

            List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, fechaInicio, fechaFin, v);
            
            List<Servicio> listaServicios = new List<Servicio>();

            foreach (Incidencia i in incidencias)
            {
                List<Servicio> servicios = fachada.GetServiciosIncidencia(i.Id);
                if (servicios.Count() != 0)
                {
                    listaServicios.AddRange(servicios);
                }              
            }
                      
                        
            return View(listaServicios);
        }
        [HttpGet]
        public ActionResult ReporteServicios()
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
            return RedirectToAction("LogIn", "LogIn");

            
        }

        [HttpPost]
        public ActionResult ExportToExcel(IEnumerable<Servicio> lista)
        {
            List<IExportable> listaExport = new List<IExportable>();
            //foreach (var item in lista)
            //    listaExport.Add(item);
            ExportExcel export = new ExportExcel();
            string nombreAArchivo = "Export_Reporte";
            export.ExportToExcel(listaExport, this.Server, this.Response, nombreAArchivo);
            return View();
        }
    }
}