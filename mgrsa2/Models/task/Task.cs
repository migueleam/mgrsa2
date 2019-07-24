namespace mgrsa2.Models
{
    //using Interfaces;
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;
    //using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    //[Table("Task")]
    public class Task
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Task()
        //{
        //    Attachments = new HashSet<Attachment>();
        //    TaskFUs = new HashSet<TaskFU>();
        //}

        public int taskId { get; set; }   //0

        //[Required]
        //[StringLength(150)]
        public string description { get; set; }

        public int advance { get; set; }

        public int loginID { get; set; }

        public int? priorityId { get; set; }

        public int? departmentId { get; set; }  //5

        public int? statusTaskId { get; set; }

        public string dueDate { get; set; }

        //public virtual Department Department { get; set; }

        //public virtual Login Login { get; set; }

        //public virtual Priority Priority { get; set; }

        //public virtual StatusTask StatusTask { get; set; }

        //[Required]
        public string notes { get; set; }  //8

        public int userId { get; set; }  //9

        
        #region Auditoria
        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }
        #endregion

    }
}
