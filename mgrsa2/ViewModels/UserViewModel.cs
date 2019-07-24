using mgrsa2.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Models;

namespace mgrsa2.Models
{
    public class UserFormModel
    {
        //public AppUser user { get; set; }
        //public Profile profile { get; set; }
        //public EamLogin login { get; set; }      

        public string Id { get; set; }

        public int ProfileId { get; set; }
        public int LoginId { get; set; }
        public string Codigo { get; set; } //000AAAA
        public string Nombre { get; set; }             
        public bool Activo { get; set; }  //5

        public string Phone { get; set; }
        public string Extension { get; set; }
        public int ContactosPermitidos { get; set; }
        public string LoginTmkLeadSources { get; set; }
        public string UserName { get; set; }


        public string Password { get; set; }
        public string Email { get; set; }   
        public int SupervisorId { get; set; }  //loginSupervisor 
        public int? PhoneProviderId { get; set; }

        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }

        
        public List<SelectListItem> PhoneProveedores { get; set; }
        public List<SelectListItem> Supervisores { get; set; }
        public List<SelectListItem> Codigos { get; set; }
        public ResponseModel resp { get; set; }

    }
      

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }

    public class RoleExt : IdentityRole
    {
        //userId
        //roleId
        public bool Active { get; set; }
        public string Group { get; set; }
        public string GroupId { get; set; }
        public string Usuarios { get; set; }
        public string RoleInitial { get; set; }

        public List<SelectListItem> Groups { get; set; }
        public ResponseModel resp { get; set; }


    }
    public class RoleForm
    {
        public string userId { get; set; } //0
        public string roleId { get; set; }
        public bool Active { get; set; }
        public string Nombre { get; set; }
        public string Group { get; set; }    //4


        public int ProfileId { get; set; }
        public int LoginId { get; set; }
        public string grupo { get; set; }
        public string roleInitial { get; set; }

    }
    public class UserRoleForm
    {
        public UserRoleForm()
        {
            this.response = new ResponseModel();
            this.roles = new List<RoleExt>();
        }

        public UserProfile userProfile { get; set; }
        public List<RoleExt> roles { get; set; }
        public List<SelectListItem> rolessel { get; set; }
        public ResponseModel response { get; set; }

    }


    public class UserProfile
    {
        public UserProfile()
        {
            this.Id = "";
            this.roles = new List<string>(new string[] { "", "", "", "", "" });
        }

        public string Id { get; set; }    //0
        public string Email { get; set; }
        public string Password { get; set; } //2
        public string UserName { get; set; }
        public int ProfileId { get; set; }
        public int LoginId { get; set; }   //5
        public string Nombre { get; set; }
        public string Codigo { get; set; } //7
        public bool Activo { get; set; }
        public string Phone { get; set; }
        public string Extension { get; set; }
        public int PhoneProviderId { get; set; }
        public int SupervisorId { get; set; }
        public string PhoneFmt { get; set; }

        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }


        public List<string> roles { get; set; }


    }


    public class UsersProfiles
    {
        public UsersProfiles()
        {
            this.resp = new ResponseModel();
            this.usersprofiles = new List<UserProfile>();
        }

        public List<UserProfile> usersprofiles;
        public ResponseModel resp { get; set; }

        public Profile agente;

        public Profile user;

    }

    public class UserPfEntryCards
    {
        public UserProfile userpf { get; set; }
        public List<EntryCard> entrycards { get; set; }
        public ResponseModel response { get; set; }

    }

    public class Auxiliar
    {
        public int Id { get; set; }
        public string Abrev { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }

    }

    public class ProfileForm
    {

        #region NO MODIFICABLE

        public string UserId { get; set; }
        public int ProfileId { get; set; }
        public int LoginId { get; set; }
        public string Codigo { get; set; } //000AAAA             
        public string UserName { get; set; } //000AAAA 
        public string Email { get; set; } //000AAAA                   

        #endregion


        public string Password { get; set; } //000AAAA        
        public bool Activo { get; set; }
        public string Nombre { get; set; }
        public int SupervisorID { get; set; }  //loginSupervisor 
        public string Phone { get; set; }
        public string Extension { get; set; }
        public int? PhoneProviderID { get; set; }
        public int ContactosPermitidos { get; set; }

        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }


        public ResponseModel resp { get; set; }

    }


    public class PhoneProviderForm
    {
        public PhoneProviderForm()
        {
            this.response = new ResponseModel();
        }

        [Display(Name = "Proveedor de Servicio Id")]
        public string PhoneProviderId { get; set; }

        [Display(Name = "Nombre del proveedor")]
        public string Description { get; set; }

        [Display(Name = "Sms dirección")]
        public string Address { get; set; }

        [Display(Name = "Longitud de texto mínima")]
        public int FromLength { get; set; }

        [Display(Name = "Longitud de texto máxima")]
        public int MaxLength { get; set; }


        public ResponseModel response { get; set; }

    }

    public class PhoneProviders
    {
        public PhoneProviders()
        {
            this.response = new ResponseModel();
            this.provs = new List<PhoneProvider>();
        }

        public List<PhoneProvider> provs { get; set; }
        public ResponseModel response { get; set; }

    }
    public class Locations
    {
        public Locations()
        {
            this.response = new ResponseModel();
            this.locs = new List<Location>();
        }

        public List<Location> locs { get; set; }
        public ResponseModel response { get; set; }

    }

    public class LocationForm
    {

        [Display(Name = "Location Id")]
        public int Id { get; set; }
        [Display(Name = "Location IP ")]
        public string IP { get; set; }
        [Display(Name = "Descripción ")]
        public string Description { get; set; }

        [Display(Name = "Activa")]
        public bool Active { get; set; }

        [Display(Name = "Editada")]
        public DateTime EditStamp { get; set; }


    }


    public class EdicionForm
    {

        [Display(Name = "Id")]
        public int EdicionId { get; set; }

        [Display(Name = "Edicion")]
        public string Edicion { get; set; }

        [Display(Name = "Fecha Publicación")]
        public DateTime Fecha { get; set; }


        [Display(Name = "Fecha Limite Aviso")]
        public DateTime? FechaAviso { get; set; }

        [Display(Name = "Hora Limite Aviso")]
        public DateTime? HoraAviso { get; set; }



        [Display(Name = "Fecha Limite Definitiva")]
        public DateTime? FechaCierre { get; set; }

        [Display(Name = "Hora Limite Definitiva")]
        public DateTime? HoraCierre { get; set; }



        [Display(Name = "Fecha Publicación")]
        public string sFecha { get; set; }

        [Display(Name = "Fecha Limite Aviso")]
        public string sFechaAviso { get; set; }

        [Display(Name = "Hora Limite Aviso")]
        public string sHoraAviso { get; set; }



        [Display(Name = "Fecha Limite Definitiva")]
        public string sFechaCierre { get; set; }

        [Display(Name = "Hora Limite Definitiva")]
        public string sHoraCierre { get; set; }



        [Display(Name = "Edicion Actual")]
        public bool edActual { get; set; }


        public ResponseModel response { get; set; }

    }
    public class Edicion
    {

        [Display(Name = "Id")]
        public int EdicionId { get; set; }

        [Display(Name = "Edicion")]
        public string edicion { get; set; }

        [Display(Name = "Fecha Publicación")]
        public string Fecha { get; set; }


        [Display(Name = "Fecha Limite Aviso")]
        public string FechaAviso { get; set; }

        [Display(Name = "Hora Limite Aviso")]
        public string HoraAviso { get; set; }



        [Display(Name = "Fecha Limite Definitiva")]
        public string FechaCierre { get; set; }

        [Display(Name = "Hora Limite Definitiva")]
        public string HoraCierre { get; set; }



        //[Display(Name = "Fecha Publicación")]
        //public string sFecha { get; set; }

        //[Display(Name = "Fecha Limite Aviso")]
        //public string sFechaAviso { get; set; }

        //[Display(Name = "Hora Limite Aviso")]
        //public string sHoraAviso { get; set; }



        //[Display(Name = "Fecha Limite Definitiva")]
        //public string sFechaCierre { get; set; }

        //[Display(Name = "Hora Limite Definitiva")]
        //public string sHoraCierre { get; set; }



        [Display(Name = "Edicion Actual")]
        public bool edActual { get; set; }

    }

    public class DoorAccessForm
    {

        [Display(Name = "Fecha Actual Limite")]
        public string sActualFecha { get; set; }

        [Display(Name = "Nueva Fecha")]
        public string sNuevaFecha { get; set; }

        public ResponseModel response { get; set; }

    }

    public class Mensaje
    {
        public bool response { get; set; }
        public string message { get; set; }
        public int identity { get; set; }
        public string sIdentity { get; set; }
        public Mensaje()
        {
            this.response = false;
            this.message = "";
            this.identity = 0;
            this.sIdentity = "";
        }

    }

    //News and Alerts

    public class NewAlert
    {
        public NewAlert()
        {
            this.Id = 0;
            this.title = "";
            this.memo = "";
            this.publish = false;
            this.orden = 0;
            this.type = "";
            this.fromDate = "";
            this.toDate = "";
            this.index = 0;
        }
        public int Id { get; set; }
        public string title { get; set; }
        public string memo { get; set; }
        public bool publish { get; set; }
        public double orden { get; set; }
        public string type { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int index { get; set; }

        public string stamp { get; set; }
    }


    public class LoginIntra
    {

        public LoginIntra()
        {
            this.login = new LoginModel();
            this.news = new List<NewAlert>();
            this.alerts = new List<NewAlert>();
            this.resp = new ResponseModel();
        }
        public LoginModel login { get; set; }
        public List<NewAlert> news { get; set; }
        public List<NewAlert> alerts { get; set; }
        public ResponseModel resp { get; set; }

    }

    public class routeVM
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }

}