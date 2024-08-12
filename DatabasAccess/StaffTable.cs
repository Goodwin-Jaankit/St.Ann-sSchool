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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class StaffTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffTable()
        {
            this.EmployeeLeavingTables = new HashSet<EmployeeLeavingTable>();
            this.EmployeeSalaryTables = new HashSet<EmployeeSalaryTable>();
            this.StaffAttendanceTables = new HashSet<StaffAttendanceTable>();
            this.TimeTblTables = new HashSet<TimeTblTable>();
        }
    
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Designation_ID { get; set; }
        public string ContactNo { get; set; }
        public double BasicSalary { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public string HomePhone { get; set; }
        public bool DoYouHaveAnyDisability { get; set; }
        public string IfDiasabilityYesGiveUsInfo { get; set; }
        public bool AreYouTakingAnyMedication { get; set; }
        public string IfAnyMedication { get; set; }
        public bool AnyCriminalOffence { get; set; }
        public string IfAnyCriminalOffence { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        public virtual DesignationTable DesignationTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeavingTable> EmployeeLeavingTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryTable> EmployeeSalaryTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffAttendanceTable> StaffAttendanceTables { get; set; }
        public virtual UserTable UserTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTblTable> TimeTblTables { get; set; }
    }
}
