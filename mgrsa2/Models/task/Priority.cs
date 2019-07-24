namespace mgrsa2.Models
{
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;
    //using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    //[Table("Priority")]
    public partial class Priority
    {
        public int PriorityId { get; set; }

        //[StringLength(100)]
        public string Description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        // public virtual ICollection<Task> Tasks { get; set; }
    }
}
