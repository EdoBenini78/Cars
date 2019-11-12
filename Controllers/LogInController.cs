using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARS.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult ValidarLogIn(string email, string pass)
        {
            Session["UserId"] = email;
            return RedirectToAction("Index", "UsuarioVehiculo");
        }
    }
}