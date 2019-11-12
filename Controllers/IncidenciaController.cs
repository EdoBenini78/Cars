using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARS.Controllers
{
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        public ActionResult Index()
        {
            return View();
        }

        
        //Método para crear incidencia del usuario del vehículo
        public ActionResult AgregarIncidencia()
        {
            return View();
        }

        public ActionResult ListaIncidencias()
        {
            return View();
        }

    }
}