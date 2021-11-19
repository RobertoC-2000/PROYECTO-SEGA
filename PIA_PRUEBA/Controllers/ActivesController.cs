using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PIA_PRUEBA.Models;

namespace PIA_PRUEBA.Controllers
{
    public class ActivesController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Actives
        public ActionResult Index()
        {
            return View(db.Actives.ToList());
        }

        // GET: Actives/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Active active = db.Actives.Find(id);
            if (active == null)
            {
                return HttpNotFound();
            }
            return View(active);
        }

        // GET: Actives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actives/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tActive_ID,vActive")] Active active)
        {
            if (ModelState.IsValid)
            {
                db.Actives.Add(active);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(active);
        }

        // GET: Actives/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Active active = db.Actives.Find(id);
            if (active == null)
            {
                return HttpNotFound();
            }
            return View(active);
        }

        // POST: Actives/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tActive_ID,vActive")] Active active)
        {
            if (ModelState.IsValid)
            {
                db.Entry(active).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(active);
        }

        // GET: Actives/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Active active = db.Actives.Find(id);
            if (active == null)
            {
                return HttpNotFound();
            }
            return View(active);
        }

        // POST: Actives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Active active = db.Actives.Find(id);
            db.Actives.Remove(active);
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
