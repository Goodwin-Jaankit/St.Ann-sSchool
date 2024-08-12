using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.ViewModels
{
    public class EmployeeLanguageVM
    {
        [Required(ErrorMessage = "Please enter Language Name")]
        public string LanguageName { get; set; }

        [Required(ErrorMessage = "Please select Proficiency")]
        public string Proficiency { get; set; }

        public List<SelectListItem> ListOfProficiency { get; set; }

    }
}