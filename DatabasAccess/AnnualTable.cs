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
    
    public partial class AnnualTable
    {
        public int AnnualID { get; set; }
        public int UserID { get; set; }
        public int ProgrameID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<double> Fees { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ProgrameTable ProgrameTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
