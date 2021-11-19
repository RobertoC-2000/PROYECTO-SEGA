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
    public class FilmsController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Films
        public ActionResult Index()
        {
            var films = db.Films.Include(f => f.Language);
            return View(films.ToList());
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            ViewBag.iLanguage_ID = new SelectList(db.Languages, "iLanguage_ID", "vName");
            return View();
        }

        // POST: Films/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iFilm_ID,vTitle,sRelease_Year,iLanguage_ID,tRental_Duration_Days,sLength_In_Minutes,sReplacement_Cost,tRating")] Film film)
        {
            if (ModelState.IsValid)
            {
                if (!db.Films.Any(model => model.iFilm_ID== film.iFilm_ID))
                {
                    db.Films.Add(film);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            ViewBag.iLanguage_ID = new SelectList(db.Languages, "iLanguage_ID", "vName", film.iLanguage_ID);
            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.iLanguage_ID = new SelectList(db.Languages, "iLanguage_ID", "vName", film.iLanguage_ID);
            return View(film);
        }

        // POST: Films/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iFilm_ID,vTitle,sRelease_Year,iLanguage_ID,tRental_Duration_Days,sLength_In_Minutes,sReplacement_Cost,tRating")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iLanguage_ID = new SelectList(db.Languages, "iLanguage_ID", "vName", film.iLanguage_ID);
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
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
