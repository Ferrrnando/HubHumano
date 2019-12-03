using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HubHumano.Models;

namespace HubHumano.Controllers
{
    public class EmpleadoController : Controller
    {
        private hubhumanoEntities1 db = new hubhumanoEntities1();

        // GET: Empleado
        public ActionResult Index()
        {
            var empleadoes = db.empleadoes.Include(e => e.cargo1).Include(e => e.departamento1);
            return View(empleadoes.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.cargo = new SelectList(db.cargoes, "id", "cargo1");
            ViewBag.departamento = new SelectList(db.departamentoes, "id", "codigo");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,codigo,nombre,apellido,telefono,departamento,cargo,fecha_ingreso,salario,estatus")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.empleadoes.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cargo = new SelectList(db.cargoes, "id", "cargo1", empleado.cargo);
            ViewBag.departamento = new SelectList(db.departamentoes, "id", "codigo", empleado.departamento);
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.cargo = new SelectList(db.cargoes, "id", "cargo1", empleado.cargo);
            ViewBag.departamento = new SelectList(db.departamentoes, "id", "codigo", empleado.departamento);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,codigo,nombre,apellido,telefono,departamento,cargo,fecha_ingreso,salario,estatus")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cargo = new SelectList(db.cargoes, "id", "cargo1", empleado.cargo);
            ViewBag.departamento = new SelectList(db.departamentoes, "id", "codigo", empleado.departamento);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleado empleado = db.empleadoes.Find(id);
            db.empleadoes.Remove(empleado);
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
