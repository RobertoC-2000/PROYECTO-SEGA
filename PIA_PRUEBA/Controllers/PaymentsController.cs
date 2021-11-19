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
    public class PaymentsController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Customer).Include(p => p.Staff);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName");
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName");
            return View();
        }

        // POST: Payments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iPayment_ID,iCustomer_ID,iStaff_ID,iAmount,dPayment_Date")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", payment.iCustomer_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", payment.iStaff_ID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", payment.iCustomer_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", payment.iStaff_ID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iPayment_ID,iCustomer_ID,iStaff_ID,iAmount,dPayment_Date")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iCustomer_ID = new SelectList(db.Customers, "iCustomer_ID", "vFirstName", payment.iCustomer_ID);
            ViewBag.iStaff_ID = new SelectList(db.Staffs, "iStaff_ID", "vFirstName", payment.iStaff_ID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
