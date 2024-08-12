using DatabasAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class ExamReportsController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: ExamReports
        public ActionResult PrintDMC()
        {
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            ViewBag.TotalObtainedMarks = 0; // Initial total obtained marks
            return View(new List<ExamMarksTable>());
        }

        [HttpPost]
        public ActionResult PrintDMC(int? promoteid, int? examid)
        {
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            var promoterecord = db.StudentPromoteTables.Find(promoteid);
            if (promoterecord != null)
            {
                var listmarks = db.ExamMarksTables.Where(e => e.ClassSubjectTable.ClassID == promoterecord.ClassID && e.StudentID == promoterecord.StudentID).ToList();
                var totalObtainedMarks = listmarks.Sum(m => m.ObtainMarks);
                ViewBag.TotalObtainedMarks = totalObtainedMarks;
                return View(listmarks);
            }
            ViewBag.TotalObtainedMarks = 0; // If no records found
            return View(new List<ExamMarksTable>());
        }
    }
}
