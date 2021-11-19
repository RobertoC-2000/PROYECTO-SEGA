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
    public class RentalsController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Rentals
        public ActionResult Index()
        {
            var rentals = db.Rentals.Include(r => r.Customer).Include(r => r.Inventory).Include(r => r.Staff);
            return View(rentals.ToList());
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName");
            ViewBag.iInventory_ID = new SelectList(db.Inventories, "iInventory_ID", "iInventory_ID");
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName");
            return View();
        }

        // POST: Rentals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iRental_ID,dRental_Date,iInventory_ID,iCustomer_ID,dReturn_Date,iStaff_ID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (!db.Rentals.Any(model => model.iRental_ID== rental.iRental_ID))
                {
                    db.Rentals.Add(rental);
                    Inventory inventory = db.Inventories.Find(rental.iInventory_ID);
                    if (inventory.sQuantity > 0)
                    {
                        inventory.sQuantity -= 1;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {

                    }
                }
            }
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", rental.iCustomer_ID);
            ViewBag.iInventory_ID = new SelectList(db.Inventories, "iInventory_ID", "iInventory_ID", rental.iInventory_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", rental.iStaff_ID);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", rental.iCustomer_ID);
            ViewBag.iInventory_ID = new SelectList(db.Inventories, "iInventory_ID", "iInventory_ID", rental.iInventory_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", rental.iStaff_ID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iRental_ID,dRental_Date,iInventory_ID,iCustomer_ID,dReturn_Date,iStaff_ID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", rental.iCustomer_ID);
            ViewBag.iInventory_ID = new SelectList(db.Inventories, "iInventory_ID", "iInventory_ID", rental.iInventory_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", rental.iStaff_ID);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            Inventory inventory = db.Inventories.Find(rental.iInventory_ID);
            inventory.sQuantity += 1;
            db.Rentals.Remove(rental);
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
