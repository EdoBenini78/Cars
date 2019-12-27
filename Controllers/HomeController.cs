using CARS.Models;
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
        public ActionResult Index()
        {   
            if (Session["UserId"] != null)
            {
                Usuario usuario = fachada.GetUsuarioBYDbId(long.Parse(Session["UserId"].ToString()));
                if (usuario.Tipo == TipoUsuario.Chofer)
                {
                    return View(fachada.GetListaIncidenciasChofer(long.Parse(Session["UserId"].ToString()),EstadoIncidencia.Pendiente));
                }

                List<Incidencia> incidenciasPendientes = fachada.GetListaIncidencias(EstadoIncidencia.Pendiente);
                ViewBag.IncidenciasPendientes = incidenciasPendientes.Count;
                return View(fachada.GetListaIncidencias(EstadoIncidencia.Pendiente));
                //return la partialView con un select tab
            }
            else
            {
                //mensaje de error
                return RedirectToAction("LogIn", "LogIn");
            }
            
        }

        public ActionResult MostrarListaIncidencia(string id)
        {
            if (Session["UserId"] != null)
            {
                Usuario usuario = fachada.GetUsuarioBYDbId(long.Parse(Session["UserId"].ToString()));
                EstadoIncidencia estadoIncidencia = (EstadoIncidencia)Enum.ToObject(typeof(EstadoIncidencia), int.Parse(id));
                if (usuario.Tipo == TipoUsuario.Chofer)
                {
                    return PartialView("ListadoIncidenciasHome",fachada.GetListaIncidenciasChofer(long.Parse(Session["UserId"].ToString()), estadoIncidencia));
                }
                return PartialView("ListadoIncidenciasHome",fachada.GetListaIncidencias(estadoIncidencia));
                //return la partialView con un select tab
            }
            else
            {
                //mensaje de error
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult Servicio(string id)
        {
            return RedirectToAction("Create", "Servicios", new { id = int.Parse(id)});
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
    }
}