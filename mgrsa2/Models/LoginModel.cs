
using mgrsa2.Common;
using System.ComponentModel.DataAnnotations;

namespace mgrsa2.Models
{
    public class LoginModel
    {

        public LoginModel()
        {
            this.resp = new ResponseModel();           
        }

        [Required(ErrorMessage = "User Name requerido")]
        [UIHint("Username")]
        public string Username { get; set; }

        //[Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Requerido")]
        [UIHint("password")]
        public string Password { get; set; }

        public ResponseModel resp { get; set; }
    }
}
