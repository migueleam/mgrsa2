using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgrsa2.ViewModels
{
        public class IdeaFUVM
        {

            public IdeaFUVM()
            {
                this.fuId = 0;
                this.ideaId = 0;
                this.advance = 0;
                this.notes = "";
                this.fuDate = "";
                this.ideaId = 0;
                this.complete = false;
            }


            public int fuId { get; set; }
            public int ideaId { get; set; }
            public int advance { get; set; }                
            public string notes { get; set; }
            public string fuDate { get; set; }        
            public bool complete { get; set; }    
        
       }
}
