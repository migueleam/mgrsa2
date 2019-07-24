using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgrsa2.ViewModels
{
    public class TaskVM
    {
        public TaskVM()
        {            
            this.taskId = 0;
            this.ideaId = 0;
            this.userId = 0;
            this.priorityId = 0;
            this.departmentId = 0;
            this.statusTaskId = 0;
            this.description = "";
            this.advance = 0;
            this.userReq = "";
            this.priority = "";
            this.dept = "";
            this.status = "";
            this.dueDate = "";
            this.notes = "";
        }
               
        public int taskId { get; set; }
        public string description { get; set; }
        public int advance { get; set; }
        public string userReq { get; set; }
        public  string priority { get; set; }            
        public string dept { get; set; }
        public string status { get; set; }
        public string dueDate { get; set; }          
        public string notes { get; set; }
        public int userId { get; set; }
        public int ideaId { get; set; }

        public int priorityId { get; set; }
        public int departmentId { get; set; }
        public int statusTaskId { get; set; }
                   

        
    }
}
