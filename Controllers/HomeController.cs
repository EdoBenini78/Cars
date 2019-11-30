﻿using CARS.Models;
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
                return View(fachada.GetListaIncidencias(long.Parse(Session["UserId"].ToString())));
            }
            else
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            
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