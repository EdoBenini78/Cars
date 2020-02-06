using CARS.Models;
using CARS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARS.Controllers
{
    public class HomeController : Controller
    {
        Fachada fachada = new Fachada();
        [HandleError(View = "Error")]
        public ActionResult Index()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Usuario usuario = fachada.GetUsuarioBYDbId(long.Parse(Session["UserId"].ToString()));
                    if (usuario.Tipo == TipoUsuario.Chofer)
                    {
                        return View(fachada.GetListaIncidenciasChofer(long.Parse(Session["UserId"].ToString()), EstadoIncidencia.Pendiente));
                    }

                    List<Incidencia> incidenciasPendientes = fachada.GetListaIncidencias(EstadoIncidencia.Pendiente);
                    ViewBag.IncidenciasPendientes = incidenciasPendientes.Count;
                    ViewBag.BtnFiltrar = 0;
                    return View(fachada.GetListaIncidencias(EstadoIncidencia.Pendiente));
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

        public ActionResult MostrarListaIncidencia(string id)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Usuario usuario = fachada.GetUsuarioBYDbId(long.Parse(Session["UserId"].ToString()));
                    EstadoIncidencia estadoIncidencia = (EstadoIncidencia)Enum.ToObject(typeof(EstadoIncidencia), int.Parse(id));
                    if (usuario.Tipo == TipoUsuario.Chofer)
                    {
                        return PartialView("ListadoIncidenciasHome", fachada.GetListaIncidenciasChofer(long.Parse(Session["UserId"].ToString()), estadoIncidencia));
                    }
                    return PartialView("ListadoIncidenciasHome", fachada.GetListaIncidencias(estadoIncidencia));
                    //return la partialView con un select tab
                }
                else
                {
                    throw new MyException("Han caducado las credenciales, por favor ingreselas nuevamente");
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Servicio(string id)
        {
            return RedirectToAction("Create", "Servicios", new { id = int.Parse(id)});
        }

        public ActionResult VerServicios(string id)
        {
            return RedirectToAction("VerServicios", "Servicios", new { id = id });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Todavia no tenemos nada en la base de datos.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BuscarIncidenciaPorMatricula (string matricula, EstadoIncidencia estado)
        {
            try
            {
                List<Incidencia> incidencias = fachada.GetListaIncidencias(estado);
                if (matricula != "")
                {
                    incidencias = fachada.GetListaIncidenciasPorMatriculaYEstado(matricula, estado);
                }

                return View(incidencias);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Ayuda()
        {
            return View("Ayuda");
        }

        public FileResult GetReport()
        {
            string ReportURL = "C:/Users/Usuario/Desktop/CARS/Docs/documento302.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
    }
}