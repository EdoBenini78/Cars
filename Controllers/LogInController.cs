using CARS.Models;
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
        Fachada fachada = new Fachada();
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult ValidarLogIn(string email, string pass)
        {
            Usuario pUsuario = fachada.ValidarLogIn(pass, email);
            if (pUsuario != null)
            {
                Session["Tipo"] = pUsuario.Tipo;
                Session["UserId"] = pUsuario.Id;
                return RedirectToAction("Index", "Home");
            }
          
            return RedirectToAction("LogIn", "LogIn"); 
            
        }
    }
}