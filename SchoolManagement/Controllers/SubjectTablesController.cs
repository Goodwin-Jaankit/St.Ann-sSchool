﻿using System;
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
    public class SubjectTablesController : Controller
    {
        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        // GET: SubjectTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var subjectTables = db.SubjectTables.Include(s => s.UserTable);
            return View(subjectTables.ToList());
        }

        // GET: SubjectTables/Details/5
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
            SubjectTable subjectTable = db.SubjectTables.Find(id);
            if (subjectTable == null)
            {
                return HttpNotFound();
            }
            return View(subjectTable);
        }

        // GET: SubjectTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: SubjectTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectTable subjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            subjectTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.SubjectTables.Add(subjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", subjectTable.UserID);
            return View(subjectTable);
        }

        // GET: SubjectTables/Edit/5
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
            SubjectTable subjectTable = db.SubjectTables.Find(id);
            if (subjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", subjectTable.UserID);
            return View(subjectTable);
        }

        // POST: SubjectTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectTable subjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            subjectTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.Entry(subjectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", subjectTable.UserID);
            return View(subjectTable);
        }

        // GET: SubjectTables/Delete/5
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
            SubjectTable subjectTable = db.SubjectTables.Find(id);
            if (subjectTable == null)
            {
                return HttpNotFound();
            }
            return View(subjectTable);
        }

        // POST: SubjectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            SubjectTable subjectTable = db.SubjectTables.Find(id);
            db.SubjectTables.Remove(subjectTable);
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
