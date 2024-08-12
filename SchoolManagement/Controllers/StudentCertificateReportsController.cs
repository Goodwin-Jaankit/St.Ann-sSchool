using DatabasAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class StudentCertificateReportsController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();
        // GET: StudentCertificateReports
        public ActionResult LeavingCertificate(int? id)
        {
            ViewBag.message = "Ready to Print";
            var student = db.StudentPromoteTables.Where(std => std.StudentID == id && std.IsAcitve == true).FirstOrDefault();
            if(student == null)
            {
                ViewBag.message = "Already Printed";
                student = db.StudentPromoteTables.Where(std => std.StudentID == id).OrderByDescending(e=>e.StudentPromoteID).FirstOrDefault();
                return View( student);
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult PrintLeavingCertificate(int? id)
        {
          
            var student = db.StudentPromoteTables.Where(std => std.StudentID == id && std.IsAcitve == true).FirstOrDefault();
            if (student == null)
            {
                student = db.StudentPromoteTables.Where(std => std.StudentID == id).OrderByDescending(e => e.StudentPromoteID).FirstOrDefault();
                ViewBag.Message = "Already Print! Please Contact to Administration Department.";
                return View("LeavingCertificate", student);
            }

            student.IsAcitve = false;
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.Message = "Print Successfully";
            return View("LeavingCertificate", student);

        }
        public ActionResult ExamCertificate(int? id)
        {
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            ViewBag.TotalObtainedMarks = 0; // Initial total obtained marks
            return View(new List<ExamMarksTable>());
        }

        [HttpPost]
        public ActionResult ExamCertificate(int? promoteid, int? examid)
        {
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");

            var promoterecord = db.StudentPromoteTables.Find(promoteid);
            if (promoterecord != null)
            {
                var listmarks = db.ExamMarksTables
                    .Where(e => e.ClassSubjectTable.ClassID == promoterecord.ClassID && e.StudentID == promoterecord.StudentID)
                    .ToList();

                var studentPromoteID = promoterecord.StudentPromoteID;
                ViewBag.StudentPromoteID = studentPromoteID;

                var totalObtainedMarks = listmarks.Sum(m => m.ObtainMarks);
                ViewBag.TotalObtainedMarks = totalObtainedMarks;

                return View(listmarks);
            }

            ViewBag.TotalObtainedMarks = 0; // If no records found
            ViewBag.StudentPromoteID = null; // If no records found
            return View(new List<ExamMarksTable>());
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}