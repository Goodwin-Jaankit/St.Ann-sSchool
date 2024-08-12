using DatabasAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace SchoolManagement.Controllers
{
    public class FeeReportController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: FeeReport
        public ActionResult CustomTution()
        {
            var tutionfee = db.SubmissionFeeTables.Where(s=>s.SubmissionDate >= DateTime.Now && s.SubmissionDate <= DateTime.Now).ToList().OrderByDescending(s=>s.SubmissionFeeID);
            return View(tutionfee);
        }
        [HttpPost]
        public ActionResult CustomTution(DateTime fromDate,DateTime toDate)
        {
            var tutionfee = db.SubmissionFeeTables.Where(s => s.SubmissionDate >= fromDate && s.SubmissionDate <= toDate).ToList().OrderByDescending(s => s.SubmissionFeeID);
            return View(tutionfee);
        }

        //public ActionResult customannual()
        //{
        //    var tutionfee = db.StudentPromoteTables.Where(s => s.PromoteDate >= DateTime.Now && s.PromoteDate <= DateTime.Now).ToList().OrderByDescending(s => s.StudentPromoteID);
        //    return View(tutionfee);
        //}
        //[HttpPost]
        //public ActionResult customannual(DateTime fromDate, DateTime toDate)
        //{
        //    var tutionfee = db.StudentPromoteTables.Where(s => s.PromoteDate >= fromDate && s.PromoteDate <= toDate).ToList().OrderByDescending(s => s.StudentPromoteID);
        //    return View(tutionfee);
        //}
    }
}