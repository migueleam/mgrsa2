using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgrsa2.ViewModels
{
    public class TaskFUVM
    {

        public TaskFUVM()
        {
            this.fuId = 0;
            this.taskId = 0;
            this.fuAdvance = 0;
            this.fuNotes = "";
            this.fuDate = "";
            this.ideaId = 0;
            this.complete = false;
         }


         public int fuId { get; set; }
         public int taskId { get; set; }
         public int fuAdvance { get; set; }                
         public string fuNotes { get; set; }
         public string fuDate { get; set; }                  
         public int ideaId { get; set; }
         public bool complete { get; set; }

    }
}
