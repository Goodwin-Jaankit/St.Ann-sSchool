using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabasAccess;

namespace SchoolManagement.Controllers
{
    public class SessionTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: SessionTables
        public ActionResult Index()
        {
            var sessionTables = db.SessionTables.Include(s => s.UserTable);
            return View(sessionTables.ToList());
        }

        // GET: SessionTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionTable sessionTable = db.SessionTables.Find(id);
            if (sessionTable == null)
            {
                return HttpNotFound();
            }
            return View(sessionTable);
        }

        // GET: SessionTables/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: SessionTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionID,UserID,Name,StartDate,EndDate")] SessionTable sessionTable)
        {
            if (ModelState.IsValid)
            {
                db.SessionTables.Add(sessionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", sessionTable.UserID);
            return View(sessionTable);
        }

        // GET: SessionTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionTable sessionTable = db.SessionTables.Find(id);
            if (sessionTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", sessionTable.UserID);
            return View(sessionTable);
        }

        // POST: SessionTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,UserID,Name,StartDate,EndDate")] SessionTable sessionTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", sessionTable.UserID);
            return View(sessionTable);
        }

        // GET: SessionTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionTable sessionTable = db.SessionTables.Find(id);
            if (sessionTable == null)
            {
                return HttpNotFound();
            }
            return View(sessionTable);
        }

        // POST: SessionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionTable sessionTable = db.SessionTables.Find(id);
            db.SessionTables.Remove(sessionTable);
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
