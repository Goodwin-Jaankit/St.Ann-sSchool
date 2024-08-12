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
    public class ProgrameSessionTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: ProgrameSessionTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var programeSessionTables = db.ProgrameSessionTables.Include(p => p.ProgrameTable).Include(p => p.SessionTable).Include(p => p.UserTable);
            return View(programeSessionTables.ToList());
        }

        // GET: ProgrameSessionTables/Details/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name");
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: ProgrameSessionTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.UserID = userid;

            if (ModelState.IsValid)
            { 
                var sessionname = db.SessionTables.Where(s=>s.SessionID == programeSessionTable.Session_ID).SingleOrDefault();
                var programname = db.ProgrameTables.Where(s => s.ProgrameID== programeSessionTable.ProgrameID).SingleOrDefault();
                if ( sessionname != null)
                {
                    if(!programeSessionTable.Details.Contains(sessionname.Name))
                    {
                        var details = "(" + sessionname.Name + "-" + (programname != null ? programname.Name : "") + ")" + programeSessionTable.Details;
                        programeSessionTable.Details = details;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Program name not found.";
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "Session name not found.";
                }

                db.ProgrameSessionTables.Add(programeSessionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Edit/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.UserID = userid;

            if (ModelState.IsValid)
            {
                var sessionname = db.SessionTables.Where(s => s.SessionID == programeSessionTable.Session_ID).SingleOrDefault();
                var programname = db.ProgrameTables.Where(s => s.ProgrameID == programeSessionTable.ProgrameID).SingleOrDefault();
                if (sessionname != null)
                {
                    if (!programeSessionTable.Details.Contains(sessionname.Name))
                    {
                        var details = "(" + sessionname.Name + "-" + (programname != null ? programname.Name : "") + ")" + programeSessionTable.Details;
                        programeSessionTable.Details = details;
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "Session name not found.";
                    return RedirectToAction("Index");
                }
                db.Entry(programeSessionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.Session_ID = new SelectList(db.SessionTables, "SessionID", "Name", programeSessionTable.Session_ID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTables/Delete/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTables.Find(id);
            db.ProgrameSessionTables.Remove(programeSessionTable);
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
