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
using System.Data.Entity;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace CARS.Controllers
{
    public class IncidenciaController : Controller
    {
        Fachada fachada = new Fachada();
        private DbCARS db = new DbCARS();
        [HandleError(View = "Error")]
        // GET: Incidencia
        public ActionResult AgregarIncidencia()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    ViewBag.Vehiculo = fachada.GetVehiculos(long.Parse(Session["UserId"].ToString()));
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
        public ActionResult ReporteServicios(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                //Vehiculo v = fachada.GetVehiculoByMatricula(matricula);
                List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, fechaInicio, fechaFin);
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
                    
                    if (fachada.GetUsuarioRole(Session["UserId"].ToString()) == TipoUsuario.Administracion)
                    {
                        List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, DateTime.Now, DateTime.Now);

                        List<Servicio> listaServicios = new List<Servicio>();

                        foreach (Incidencia i in incidencias)
                        {
                            listaServicios.Add(fachada.GetServiciosIncidencia(i.Id));
                        }
                        return View(listaServicios);
                    }
                    else
                    {
                        throw new MyException("Usuario no Autorizado");
                    }
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
        [HandleError(View = "Error")]
        public void ExportToExcel([FromBody] IEnumerable<string> exportTable)
        {
            try
            {

                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) == TipoUsuario.Administracion || fachada.GetUsuarioRole(Session["UserId"].ToString()) == TipoUsuario.Director)
                {
                    if (exportTable != null)
                    {
                        List<IExportable> listaExport = new List<IExportable>();
                        List<Servicio> servicios = ParseoLista(exportTable);
                        foreach (var item in servicios)
                            listaExport.Add(item);
                        ExportExcel export = new ExportExcel();
                        string nombreArchivo = "Export_Reporte";
                        
                        ExportToExcel(listaExport, export, nombreArchivo);
                       // export.ExportExcel1(listaExport,this.Response, nombreArchivo);
                       // export.ToExcel(this.Response,listaExport);

                    }
                    else
                    {
                        throw new MyException("No hay SERVICIOS para exporta en el reporte");
                    } 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public HttpResponseBase ExportToExcel([FromBody]List<IExportable> lista, ExportExcel export, string nombreArchivo)
        {
            DataTable dt = export.ToDataTable(lista);
            var workbook = new XLWorkbook();

            workbook.Worksheets.Add(dt, "Reporte");

            string myName = Server.UrlEncode(nombreArchivo + ".xlsx");
            MemoryStream stream = GetStream(workbook);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + myName);
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
            return Response;
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
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
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Incidencia/Cancelar/5
        [HttpPost, ActionName("Cancelar")]
        [ValidateAntiForgeryToken]
        public ActionResult CalcelarConfirmed(long id)
        {
            try
            {
                //Las incidencias pendientes no tienen ningún servicio, por lo que siempre pueden cancelarse
                Incidencia incidencia = db.DbIncidencias.Find(id);
                if (incidencia.Estado == EstadoIncidencia.Procesando)
                {
                    List<Servicio> servicios = fachada.GetServiciosIncidencia(id);
                    foreach (Servicio s in servicios)
                    {
                        if (s.Estado != TipoEstado.Cancelado)
                        {
                            throw new MyException("No se puede borrar la incidencia ya que tiene SERVICIOS activos");
                            //RETORNAR MENSAJE DE ERROR.  NO SE PUEDE CANCELAR LA INCIDENCIA SI HAY SERVICIOS PENDIENTES O FINALIZADOS
                        }
                    }
                }
                db.Entry(incidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Incidencia/Edit/5
        public ActionResult Edit(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Incidencia incidencia = db.DbIncidencias.Find(id);
                if (incidencia == null)
                {
                    return HttpNotFound();
                }
                return View(incidencia);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([System.Web.Mvc.Bind(Include = "Id,FechaInicio,FechaFin,FechaSugerida,Descripcion,DireccionSugerida,Kilometraje")] Incidencia incidencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(incidencia).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Servicios/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Incidencia incidencia = db.DbIncidencias.Find(id);
                if (incidencia == null)
                {
                    return HttpNotFound();
                }
                return View(incidencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}