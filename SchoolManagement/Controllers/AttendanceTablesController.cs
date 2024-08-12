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
    public class AttendanceTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: AttendanceTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var attendanceTables = db.AttendanceTables.Include(a => a.StudentTable).Include(a => a.ClassTable);
            return View(attendanceTables.ToList());
        }

        // GET: AttendanceTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceTable attendanceTable = db.AttendanceTables.Find(id);
            if (attendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(attendanceTable);
        }

        // GET: AttendanceTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName");
            ViewBag.SessionID = new SelectList(db.SessionTables, "SessionID", "Name");
            return View();
        }

        // POST: AttendanceTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceTable attendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.AttendanceTables.Add(attendanceTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", attendanceTable.StudentID);
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", attendanceTable.ClassID);
            return View(attendanceTable);
        }

        // GET: AttendanceTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceTable attendanceTable = db.AttendanceTables.Find(id);
            if (attendanceTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", attendanceTable.StudentID);
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", attendanceTable.ClassID);
            return View(attendanceTable);
        }

        // POST: AttendanceTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttendanceTable attendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(attendanceTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", attendanceTable.StudentID);
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", attendanceTable.ClassID);
            return View(attendanceTable);
        }

        // GET: AttendanceTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceTable attendanceTable = db.AttendanceTables.Find(id);
            if (attendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(attendanceTable);
        }

        // POST: AttendanceTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttendanceTable attendanceTable = db.AttendanceTables.Find(id);
            db.AttendanceTables.Remove(attendanceTable);
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
