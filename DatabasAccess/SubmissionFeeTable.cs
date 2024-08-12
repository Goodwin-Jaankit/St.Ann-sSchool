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
    
    public partial class SubmissionFeeTable
    {
        public int SubmissionFeeID { get; set; }
        public int UserID { get; set; }
        public int ClassID { get; set; }
        public int StudentID { get; set; }
        public double Amount { get; set; }
        public int ProgrameID { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public string FeesMonth { get; set; }
        public string Description { get; set; }
    
        public virtual ClassTable ClassTable { get; set; }
        public virtual ProgrameTable ProgrameTable { get; set; }
        public virtual StudentTable StudentTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
