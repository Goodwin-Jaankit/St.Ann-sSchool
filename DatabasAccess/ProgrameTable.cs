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
    
    public partial class ProgrameTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgrameTable()
        {
            this.AnnualTables = new HashSet<AnnualTable>();
            this.ProgrameSessionTables = new HashSet<ProgrameSessionTable>();
            this.StudentTables = new HashSet<StudentTable>();
            this.SubmissionFeeTables = new HashSet<SubmissionFeeTable>();
        }
    
        public int ProgrameID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnnualTable> AnnualTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgrameSessionTable> ProgrameSessionTables { get; set; }
        public virtual UserTable UserTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTable> StudentTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubmissionFeeTable> SubmissionFeeTables { get; set; }
    }
}