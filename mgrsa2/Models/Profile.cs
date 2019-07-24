using mgrsa2.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mgrsa2.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public int LoginId { get; set; }

        [Required]
        [Display(Name = "Codigo EAM")]
        [DefaultValue("000TEMP")]
        public string Codigo { get; set; } //000AAAA

        [Required]
        [Display(Name = "Nombre Completo")]
        [DefaultValue("")]
        public string Nombre { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Activo { get; set; }

        public string Phone { get; set; }

        public string Extension { get; set; }

        [DefaultValue(25)]
        public int ContactosPermitidos { get; set; }

        public string LoginTmkLeadSources { get; set; }
        //existed
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        //para Roles
        public int LevelId { get; set; } //1 Admin, 2 Editor, 3 Usuario (Cambio)
        public int SupervisorId { get; set; }  //loginSupervisor 
        public bool Supervisor { get; set; }   //SupevisorB 
        public string LoginDept { get; set; }   //
        public bool CoSupervisor { get; set; }
        public string Areas { get; set; }
        public int? PhoneProviderId { get; set; }
        public string LoginTelemarketingArea { get; set; }
        public bool LoginTelemarketing { get; set; }
        public bool LoginTelemarketingSupervisor { get; set; }
        public bool LoginOt { get; set; }
        public bool LoginDesign { get; set; }
        public bool LoginAdminRpts { get; set; }

        ///-------------------------------------------------------------------

        //public AppUser AppUser { get; set; }

        //public PhoneProvider PhoneProvider { get; set; }   

        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }

    }

    public class myProfile
    {

        public myProfile()
        {
            this.ProfileId = 0;
            this.LoginId = 0;
            this.Nombre = "";
            this.Phone = "";
            this.Extension = "";
            this.UserName = "";
            this.Email = "";
            this.PhoneProviderId = 0;
            this.twitter = "";
            this.facebook = "";
            this.instagram = "";
            this.linkedIn = "";            

            this.resp = new ResponseModel();
        }

        public int ProfileId { get; set; }
        public int LoginId { get; set; }

        public string Nombre { get; set; }
        public string Phone { get; set; }
        public string Extension { get; set; }
        public string UserName { get; set; }

        //public string Password { get; set; }

        public string Email { get; set; }
        public int? PhoneProviderId { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }
        public ResponseModel resp { get; set; }

    }
}
