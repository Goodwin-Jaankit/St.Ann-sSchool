using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabasAccess;

namespace SchoolManagement.Controllers
{
    public class StudentPromoteTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: StudentPromoteTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var studentPromoteTables = db.StudentPromoteTables.Include(s => s.ClassTable).Include(s => s.ProgrameSessionTable).Include(s => s.SectionTable).Include(s => s.StudentTable).OrderByDescending(s=> s.StudentPromoteID);
            return View(studentPromoteTables.ToList());
        }


        public ActionResult GetPromoteClsList(string sid)
        {
            if (string.IsNullOrEmpty(sid))
            {
                return Json(new { data = new List<ClassTable>() }, JsonRequestBehavior.AllowGet);
            }

            int studentid;
            if (!int.TryParse(sid, out studentid))
            {
                return Json(new { data = new List<ClassTable>() }, JsonRequestBehavior.AllowGet);
            }

            var student = db.StudentTables.Find(studentid);
            if (student == null)
            {
                return Json(new { data = new List<ClassTable>() }, JsonRequestBehavior.AllowGet);
            }

            var classTable = db.ClassTables
                                .Where(cls => cls.ClassID > student.ClassID)
                                .Select(cls => new { ClassID = cls.ClassID, ClassName = cls.ClassName })
                                .ToList();

            return Json(new { data = classTable }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAnnulFee(string sid)
        {
            if (int.TryParse(sid, out int progessid))
            {
                var ps = db.ProgrameSessionTables.Find(progessid);
                if (ps != null)
                {
                    var annulfee = db.AnnualTables.SingleOrDefault(a => a.AnnualID == ps.ProgrameID);
                    if (annulfee != null)
                    {
                        double? fee = annulfee.Fees;
                        return Json(new { fees = fee }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { fees = (double?)null }, JsonRequestBehavior.AllowGet);
        }


        // GET: StudentPromoteTables/Details/5
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
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName");
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details");
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            return View();
        }

        // POST: StudentPromoteTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentPromoteTable studentPromoteTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.StudentPromoteTables.Add(studentPromoteTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Edit/5
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
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // POST: StudentPromoteTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentPromoteID,StudentID,ClassID,ProgramSessionID,PromoteDate,AnnualFee,IsAcitve,IsSubmit,SectionID")] StudentPromoteTable studentPromoteTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(studentPromoteTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "ClassName", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Delete/5
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
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromoteTable);
        }

        // POST: StudentPromoteTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            db.StudentPromoteTables.Remove(studentPromoteTable);
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
