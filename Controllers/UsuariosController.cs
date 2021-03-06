﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CARS;
using CARS.Models;
using CARS.Utilities;

namespace CARS.Controllers
{
    public class UsuariosController : Controller
    {
        Fachada fachada = new Fachada();
        private DbCARS db = new DbCARS();
        [HandleError(View = "Error")]
        // GET: Usuarios
        public ActionResult Index()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                    {
                        return View(db.DbUsuarios.ToList());
                    }
                    else
                    {
                        throw new MyException("Credenciales no adecuadas.");
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

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.DbUsuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    return View();
                }
                else
                {
                    throw new MyException("Credenciales no adecuadas.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mail,Contrasenia,Activo,FechaIngreso,Tipo,Nombre,Apellido,Telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaIngreso = DateTime.Now;
                db.DbUsuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                       
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.DbUsuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mail,Contrasenia,Activo,FechaIngreso,Tipo,Nombre,Apellido,Telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.DbUsuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Usuario usuario = db.DbUsuarios.Find(id);
            usuario.Activo = false;
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
