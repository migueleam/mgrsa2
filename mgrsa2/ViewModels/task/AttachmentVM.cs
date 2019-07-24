using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mgrsa2.Common;

namespace mgrsa2.ViewModels
{
    public class AttachmentVM
    {
        public AttachmentVM()
        {
            this.attachmentId = 0; //0
            this.fileName = "";//1
            this.notes = "";//2
            this.extension = "";//3
            this.taskId = 0;
            this.attachmentDate = "";
            this.ideaId = 0;
            this.userId = 0;
            this.user = "";
            this.resp = new ResponseModel();
        }
               
        public int attachmentId { get; set; }  //0
        public string fileName { get; set; } //1
        public string notes { get; set; } //2
        public string extension { get; set; } //3
        public int taskId { get; set; }
        public string attachmentDate { get; set; }        
        public int ideaId { get; set; }  

        public int userId { get; set; }
        public string user { get; set; }
        
        public ResponseModel resp { get; set; }
    }
}
