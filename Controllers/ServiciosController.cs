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
    public class ServiciosController : Controller
    {
        private DbCARS db = new DbCARS();
        private Fachada fachada = new Fachada();
        [HandleError(View = "Error")]
        // GET: Servicios
        public ActionResult Index()
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    return View(db.DbServicios.ToList());
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

                throw ex;
            }
        }

        // GET: Servicios/Create
        public ActionResult Create(string id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    ViewBag.ListadoTalleres = fachada.GetTalleresDistanciaOk(fachada.GetIncidenciaByDbId(long.Parse(id)));
                    ViewBag.Incidencia = id;
                    long miID = long.Parse(id);
                    ViewBag.Matricula = fachada.GetIncidenciaByDbId(miID).Vehiculo.Matricula;
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

        // POST: Servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tipo,FechaSugerida,Estado,NumeroOrden")] Servicio servicio, string incidencia, string taller)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    if (ModelState.IsValid)
                    {
                        Incidencia aIncidencia = fachada.GetIncidenciaByDbId(long.Parse(incidencia));
                        if (aIncidencia.FechaInicio > servicio.FechaSugerida)
                        {
                            throw new MyException("La fecha selecionada tiene que ser mayor a " + aIncidencia.FechaInicio);
                        }
                        fachada.CreateServicio(aIncidencia, servicio, taller);

                        return RedirectToAction("VerServicios", "Servicios", new { id = aIncidencia.Id.ToString() });  
                    }

                    return View(servicio);
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

            // GET: Servicios/Edit/5
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
                    Servicio servicio = db.DbServicios.Find(id);
                    if (servicio == null)
                    {
                        return HttpNotFound();
                    }
                    return View(servicio);
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

        // POST: Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo,FechaSugerida,FechaEntrada,FechaSalida,Estado,NumeroOrden")] Servicio servicio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Servicio getServicio = fachada.GetServicioById(servicio.Id);
                    servicio.Taller = getServicio.Taller;
                    servicio.Vehiculo = getServicio.Vehiculo;
                    servicio.Hora = DateTime.Now;
                    db.Entry(servicio).State = EntityState.Modified;
                    db.SaveChanges();
                }

                fachada.ControlStatusIncidencia(servicio);
                Incidencia aIncidencia = fachada.GetIncidenciaDeServicio(servicio);
                return RedirectToAction("VerServicios", "Servicios", new { id = aIncidencia.Id.ToString() });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult VerServicios(string id)
        {
            try
            {
                if (fachada.GetUsuarioRole(Session["UserId"].ToString()) != TipoUsuario.Chofer)
                {
                    List<Servicio> serviciosDeIncidencia = fachada.GetServiciosIncidencia(long.Parse(id));
                    if (serviciosDeIncidencia.Count == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.Incidencia = id;
                    return View("Index", serviciosDeIncidencia);
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

        // GET: Servicios/Delete/5
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
                    Servicio servicio = db.DbServicios.Find(id);
                    if (servicio == null)
                    {
                        return HttpNotFound();
                    }
                    return View(servicio);
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

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Servicio servicio = db.DbServicios.Find(id);
            servicio.Estado = TipoEstado.Cancelado;
            db.Entry(servicio).State = EntityState.Modified;
            db.SaveChanges();
            Incidencia aIncidencia = fachada.GetIncidenciaDeServicio(servicio);
            return RedirectToAction("VerServicios", "Servicios", new { id = aIncidencia.Id.ToString() });
            
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
