using DatabasAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class TimeTableReportsController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();
        // GET: TimeTableReports
        public ActionResult Teacher(int? id)
        {
            var teacherclass = db.TimeTblTables.Where(t=>t.StaffID == id).OrderByDescending(t=>t.TimeTableID); 
            return View(teacherclass);
        }
        public ActionResult TeacherWiseReport(int? id)
        {
            var teacherclass = db.TimeTblTables.Where(i=>i.IsActive==true).OrderByDescending(t => t.StaffID);
            return View(teacherclass);
        }
        public ActionResult StudentWiseReport(int? id)
        {
            var studentPromoteRecord = db.StudentPromoteTables
                .Where(p => p.StudentID == id && p.IsAcitve == true)
                .FirstOrDefault();

            // If no record is found, handle it appropriately (e.g., return an error view or redirect)
            if (studentPromoteRecord == null)
            {
                return HttpNotFound();
            }

            // Extract the ClassID from the found StudentPromoteTable record
            int classId = studentPromoteRecord.ClassID;

            // Fetch the subject times based on the ClassID
            var subjectTime = db.TimeTblTables
                .Where(t => t.ClassSubjectTable.ClassID == classId)
                .ToList();

            return View(subjectTime);
        }

        public ActionResult StaffAttendance(int? id)
        {
            var staffattendance = db.StaffAttendanceTables.Where(t => t.StaffID == id).OrderByDescending(t => t.StaffAttendanceID);
            return View(staffattendance);
        }
    }
}