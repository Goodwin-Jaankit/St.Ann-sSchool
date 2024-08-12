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
    public class ClassSubjectTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: ClassSubjectTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var classSubjectTables = db.ClassSubjectTables.Include(c => c.ClassTable).Include(c => c.SubjectTable);
            return View(classSubjectTables.ToList());
        }

        // GET: ClassSubjectTables/Details/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(a => a.IsActive == true), "ClassID", "ClassName");
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");
            return View();
        }

        // POST: ClassSubjectTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ClassSubjectTables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassSubjectID,ClassID,SubjectID,Name,IsActive")] ClassSubjectTable classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var classname = db.ClassTables.SingleOrDefault(c => c.ClassID == classSubjectTable.ClassID);
                var subjectname = db.SubjectTables.SingleOrDefault(s => s.SubjectID == classSubjectTable.SubjectID);

                if (classname != null && subjectname != null)
                {
                    if (string.IsNullOrEmpty(classSubjectTable.Name))
                    {
                        classSubjectTable.Name = $"{subjectname.Name}-{classname.ClassName}";
                    }
                    else
                    {
                        if (!classSubjectTable.Name.Contains(classname.ClassName))
                        {
                            classSubjectTable.Name = $"{classSubjectTable.Name}-{classname.ClassName}";
                        }
                    }
                }
                else
                {
                    if (classname == null)
                    {
                        ModelState.AddModelError("", "Class not found.");
                    }
                    if (subjectname == null)
                    {
                        ModelState.AddModelError("", "Subject not found.");
                    }

                    ViewBag.ClassID = new SelectList(db.ClassTables.Where(a => a.IsActive == true), "ClassID", "ClassName", classSubjectTable.ClassID);
                    ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
                    return View(classSubjectTable);
                }

                db.ClassSubjectTables.Add(classSubjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables.Where(a => a.IsActive == true), "ClassID", "ClassName", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }


        // GET: ClassSubjectTables/Edit/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(a => a.IsActive == true), "ClassID", "ClassName", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassSubjectID,ClassID,SubjectID,Name,IsActive")] ClassSubjectTable classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var classname = db.ClassTables.Where(c => c.ClassID == classSubjectTable.ClassID).SingleOrDefault();

                if (classname != null)
                {
                    if (!classSubjectTable.Name.Contains(classname.ClassName))
                    {
                        classSubjectTable.Name = classSubjectTable.Name + "-" + classname.ClassName;

                    }

                }
                db.Entry(classSubjectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(a => a.IsActive == true), "ClassID", "ClassName", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Delete/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            db.ClassSubjectTables.Remove(classSubjectTable);
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
