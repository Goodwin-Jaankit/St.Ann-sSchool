using DatabasAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {


        private DbSchoolMgtEntities db = new DbSchoolMgtEntities();

        public ActionResult Login()
        {
            // Clear session on loading login page
            ClearSession();
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            ClearSession();
            try
            {

                if (email != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count() == 1)
                    {
                        Session["UserID"] = finduser[0].UserID;
                        Session["UserTypeID"] = finduser[0].UserTypeID;
                        Session["FullName"] = finduser[0].FullName;
                        Session["UserName"] = finduser[0].UserName;
                        Session["Password"] = finduser[0].Password;
                        Session["ContactNo"] = finduser[0].ContactNo;
                        Session["EmailAddress"] = finduser[0].EmailAddress;
                        Session["Address"] = finduser[0].Address;
                        var userid = finduser[0].UserID;
                        var studentphoto = db.StudentTables
                            .Where(s => s.UserID == userid)
                            .FirstOrDefault();

                        if (studentphoto != null)
                        {
                            Session["Photo"] = studentphoto.Photo;
                        }
                        else
                        {
                            var employee = db.StaffTables
                                .Where(e => e.UserID == userid)
                                .FirstOrDefault();

                            if (employee != null)
                            {
                                Session["Photo"] = employee.Photo;
                            }
                            else
                            {
                                // Set the session photo to the path of the default photo
                                Session["Photo"] = "~/Content/EmployeePhotos/5.png";
                            }  
                        }





                        string url = string.Empty;
                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            return RedirectToAction("About");
                        }
                        else
                        {
                            url = "About";
                        }
                        return RedirectToAction(url);
                    }
                    else
                    {
                        ClearSession();
                        ModelState.Clear();
                        TempData["ErrorMessage"] = "Invalid email or password.";
                    }
                }
                else
                {
                    ClearSession();
                    ModelState.Clear();
                    TempData["ErrorMessage"] = "Email and password are required.";
                }
            }
            catch (Exception ex)
            {
                ClearSession();
                ModelState.Clear();
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePasswordU(string oldpassword, string newpassword, string confirmpassword)
        {
            if (newpassword != confirmpassword)
            {
                ViewBag.Message = "Not Matched!";
                return View("ChangePassword");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            var getuser = db.UserTables.Find(userid);
            if (getuser.Password == oldpassword.Trim())
            {
                getuser.Password = newpassword.Trim();
                
            }
            db.Entry(getuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout");
        }


        private void ClearSession()
        {
          
            Session["UserID"] = null;
            Session["UserTypeID"] = null;
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["Password"] = null;
            Session["ContactNo"] = null;
            Session["EmailAddress"] = null;
            Session["Address"] = null;
        }


        public ActionResult About()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.SessionID = Session.SessionID;

            return View();
        }

        public ActionResult Logout()
        {
            ClearSession();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}