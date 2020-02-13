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
    public class VehiculosController : Controller
    {
        private DbCARS db = new DbCARS();
        private Fachada fachada = new Fachada();
        [HandleError(View = "Error")]
        // GET: Vehiculos
        public ActionResult Index()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                    {
                        return View(db.DbVehiculos.ToList());
                    }
                    else
                    {
                        throw new MyException("Credenciales no adecuadas");
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

        // GET: Vehiculos/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Vehiculo vehiculo = db.DbVehiculos.Find(id);
                    if (vehiculo == null)
                    {
                        return HttpNotFound();
                    }
                    return View(vehiculo);
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

        // GET: Vehiculos/Create
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
                    throw new MyException("Credenciales no adecuadas");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Matricula,Kilometros,FechaDeCompra,NumeroUnidad,Marca,Modelo,Anio,Motor,Chasis,Padron,Traccion,Combustible,FechaIngreso, Activo")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.DbVehiculos.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(long? id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Vehiculo vehiculo = db.DbVehiculos.Find(id);
                    if (vehiculo == null)
                    {
                        return HttpNotFound();
                    }
                    return View(vehiculo);
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

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Matricula,Kilometros,FechaDeCompra,NumeroUnidad,Marca,Modelo,Anio,Motor,Chasis,Padron,Traccion,Combustible,Activo,FechaIngreso")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
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
                    Vehiculo vehiculo = db.DbVehiculos.Find(id);
                    if (vehiculo == null)
                    {
                        return HttpNotFound();
                    }
                    return View(vehiculo);
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

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Vehiculo vehiculo = db.DbVehiculos.Find(id);
            vehiculo.Activo = false;
            db.Entry(vehiculo).State = EntityState.Modified;
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

        public ActionResult AgregarChoferView(int Id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    ViewBag.Vehiculo = Id;
                    ViewBag.ListaChoferes = fachada.GetListaChoferesParaVehiculo(Id);
                    return View();
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

        [HttpPost]
        public ActionResult AsignarChofer(string chofer, string IdVehiculo)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    fachada.AgregarChoferaVehiculo(fachada.GetUsuarioBYDbId(long.Parse(chofer)), fachada.GetVehiculoByDbId(long.Parse(IdVehiculo)));
                    return RedirectToAction("Index", "Vehiculos");
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
    }
}
