using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CARS.Models;

namespace CARS.Controllers
{
    public class VehiculosController : Controller
    {
        private DbCARS db = new DbCARS();
        private Fachada fachada = new Fachada();

        // GET: Vehiculos
        public ActionResult Index()
        {
            return View(db.DbVehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(long? id)
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

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            return View();
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

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Vehiculo vehiculo = db.DbVehiculos.Find(id);
            db.DbVehiculos.Remove(vehiculo);
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
            ViewBag.Vehiculo = Id;
            ViewBag.ListaChoferes = fachada.GetListaChoferesParaVehiculo(Id);
            return View();
        }

        [HttpPost]
        public ActionResult AsignarChofer(string chofer, string IdVehiculo)
        {
            fachada.AgregarChoferaVehiculo(fachada.GetUsuarioBYDbId(long.Parse(chofer)),fachada.GetVehiculoByDbId(long.Parse(IdVehiculo)));           
            return RedirectToAction("Index","Vehiculos");
        }
    }
}
