namespace mgrsa2.Models
{
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;
   
    public class Attachment
    {

        public Attachment()
        {
            this.attachmentId = 0;
            this.fileName = "";
            this.notes = "";
            this.extension = "";
            this.taskId = 0;
            this.attachmentDate = "";
            this.userId = 0;
            this.ideaId = 0;
        }

        public int attachmentId { get; set; }
        public string fileName { get; set; }        
        public string notes { get; set; }       
        public string extension { get; set; }
        public int taskId { get; set; }      
        public string attachmentDate { get; set; }
        public int ideaId { get; set; }
        public int userId { get; set; }

    }
}
