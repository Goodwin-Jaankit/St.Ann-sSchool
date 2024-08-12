using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.ViewModels
{
    public class EmployeeCertificationVM
    {
        public int EmployeeCertificationID { get; set; }
        [Required(ErrorMessage = "Please provide your certification name.")]
        public string CertificationName { get; set; }
        [Required(ErrorMessage = "Please enter the certification authority.")]
        public string CertificationAuthority { get; set; }
        [Required(ErrorMessage = "Please select the certification level.")]
        public string LevelCertification { get; set; }
        [Required(ErrorMessage = "Please select the achievement date.")]
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<int> EmployeeResumeID { get; set; }
        public int UserID { get; set; }


        public List<SelectListItem> ListOfLevel { get; set; }

    }
}