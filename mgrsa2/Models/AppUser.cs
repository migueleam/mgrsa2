using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mgrsa2.Models {

    public class AppUser : IdentityUser
    {
        public int LoginId { get; internal set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }   //1 to 1        

    }
}
