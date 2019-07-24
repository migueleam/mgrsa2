using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Services;
using mgrsa2.Models;
using mgrsa2.Common;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mgrsa2.Controllers
{

    [Authorize(Roles = "Admin, Supervisor, Asistente, Editor, Usuario")]
    [Authorize(Roles = "Editorial, Tecnico")]

    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserServices _userservices;

        private IAdminServices _adminservices;
        private IAuxServices _auxservices;
        //private IArtServices _artservices;
        private ICommonServices _commonservices;

        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        public object Objecto { get; private set; }

        //private UserManager<AppUser> userManager;
        //private IProspectoServices _prospectoservices;

        //private IOrdenServices _ordservices;      
        //private AppIdentityDbContext _db;

        public AdminController(
            UserManager<AppUser> usrMgr,
            RoleManager<IdentityRole> roleMgr,
            IAdminServices adminservices,
            IUserServices userservices,
            IAuxServices auxservices,
            //IArtServices artservices,
            ICommonServices commonservices,
            
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash
        )
        {
            userManager = usrMgr;
            roleManager = roleMgr;
            _userservices = userservices;
            _adminservices = adminservices;
            _auxservices = auxservices;
            //_artservices = artservices;
            _commonservices = commonservices;

            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        #region EDICIONES

        public IActionResult Ediciones(string vsearch = "")
        {

            Ediciones eds = new Ediciones();
            eds.ediciones = _adminservices.GetEdiciones();
            eds.response.message = "er|NO DATOS ENCONTRADOS| Ediciones Mgr";

            eds.response.route.Add(new routeVM() { nombre = "refreshTable", valor = "false" });
            eds.response.route.Add(new routeVM() { nombre = "search", valor = vsearch });

            if (eds.ediciones.Count > 0)
            {
                eds.response.message = "ok|Ediciones Leidos| Edicione Mgr|";
                List<Edicion> edact = eds.ediciones.Where(e => e.edActual).ToList();
                if (edact.Count > 0)
                    eds.response.message += edact[0].edicion;
            }


            return View(eds);

        }

        public string GetEdicion(string edicionid)
        {
            Edicion ed = _adminservices.GetEdicion(edicionid);

            string sResult = "er|NO DATOS ENCONTRADOS|Ediciones Mgr";

            if (ed != null)
            {
                sResult = Objeto.SerializarItem(ed, '|', '~', false);
            }
            return sResult;
        }
        public string UpdateEdicion(Edicion model)
        {
            ResponseModel resp = new ResponseModel();
            resp.message = "er|Edicion NO ACTUALIZADA|Edicion Mgr|0";

            if (model != null)
            {
                if (model.edicion != "0")
                {
                    resp = _adminservices.UpdateEdicion(model);
                }

                resp.message += "^" + Objeto.SerializarItem(model, '|', '~', false);

            }

            return resp.message;

        }


        #endregion
       
        #region AGENDA

        public IActionResult agendaG()
        {

            var userId = userManager.GetUserId(User);
            Profile user = _userservices.GetCurrentUser(userId);

            EventList ev = new EventList();

            ev.usuarios = _auxservices.GetUsuarios("E", "0", "0", "0");

            ev.resp.message = "er|NO DATOS ENCONTRADOS|AGENDA";

            if (ev.eventosl.Count > 0)
            {
                ev.resp.message = "ok|Eventos Leidos|AGENDA";
            }

            return View(ev);

        }

        public IActionResult agenda()
        {

            var userId = userManager.GetUserId(User);
            Profile user = _userservices.GetCurrentUser(userId);

            EventList ev = new EventList();
            
            ev.usuarios = _auxservices.GetUsuarios("I", "0", "0",user.Codigo);

            ev.resp.message = "er|NO DATOS ENCONTRADOS|AGENDA";

            if (ev.eventosl.Count > 0)
            {
                ev.resp.message = "ok|Eventos Leidos|AGENDA";
            }

            return View(ev);

        }

        #endregion

        #region AREAS

        public IActionResult Areas(string vsearch = "")
        {

            Areas areas = new Areas();
            areas.areas = _auxservices.GetAreas();
            areas.response.message = "wn|NO DATOS ENCONTRADOS| Dist";

            areas.response.route.Add(new routeVM() { nombre = "refreshTable", valor = "false" });
            areas.response.route.Add(new routeVM() { nombre = "search", valor = vsearch });

            if (areas.areas.Count > 0)
            {
                areas.response.message = "ok|Areas Leidas| Dist|";

            }

            return View(areas);

        }

        #endregion

        #region CHECKLIST

        public IActionResult Avance()
        {
            //primer view

            return View();
                       
        }             
               
        
        #endregion


        #region PROFILE

     
        public JsonResult GetMyProfile()
        {

            var userId = userManager.GetUserId(User);
            Profile user = _userservices.GetCurrentUser(userId);

            myProfile myprof = new myProfile();

            myprof.ProfileId = user.ProfileId;
            myprof.LoginId = user.LoginId;
            myprof.Nombre = user.Nombre;
            myprof.Phone = user.Phone;
            myprof.Extension = user.Extension;
            myprof.UserName = user.UserName;

            myprof.Email = user.Email;
            myprof.PhoneProviderId = user.PhoneProviderId;
            myprof.twitter = user.twitter;
            myprof.facebook = user.facebook;
            myprof.instagram = user.instagram;
            myprof.linkedIn = user.linkedIn;

            return Json(myprof);

        }
      
        public JsonResult UpdateMyProfile(myProfile model)
        {

            var userId = userManager.GetUserId(User);
            Profile user = _userservices.GetCurrentUser(userId);
           
            model.ProfileId = user.ProfileId;                             
           
            myProfile myprof = new myProfile();
            myprof.resp.message = "er|My Profile NO Actualizado| Users";

            myprof.resp = _userservices.UpdateMyProfile(model);
            user = _userservices.GetCurrentUser(userId);           

            myprof.ProfileId = user.ProfileId;
            myprof.LoginId = user.LoginId;
            myprof.Nombre = user.Nombre;
            myprof.Phone = user.Phone;
            myprof.Extension = user.Extension;
            myprof.UserName = user.UserName;

            myprof.Email = user.Email;
            myprof.PhoneProviderId = user.PhoneProviderId;
            myprof.twitter = user.twitter;
            myprof.facebook = user.facebook;
            myprof.instagram = user.instagram;
            myprof.linkedIn = user.linkedIn;

            return Json(myprof);

        }

      
        public JsonResult UpdateAuth(int profileId, int loginId, string newAuth)
        {
            ResponseModel resp = new ResponseModel();                       
            var userId = userManager.GetUserId(User);

            Profile userprof = _userservices.GetCurrentUser(userId);
            AppUser user = GetFindUser(userId).Result;

            if (user != null)
            {

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(newAuth))
                {
                    //validPass =  await passwordValidator.ValidateAsync(userManager,user, model.Password);
                    validPass = GetPasswordValidator(user, userprof.Password).Result;

                    if (validPass.Succeeded)
                    {                        
                        user.PasswordHash = passwordHasher.HashPassword(user, newAuth);
                    }
                    else
                    {                      
                        resp.response = true;
                        resp.message = "wn| Password No validado (revise caracteres) |Usuarios Mgr";

                    }
                }

                //string hashpassw = passwordHasher.HashPassword(user, newPassw);

                if (!string.IsNullOrEmpty(newAuth) && validPass.Succeeded)
                {                    
                    IdentityResult result = UpdateUserAsync(user).Result;

                    if (result.Succeeded)
                    {
                        resp.response = true;
                        resp.message = "ok| Usuario Actualizado |Usuarios Mgr";
                    }
                    else
                    {
                        //AddErrorsFromResult(result);
                        resp.response = true;
                        resp.message = "wn| Password No validado| Usuarios Mgr ";
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario No Encontrado");

                resp.response = true;
                resp.message = "wn| Usuario No encontrado| Admin";
            }

            return Json(resp);

        }
        private async Task<AppUser> GetFindUser(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            return user;
        }
        private async Task<IdentityResult> GetPasswordValidator(AppUser user, string Password)
        {
            IdentityResult identityResult = await passwordValidator.ValidateAsync(userManager, user, Password);
            return identityResult;
        }
        private async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            IdentityResult identityResult = await userManager.UpdateAsync(user);
            return identityResult;
        }

        #endregion

        #region ROLES

        public IActionResult Roles(string vsearch = "")
        {
            //var userId = userManager.GetUserId(User);
            //Profile user = _userservices.GetCurrentUser(userId);

            UserRoleForm uroles = new UserRoleForm();
            uroles.roles = _adminservices.GetRolesExt("", vsearch, true);

            uroles.response.route.Add(new routeVM() { nombre = "refreshTable", valor = "false" });
            uroles.response.route.Add(new routeVM() { nombre = "search", valor = vsearch });

            uroles.response.message = "er|NO DATOS ENCONTRADOS|Mgr ROLES";

            if (uroles.roles.Count > 0)
            {
                uroles.response.message = "ok|Usuarios Leidos|Usuarios";
            }

            return View(uroles);

        }

        public string GetRole(string roleid)
        {
            List<RoleExt> roles = new List<RoleExt>();
            RoleExt role = _adminservices.GetRoleExt(roleid);
            //role.Groups = _adminservices.GetRoleGroups();
            string sResult = "er|NO DATOS ENCONTRADOS|Mgr ROLES";


            if (role.Id != null)
            {
                roles.Add(role);
                sResult = Objeto.SerializarLista(roles, '|', '~', false);
            }
            return sResult;
        }
        public string GetRoles(string texto)
        {
            List<RoleExt> roles = new List<RoleExt>();
            roles = _adminservices.GetRoles(texto);

            string sResult = "er|NO DATOS ENCONTRADOS|Mgr ROLES";

            if (roles.Count > 0)
            {
                sResult = "ok|ROL EXISTENTE|Mgr ROLES";
            }
            return sResult;
        }

        public string UpdateRole(
                string roleId,
                string nombre,
                string inicial,
                string grupo,
                string activo
            )
        {

            ResponseModel resp = new ResponseModel();
            resp.message = "wn| Role No encontrado|Roles Mgr";


            RoleExt role = new RoleExt();

            //prof = _userservices.GetProfileForm(int.Parse(xprofileId));

            if (role != null)
            {
                role.Id = roleId;
                role.Name = nombre;
                role.RoleInitial = inicial;
                role.GroupId = grupo;
                role.Active = (activo == "1" ? true : false);

                if (!string.IsNullOrEmpty(role.Id))
                {
                    //update  
                    resp = _adminservices.UpdateRole(role);
                }
                else
                {
                    //create
                    IdentityResult result = CreateRole(role.Name).Result;
                    if (result.Succeeded)
                    {
                        //con roleId updateRole Parcial //inicial y grupo...
                        IdentityRole newRole = FindRoleByNombre(role.Name).Result;
                        role.Id = newRole.Id;
                        resp = _adminservices.UpdateRole(role);
                        if (resp.response)
                            resp.message = "ok| Role Adicionado|Roles Mgr|1000";
                    }
                    else
                    {
                        resp.message = "er| Role NO Adicionado|Roles Mgr";

                    }
                }


            }

            return resp.message;

        }

        private async Task<IdentityResult> CreateRole(string Nombre)
        {
            IdentityRole newrole = new IdentityRole(Nombre);
            IdentityResult identityResult = await roleManager.CreateAsync(newrole);
            return identityResult;
        }

        private async Task<IdentityRole> FindRoleByNombre(string Nombre)
        {
            IdentityRole role = new IdentityRole(Nombre);
            IdentityRole identityResult = await roleManager.FindByNameAsync(Nombre);
            return role;
        }


        #endregion



        #region Locations

        public IActionResult Locs(string vsearch = "")
        {

            Locations locs = new Locations();
            locs.locs = _adminservices.GetLocations();
            locs.response.message = "er|NO DATOS ENCONTRADOS|Puntos de Acceso Mgr";

            locs.response.route.Add(new routeVM() { nombre = "refreshTable", valor = "false" });
            locs.response.route.Add(new routeVM() { nombre = "search", valor = vsearch });

            if (locs.locs.Count > 0)
            {
                locs.response.message = "ok|Puntos de Acceso Leidos|Punto de Acceso Mgr";
            }

            return View(locs);

        }
        public string GetLoc(string locid)
        {

            //List<PhoneProviderForm> locs = new List<PhoneProvider>();
            Location loc = _adminservices.GetLocation(Convert.ToInt32(locid));

            string sResult = "er|NO DATOS ENCONTRADOS|Puntos de Accesos Mgr";

            if (loc != null)
            {
                sResult = Objeto.SerializarItem(loc, '|', '~', false);
            }
            return sResult;
        }
        public string UpdateLoc(Location model)
        {
            ResponseModel resp = new ResponseModel();
            resp.message = "er|Punto de Acceso NO ACTUALIZADO|Punto de Acceso Mgr|0";


            if (model != null)
            {
                if (model.Id != 0)
                {
                    resp = _adminservices.UpdateLocation(model);
                }
                else
                {
                    resp = _adminservices.CreateLocation(model);
                    if (resp.response)
                        model.Id = resp.identity;
                }

                resp.message += "^" + Objeto.SerializarItem(model, '|', '~', false);

            }

            return resp.message;

        }

        #endregion

    }


}

