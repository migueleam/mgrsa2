namespace mgrsa2.Models
{  
    //[Table("Department")]
    public class Department
    {
        public int DepartmentId { get; set; }

        //[StringLength(100)]
        public string Description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Task> Tasks { get; set; }
    }
}
