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
    public class CustomersController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Active).Include(c => c.Address).Include(c => c.Store);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive");
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress");
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID");
            return View();
        }

        // POST: Customers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iCustomer_ID,iStore_ID,vFirstName,vLastName,vEMail,iAddress_ID,iCreation_Date,tActive_ID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!db.Customers.Any(model => model.iCustomer_ID == customer.iCustomer_ID))
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", customer.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", customer.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", customer.iStore_ID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", customer.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", customer.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", customer.iStore_ID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iCustomer_ID,iStore_ID,vFirstName,vLastName,vEMail,iAddress_ID,iCreation_Date,tActive_ID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", customer.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", customer.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", customer.iStore_ID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
