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
    public class ExamMarksTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: ExamMarksTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var examMarksTables = db.ExamMarksTables.Include(e => e.ExamTable).Include(e => e.StudentTable).Include(e => e.UserTable).Include(e => e.ClassSubjectTable);
            return View(examMarksTables.ToList());
        }

        public ActionResult GetByPromoteID(string sid)
        {
            int promoteid = Convert.ToInt32(sid);
            var promoterecord = db.StudentPromoteTables.Find(promoteid);

            if (promoterecord == null)
            {
                return Json(new { error = "Record not found" }, JsonRequestBehavior.AllowGet);
            }

            var student = promoterecord.StudentTable.Name;
            var studentId = promoterecord.StudentID; // Assuming you have StudentID
            var classsubjects = db.ClassSubjectTables
                                  .Where(cls => cls.ClassID == promoterecord.ClassID)
                                  .Select(cls => new
                                  {
                                      ClassSubjectID = cls.ClassSubjectID,
                                      Name = cls.Name
                                  }).ToList();

            return Json(new
            {
                student = student,
                StudentID = studentId, // Return StudentID
                Subjects = classsubjects,
                ClassID = promoterecord.ClassID,
                ProgrameID = promoterecord.ProgrameSessionTable.ProgrameID
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Gettotalmarks(string sid)
        {
            int classsubjectid = Convert.ToInt32(sid);
            var totalmarks = db.ClassSubjectTables
                                .Where(cs => cs.ClassSubjectID == classsubjectid)
                                .Select(cs => cs.SubjectTable.TotalMarks)
                                .FirstOrDefault();

            if (totalmarks == null)
            {
                return Json(new { error = "Marks not found" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = totalmarks }, JsonRequestBehavior.AllowGet);
        }


        // GET: ExamMarksTables/Details/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // GET: ExamMarksTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name");
            return View();
        }

        // POST: ExamMarksTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarksTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.ExamMarksTables.Add(examMarksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            return View(examMarksTable);
        }

        // GET: ExamMarksTables/Edit/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            return View(examMarksTable);
        }

        // POST: ExamMarksTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarksID,ExamID,ClassSubjectID,StudentID,UserID,TotalMarks,ObtainMarks")] ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarksTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.Entry(examMarksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            return View(examMarksTable);
        }

        // GET: ExamMarksTables/Delete/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // POST: ExamMarksTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            db.ExamMarksTables.Remove(examMarksTable);
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
