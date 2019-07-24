namespace mgrsa2.Models
{
    //using Interfaces;
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;
    //using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    //[Table("TaskFU")]
    public class TaskFU
    {
        public int TaskFUId { get; set; }

        public int TaskId { get; set; }

        public int? Advance { get; set; }

        public string TaskFUDate { get; set; }

        //[Required]
        public string Notes { get; set; }
              
        //public virtual Task Task { get; set; }
        
    }
}
