namespace mgrsa2.Models
{
    
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;
    //using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    //[Table("Idea")]
    public class Idea
    {
        public int IdeaId { get; set; }   //0
        //[Required]
        //[StringLength(150)]
        public string Description { get; set; }

        public int Advance { get; set; }

        public int LoginID { get; set; }

        public int PriorityID { get; set; }

        public int DepartmentID { get; set; }  //5

        public int StatusIdeaId { get; set; }

        public string DueDate { get; set; }

        public int CreateBy { get; set; }

        public string CreateDate { get; set; }

        public int UpdateBy { get; set; }  //

        public string UpdatedDate { get; set; }

        //[Required]
        public string Notes { get; set; }  //12

        public int userID { get; set; }    //13  
   

    }
}