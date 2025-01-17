﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.ViewModels
{
    public class EmployeeWorkExperienceVM
    {
        public int EmployeeWorkExperienceID { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<System.DateTime> ToYear { get; set; }
        public string Description { get; set; }
        public Nullable<int> EmployeeResumeID { get; set; }
        public int UserID { get; set; }

        public List<SelectListItem> ListOfCountries { get; set; }

    }
}