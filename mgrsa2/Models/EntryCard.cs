using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mgrsa2.Models
{
    public class EntryCard
    {
        public int EntryCardId { get; set; }
        public string UserId { get; set; }
        public string Card { get; set; }  //   
        public int ProfileId { get; set; }
        public bool Active { get; set; }
        public int LoginId { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateStamp { get; set; } = DateTime.UtcNow;
        public int CreateLogiId { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? EditStamp { get; set; } = DateTime.UtcNow;

        public int EditLogiId { get; set; }

        public int CreateProfileId { get; set; }
        public int EditProfileId { get; set; }



    }
}
