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
    public class StaffsController : Controller
    {
        private PIA_O_F_R2Entities db = new PIA_O_F_R2Entities();

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Active).Include(s => s.Address).Include(s => s.Store);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive");
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress");
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID");
            return View();
        }

        // POST: Staffs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iStaff_ID,vFirstName,vLastName,vEMail,iAddress_ID,iStore_ID,tActive_ID,vUsername,vPassword")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                if (!db.Staffs.Any(model => model.iStaff_ID == staff.iStaff_ID))
                {
                    db.Staffs.Add(staff);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }

            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", staff.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", staff.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", staff.iStore_ID);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", staff.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", staff.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", staff.iStore_ID);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iStaff_ID,vFirstName,vLastName,vEMail,iAddress_ID,iStore_ID,tActive_ID,vUsername,vPassword")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tActive_ID = new SelectList(db.Actives, "tActive_ID", "vActive", staff.tActive_ID);
            ViewBag.iAddress_ID = new SelectList(db.Addresses, "iAddress_ID", "vAddress", staff.iAddress_ID);
            ViewBag.iStore_ID = new SelectList(db.Stores, "iStore_ID", "iStore_ID", staff.iStore_ID);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
