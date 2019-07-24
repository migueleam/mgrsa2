
namespace mgrsa2.Models
{
   
    using System;  
    //using System.ComponentModel.DataAnnotations;
    
    //[Table("IdeaFU")]
    public class IdeaFU
    {
        public int IdeaFUId { get; set; }

        public int IdeaID { get; set; }

        public int? Advance { get; set; }

        public string IdeaFUDate { get; set; }

        //[Required]
        public string Notes { get; set; }

    }
}
