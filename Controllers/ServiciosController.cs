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
    public class ServiciosController : Controller
    {
        private DbCARS db = new DbCARS();
        private Fachada fachada = new Fachada();

        // GET: Servicios
        public ActionResult Index()
        {
            try
            {
                return View(db.DbServicios.ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: Servicios/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Servicio servicio = db.DbServicios.Find(id);
                if (servicio == null)
                {
                    return HttpNotFound();
                }
                return View(servicio);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: Servicios/Create
        public ActionResult Create(string id)
        {
            try
            {
                ViewBag.ListadoTalleres = fachada.GetTalleresDistanciaOk(fachada.GetIncidenciaByDbId(long.Parse(id)));
                ViewBag.Incidencia = id;
                long miID = long.Parse(id);
                ViewBag.Matricula = fachada.GetIncidenciaByDbId(miID).Vehiculo.Matricula;
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: Servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tipo,FechaSugerida,Estado")] Servicio servicio, string incidencia, string taller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Incidencia aIncidencia = fachada.GetIncidenciaByDbId(long.Parse(incidencia));
                    if (aIncidencia.Estado == EstadoIncidencia.Pendiente)
                    {
                        aIncidencia.Estado = EstadoIncidencia.Procesando;
                        db.Entry(aIncidencia).State = EntityState.Modified;
                    }
                    servicio.Taller = fachada.GetTallerByDbId(long.Parse(taller));
                    db.DbServicios.Add(servicio);
                    ServicioIncidencia servicioIncidencia = new ServicioIncidencia(servicio, aIncidencia);
                    db.DbServicioDeIncidencia.Add(servicioIncidencia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(servicio);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

            // GET: Servicios/Edit/5
            public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.DbServicios.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo,FechaSugerida,FechaEntrada,FechaSalida,Estado")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                db.SaveChanges();               
            }

            fachada.ControlStatusIncidencia(servicio); 
            return RedirectToAction("Index");
        }

        public ActionResult VerServicios(string id)
        {
            List<Servicio> serviciosDeIncidencia = fachada.GetServiciosIncidencia(long.Parse(id));
            if (serviciosDeIncidencia.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.Incidencia = id;
            return View("Index",serviciosDeIncidencia);
        }

        // GET: Servicios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.DbServicios.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Servicio servicio = db.DbServicios.Find(id);
            db.DbServicios.Remove(servicio);

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
