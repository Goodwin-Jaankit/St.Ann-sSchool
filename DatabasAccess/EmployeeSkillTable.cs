//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabasAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeSkillTable
    {
        public int EmployeeSkillID { get; set; }
        public string SkillName { get; set; }
        public Nullable<int> EmployeeResumeID { get; set; }
        public int UserID { get; set; }
    
        public virtual EmployeeResumeTable EmployeeResumeTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
