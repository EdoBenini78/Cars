using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CARS.Models;
using CARS.Utilities;

namespace CARS.Controllers
{
    public class TalleresController : Controller
    {
        Fachada fachada = new Fachada();
        private DbCARS db = new DbCARS();
        [HandleError(View = "Error")]
        // GET: Talleres
        public ActionResult Index()
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    if (true)
                    {
                        return View(db.DbTalleres.ToList()); 
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

        // GET: Talleres/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taller taller = db.DbTalleres.Find(id);
            if (taller == null)
            {
                return HttpNotFound();
            }
            return View(taller);
        }

        // GET: Talleres/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Talleres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Rut,NombreContacto,Telefono,Direccion")] Taller taller, string longitud, string latitud)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.DbTalleres.Where(t => t.Rut == taller.Rut).FirstOrDefault() == null)
                    {
                        longitud.Replace(".", "");
                        latitud.Replace(".", "");
                        longitud.Replace(",", "");
                        latitud.Replace(",", "");
                        //En la web hay que fijarse si toma con coma
                        string lat = latitud.Substring(0, 3) + "," + latitud.Substring(3);
                        latitud = lat;
                        string lon = longitud.Substring(0, 3) + "," + longitud.Substring(3);
                        longitud = lon;
                        //if (!latitud.Contains("."))
                        //{
                            
                        //}
                    

                        //if (!longitud.Contains("."))
                        //{
                           
                        //}
                                                                                                                    
                        taller.Longitud = double.Parse(longitud);
                        taller.Latitud = double.Parse(latitud);
                        db.DbTalleres.Add(taller);
                        db.SaveChanges();
                        //success
                    }
                    else
                    {
                        throw new MyException("Rut ya existente");
                    }
                    return RedirectToAction("Index");
                }

                return View(taller);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Talleres/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taller taller = db.DbTalleres.Find(id);
            if (taller == null)
            {
                return HttpNotFound();
            }
            return View(taller);
        }

        // POST: Talleres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Rut,NombreContacto,Telefono,Activo,FechaIngreso,Longitud,Latitud,Direccion")] Taller taller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taller).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(taller);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Talleres/Delete/5
        public ActionResult Delete(long? id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Taller taller = db.DbTalleres.Find(id);
                    if (taller == null)
                    {
                        return HttpNotFound();
                    }
                    return View(taller);
                }
                else
                {
                    throw new MyException("Credenciales no adecuadas");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Talleres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                Taller taller = db.DbTalleres.Find(id);
                taller.Activo = false;
                db.Entry(taller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
