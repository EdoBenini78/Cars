using CARS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARS.Controllers
{
    public class IncidenciaController : Controller
    {
        Fachada fachada = new Fachada();
        // GET: Incidencia
        public ActionResult Index()
        {
            ViewBag.Vehiculo = fachada.GetVehiculoByChofer(Session["UserId"].ToString());
            return View();
        }

        
        //Método para crear incidencia del usuario del vehículo
        public ActionResult AgregarIncidencia()
        {
            //fachada.AgregarIncidencia();
            return View();
        }

        public ActionResult ListaIncidencias()
        {
            return View();
        }

    }
}