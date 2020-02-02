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
            Session["Tipo"] = null;
            Session["UserId"] = null;
            Session["Mail"] = null;
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult ValidarLogIn(string email, string pass)
        {
            Usuario pUsuario = fachada.ValidarLogIn(pass, email);
            if (pUsuario != null)
            {
                Session["Tipo"] = pUsuario.Tipo;
                Session["Mail"] = pUsuario.Mail;
                Session["UserId"] = pUsuario.Id;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("LogIn"); 
            
        }

        [HttpGet]
        public ActionResult OlvideMiContrasenia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OlvideMiContrasenia(string email)
        {
            //MANDA UN MAIL SOLICITANDO UNA NUEVA CONTRASEÑA
            return RedirectToAction("LogIn", "LogIn");
        }
    }
}