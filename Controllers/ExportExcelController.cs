using CARS.Interfaces;
using CARS.Models;
using CARS.Utilities;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARS.Controllers
{
    public class ExportExcelController : Controller
    {
        Fachada fachada = new Fachada();
        private DbCARS db = new DbCARS();
        // GET: ExportExcel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReporteServicios()
        {
            try
            {
                if (Session["UserId"] != null)
                {

                    if (fachada.GetUsuarioRole(Session["UserId"].ToString()) == TipoUsuario.Administracion || fachada.GetUsuarioRole(Session["UserId"].ToString()) == TipoUsuario.Director)
                    {

                        List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, DateTime.MinValue, DateTime.MaxValue);

                        List<Servicio> listaServicios = new List<Servicio>();

                        foreach (Incidencia i in incidencias)
                        {
                            listaServicios.AddRange(fachada.GetServiciosIncidencia(i.Id));
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

        public ActionResult ReporteServicios(string fechaInicio, string fechaFin, string submitButton)
        {
            try
            {

                DateTime fechaI = new DateTime();
                DateTime fechaF = new DateTime();

                if (fechaInicio == "")
                {
                    fechaI = DateTime.MinValue;
                }
                else
                {
                    fechaI = DateTime.Parse(fechaInicio);
                }

                if (fechaFin == "")
                {
                    fechaF = DateTime.MaxValue;
                }
                else
                {
                    fechaF = DateTime.Parse(fechaFin);
                }

                List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, fechaI, fechaF);
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

                if (submitButton == "Filtrar")
                {
                    ViewBag.FechaInicio = fechaInicio;
                    ViewBag.FechaFin = fechaFin;
                    return View(listaServicios);
                }
                else if (submitButton == "Exportar")
                {
                    if (listaServicios.Count > 0)
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("Tipo", typeof(string));
                        table.Columns.Add("Fecha Sugerida", typeof(string));
                        table.Columns.Add("Hora", typeof(string));
                        table.Columns.Add("Fecha Entrada", typeof(string));
                        table.Columns.Add("Fecha Salida", typeof(string));
                        table.Columns.Add("Estado", typeof(string));
                        table.Columns.Add("Descripción", typeof(string));
                        table.Columns.Add("Número de orden", typeof(string));

                        // Add Three rows with those columns filled in the DataTable.
                        foreach (Servicio s in listaServicios)
                        {
                            table.Rows.Add(s.Tipo, s.FechaSugerida, s.Hora, s.FechaEntrada, s.FechaSalida, s.Estado, s.Descripcion, s.NumeroOrden);
                        }


                        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
                        wbook.Worksheets.Add(table, "tab1");
                        // Prepare the response
                        HttpResponseBase httpResponse = Response;
                        httpResponse.Clear();
                        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Provide you file name here
                        httpResponse.AddHeader("content-disposition", "attachment;filename=\"Samplefile.xlsx\"");

                        // Flush the workbook to the Response.OutputStream
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            wbook.SaveAs(memoryStream);
                            memoryStream.WriteTo(httpResponse.OutputStream);
                            memoryStream.Close();
                        }

                        httpResponse.End();
                        return View(listaServicios);
                    }
                    else
                    {
                        throw new MyException("No hay servicios para exportar en el período");
                    }
                }

                return View(listaServicios);

            }


            catch (Exception ex)
            {

                throw ex;
            }

        }

        //[HttpPost]
        //public void ExportExcel(string fechaInicio, string fechaFin)
        //{
        //    try
        //    {
        //        DateTime fechaI = new DateTime();
        //        DateTime fechaF = new DateTime();

        //        if (fechaInicio == "")
        //        {
        //            fechaI = DateTime.MinValue;
        //        }
        //        else
        //        {
        //            fechaI = DateTime.Parse(fechaInicio);
        //        }

        //        if (fechaFin == "")
        //        {
        //            fechaF = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            fechaF = DateTime.Parse(fechaFin);
        //        }

        //        List<Incidencia> incidencias = fachada.GetIncidenciasReporte(EstadoIncidencia.Finalizada, fechaI, fechaF);
        //        List<Servicio> listaServicios = new List<Servicio>();

        //        foreach (Incidencia i in incidencias)
        //        {
        //            List<Servicio> servicios = fachada.GetServiciosIncidencia(i.Id);
        //            if (servicios.Count() != 0)
        //                if (servicios.Count() != 0)
        //                {
        //                    listaServicios.AddRange(servicios);
        //                }
        //        }

        //        if (listaServicios.Count > 0)
        //        {
        //            DataTable table = new DataTable();
        //            table.Columns.Add("Tipo", typeof(string));
        //            table.Columns.Add("Fecha Sugerida", typeof(string));
        //            table.Columns.Add("Hora", typeof(string));
        //            table.Columns.Add("Fecha Entrada", typeof(string));
        //            table.Columns.Add("Fecha Salida", typeof(string));
        //            table.Columns.Add("Estado", typeof(string));
        //            table.Columns.Add("Descripción", typeof(string));
        //            table.Columns.Add("Número de orden", typeof(string));

        //            // Add Three rows with those columns filled in the DataTable.
        //            foreach (Servicio s in listaServicios)
        //            {
        //                table.Rows.Add(s.Tipo, s.FechaSugerida, s.Hora, s.FechaEntrada, s.FechaSalida, s.Estado, s.Descripcion, s.NumeroOrden);


        //            }




        //            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        //            wbook.Worksheets.Add(table, "tab1");
        //            // Prepare the response
        //            HttpResponseBase httpResponse = Response;
        //            httpResponse.Clear();
        //            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            //Provide you file name here
        //            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Samplefile.xlsx\"");

        //            // Flush the workbook to the Response.OutputStream
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                wbook.SaveAs(memoryStream);
        //                memoryStream.WriteTo(httpResponse.OutputStream);
        //                memoryStream.Close();
        //            }

        //            httpResponse.End();
        //        }


        //    }


        //catch (Exception ex)
        //{

        //    throw ex;
        //}

        //}


    }
}