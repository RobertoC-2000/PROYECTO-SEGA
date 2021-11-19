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
    public class InventoriesController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Inventories
        public ActionResult Index()
        {
            var inventories = db.Inventories.Include(i => i.Film).Include(i => i.Store);
            return View(inventories.ToList());
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            ViewBag.iFilm_ID = new SelectList(db.Films, "iFilm_ID", "vTitle");
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID");
            return View();
        }

        // POST: Inventories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iInventory_ID,iFilm_ID,iStore_ID,sQuantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                if (!db.Inventories.Any(model => model.iInventory_ID == inventory.iInventory_ID))
                {
                    db.Inventories.Add(inventory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            ViewBag.iFilm_ID = new SelectList(db.Films, "iFilm_ID", "vTitle", inventory.iFilm_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", inventory.iStore_ID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.iFilm_ID = new SelectList(db.Films, "iFilm_ID", "vTitle", inventory.iFilm_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", inventory.iStore_ID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iInventory_ID,iFilm_ID,iStore_ID,sQuantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iFilm_ID = new SelectList(db.Films, "iFilm_ID", "vTitle", inventory.iFilm_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", inventory.iStore_ID);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
