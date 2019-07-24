using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Models;
using mgrsa2.Common;
using mgrsa2.Services;

namespace mgrsa2.Controllers
{
    
        [Authorize(Roles = "Admin")]
        public class UsersController : Controller
        {
            //Access to the user data is through the
            //UserManager<AppUser> object that is received by the controller constructor and provided through
            //dependency injection.

            private UserManager<AppUser> userManager;
            private IUserValidator<AppUser> userValidator;
            private IPasswordValidator<AppUser> passwordValidator;
            private IPasswordHasher<AppUser> passwordHasher;
            IUserServices _userservices;

            public UsersController(UserManager<AppUser> usrMgr,
                    IUserValidator<AppUser> userValid,
                    IPasswordValidator<AppUser> passValid,
                    IPasswordHasher<AppUser> passwordHash,
                    IUserServices userservices
                )
            {
                userManager = usrMgr;
                userValidator = userValid;
                passwordValidator = passValid;
                passwordHasher = passwordHash;
                _userservices = userservices;

            }


            //public ViewResult Index() => View(userManager.Users);

            public IActionResult Indexva()
            {
                return View(_userservices.GetUserProfiles());
            }

            public IActionResult Index(string vsearch = "", bool brefreshTable = false)
            {

                var userId = userManager.GetUserId(User);
                Profile user = _userservices.GetCurrentUser(userId);

                UsersProfiles users = new UsersProfiles();
                users.usersprofiles = _userservices.GetUserProfiles();

                users.resp.message = "er|NO DATOS ENCONTRADOS|Usuarios";

                users.user = user;

                string vrefreshTable = (brefreshTable ? "true" : "false");
                users.resp.route.Add(new routeVM() { nombre = "search", valor = vsearch });
                users.resp.route.Add(new routeVM() { nombre = "refreshTable", valor = vrefreshTable });


                if (users.usersprofiles.Count > 0)
                {
                    users.resp.message = "ok|Usuarios Leidos|Usuarios";
                }

                return View(users);
            }

            //[HttpPost]
            //[ValidateAntiForgeryToken]
            public IActionResult RefreshIndex(string search)
            {
                return RedirectToAction("Index", new { vsearch = search });

            }


            #region USERS nueva version
            public string GetUser(string userid)
            {
                List<UserFormModel> users = new List<UserFormModel>();
                UserFormModel user = _userservices.GetUserForm(userid);

                string sdata = "er|NO DATOS ENCONTRADOS|Usuarios Mgr";

                if (!string.IsNullOrWhiteSpace(user.Id))
                {
                    users.Add(user);
                    sdata = Objeto.SerializarLista(users, '|', '~', false);
                }

                return sdata;
            }

            public string UpdateUser(string Id, string Password)
            {
                ResponseModel resp = new ResponseModel();

                AppUser user = GetFindUser(Id).Result;
                //AppUser user = await userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(Password))
                    {
                        //validPass =  await passwordValidator.ValidateAsync(userManager,user, model.Password);
                        validPass = GetPasswordValidator(user, Password).Result;

                        if (validPass.Succeeded)
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user, Password);
                        }
                        else
                        {
                            AddErrorsFromResult(validPass);

                            resp.response = true;
                            resp.message = "wn| Password No validado |Usuarios Mgr";

                        }
                    }


                    if (Password != string.Empty && validPass.Succeeded)
                    {

                        //IdentityResult result = await userManager.UpdateAsync(user);

                        IdentityResult result = UpdateUserAsync(user).Result;

                        if (result.Succeeded)
                        {
                            resp.response = true;
                            resp.message = "ok| Usuario Actualizado |Usuarios Mgr";
                        }
                        else
                        {
                            AddErrorsFromResult(result);
                            resp.response = true;
                            resp.message = "wn| Password No validado| Usuarios Mgr ";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");

                    resp.response = true;
                    resp.message = "wn| Usuario No encontrado|Usuarios Mgr";
                }

                return resp.message;

            }

            public string AddUser(string nombre, string usuario, string codigo, string passw, string search)
            {

                ResponseModel resp;
                UserFormModel model = new UserFormModel();
                List<UserFormModel> users = new List<UserFormModel>();

                resp = new ResponseModel();
                //RELLENAR VALUES CASE FAIL...
                //model.Supervisores = _userservices.GetSupervisores();
                //model.Codigos = _userservices.GetCodigos();
                //model.PhoneProveedores = _userservices.GetProveedores();

                //<!===========================================>
                //     CREAR LOGIN
                //<!===========================================>
                model.resp = resp;
                model.UserName = usuario.ToLower();
                model.Codigo = codigo.ToUpper();
                model.Nombre = nombre;
                model.Password = passw;

                model.resp = _userservices.AddLogin(model);
                model.LoginId = model.resp.identity;

                //trer resp.identity (loginId in case exist)
                // en case
                //model.LoginId = resp.identity;

                //<!===========================================>
                //     CREAR PROFILE 
                //<!===========================================>

                model.resp = _userservices.AddProfile(model);
                // trer resp.identity (loginId in case exist)
                // en case
                model.ProfileId = resp.identity;

                if (!model.resp.response)
                {
                    //ViewBag.typeMsg = "wn";
                    //ViewBag.message = model.resp.message;
                    return model.resp.message;
                }

                model.ProfileId = model.resp.identity;

                //<!===========================================>
                //     CREAR USER
                //<!===========================================>


                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.UserName + "@elaviso.com",
                    ProfileId = model.ProfileId,
                    LoginId = model.LoginId
                };

                IdentityResult result = CreateUser(user, model.Password).Result;

                if (result.Succeeded)
                {
                    model.Id = user.Id;
                    users.Add(model);
                    string sdata = "ok| Usuario Creado (" + model.LoginId.ToString() + ")" + "^";
                    sdata += Objeto.SerializarLista(users, '|', '~', false);
                    return sdata;
                }
                else
                {
                    return "er| " + resp.message + "| Users Mgr ";
                }


            }

            //private async Task<IdentityResult> CreateUser(AppUser user, string Password)
            //{
            //    IdentityResult identityResult = await userManager.CreateAsync(user, Password);
            //    return identityResult;
            //}

            #endregion
            public string GetProfile(string userId, int loginId, int profileId)
            {
                List<ProfileForm> profs = new List<ProfileForm>();
                ProfileForm prof = new ProfileForm();
                List<EntryCard> entrycards = new List<EntryCard>();
                List<RoleForm> roles = new List<RoleForm>();

                prof = _userservices.GetProfileForm(userId, loginId, profileId);
                //prof.resp.message = "er|NO DATOS ENCONTRADOS|Admin Mgr";
                profs.Add(prof);

                entrycards = _userservices.GetEntryCards("", 0, prof.ProfileId, "");
                roles = _userservices.GetRolesForm(userId);

                string sdata = "er|NO DATOS ENCONTRADOS|Admin Mgr";

                if (profs.Count > 0)
                {
                    sdata = Objeto.SerializarLista(profs, '|', '~', false);
                    sdata += "^";
                    if (entrycards.Count > 0)
                    {
                        sdata += Objeto.SerializarLista(entrycards, '|', '~', false);
                    }
                    sdata += "^";
                    if (roles.Count > 0)
                    {
                        sdata += Objeto.SerializarLista(roles, '|', '~', false);
                    }
                }

                return sdata;

            }
            public JsonResult GetProfileJ(string userId, int loginId, int profileId)
            {
                List<ProfileForm> profs = new List<ProfileForm>();
                ProfileForm prof = new ProfileForm();
                List<EntryCard> entrycards = new List<EntryCard>();
                List<RoleForm> roles = new List<RoleForm>();

                prof = _userservices.GetProfileForm(userId, loginId, profileId);
                //prof.resp.message = "er|NO DATOS ENCONTRADOS|Admin Mgr";
                profs.Add(prof);

                entrycards = _userservices.GetEntryCards("", 0, prof.ProfileId, "");
                roles = _userservices.GetRolesForm(userId);

                string sdata = "er|NO DATOS ENCONTRADOS|Admin Mgr";

                if (profs.Count > 0)
                {
                    sdata = Objeto.SerializarLista(profs, '|', '~', false);
                    sdata += "^";
                    if (entrycards.Count > 0)
                    {
                        sdata += Objeto.SerializarLista(entrycards, '|', '~', false);
                    }
                    sdata += "^";
                    if (roles.Count > 0)
                    {
                        sdata += Objeto.SerializarLista(roles, '|', '~', false);
                    }
                }

                return Json(sdata);

            }
        public string UpdateProf(string profileId,
                    string activopf,
                    string nombrepf,
                    string supervidorId,
                    string phonepf,
                    string extensionpf,
                    string phproveedorId,
                    string contactosPerm,
                    string twitter,
                    string facebook,
                    string instagram,
                    string linkedIn
                )
            {

                ResponseModel resp = new ResponseModel();
                resp.message = "wn| Usuario No encontrado|Usuarios Mgr";


                ProfileForm prof = new ProfileForm();

                //prof = _userservices.GetProfileForm(int.Parse(xprofileId));

                if (prof != null)
                {
                    prof.ProfileId = int.Parse(profileId);
                    prof.Activo = (activopf == "true") ? true : false;
                    prof.Nombre = nombrepf;
                    prof.SupervisorID = int.Parse(Global.ExtractOnlyNumeric(supervidorId));
                    prof.Phone = phonepf;
                    prof.Extension = extensionpf;
                    prof.PhoneProviderID = int.Parse(Global.ExtractOnlyNumeric(phproveedorId));
                    prof.ContactosPermitidos = int.Parse(Global.ExtractOnlyNumeric(contactosPerm));

                    prof.twitter = "" +  twitter;
                    prof.facebook = "" + facebook;
                    prof.instagram = "" + instagram;
                    prof.linkedIn = "" + linkedIn; 

                    resp = _userservices.UpdateProfile(prof);

                }

                return resp.message;

            }
            /// <summary>
            /// / ENTRY CARDS
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>


            //Se mantendra independiente para refresh    
            public string GetEntryCards(string profileId)
            {
                //ProfileForm prof = new ProfileForm();
                //prof = _userservices.GetProfileForm(userId,0,0);

                //ResponseModel resp = new ResponseModel();
                string sResult = "wn|NO tiene Tarjetas de Entrada|Mgr Card Entry";

                List<EntryCard> cards = new List<EntryCard>();

                cards = _userservices.GetEntryCards("", 0, int.Parse(profileId), "");


                if (cards.Count > 0)
                {
                    sResult = Objeto.SerializarLista(cards, '|', '~', false);
                }

                return sResult;

            }


            public string AddEntryCard(string userId, int loginId, int profileId, string card)
            {
                ResponseModel resp = new ResponseModel();

                var user = _userservices.GetUserProfiles(userManager.GetUserId(User).ToString(), 0, 0);

                if (profileId > 0 && loginId > 0 && !string.IsNullOrEmpty(card) && card.Length == 10)
                {
                    EntryCard Card = new EntryCard
                    {
                        Card = card,
                        ProfileId = profileId,
                        LoginId = loginId,
                        UserId = userId,

                        CreateProfileId = user.ProfileId,
                        EditProfileId = user.ProfileId,

                        CreateLogiId = user.LoginId,
                        EditLogiId = user.LoginId
                    };

                    resp = _userservices.AddEntryCard(Card);

                }
                else if (card.Length != 10)
                {
                    resp.message = "wn|Tarjeta NO Adicionada, número de digitos <> 10|ADICION DE TARJETA";
                    resp.response = false;

                }

                if (resp.response)
                {
                    resp.message = "ok|Tarjeta Adicionada (" + resp.identity.ToString() + ")|ADICION DE TARJETA";
                }
                else
                {
                    if (!string.IsNullOrEmpty(resp.message))
                        resp.message = "wn|Tarjeta NO Adicionada, revise información|ADICION DE TARJETA";
                }

                return resp.message;
            }

            public string ActiveEntryCard(int profileId, int loginId, int cardId = 0, bool active = false, string card = "")
            {
                ResponseModel resp = new ResponseModel();

                var user = _userservices.GetUserProfiles(userManager.GetUserId(User).ToString(), 0, 0);

                if (profileId > 0 && loginId > 0 && cardId != 0)
                {
                    EntryCard Card = new EntryCard
                    {
                        EntryCardId = cardId,
                        Card = card,
                        Active = active,
                        ProfileId = profileId,
                        LoginId = loginId,
                        EditLogiId = user.LoginId,
                        EditProfileId = user.ProfileId

                    };

                    resp = _userservices.UpdateEntryCard(Card);

                }

                if (resp.response)
                {
                    resp.message = "ok|Tarjeta (" + resp.identity.ToString() + ") Actualizada| ";
                    if (active)
                        resp.message += "ACTIVACION DE TARJETA";
                    else
                        resp.message += "DE-ACTIVACION DE TARJETA";
                }
                else
                {
                    if (!string.IsNullOrEmpty(resp.message))
                        resp.message = "wn|Tarjeta (" + resp.identity.ToString() + ") NO Actualizada, revise información | ACTIVACION DE TARJETA";
                }

                return resp.message;
            }


            #region ROLES

            /// <summary>
            /// ROLES 
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>

            //Se mantendra independiente para refresh   
            public string GetRoles(string userId)
            {
                List<RoleForm> roles = new List<RoleForm>();
                roles = _userservices.GetRolesForm(userId);

                string sResult = "wn|NO tiene Tarjetas de Entrada|Mgr Roles";
                if (roles.Count > 0)
                {
                    sResult = Objeto.SerializarLista(roles, '|', '~', false);
                }
                return sResult;

            }

            public string AddRoles(string userId, string loginId, string[] roles)
            {

                ResponseModel resp = new ResponseModel();
                UserRole userrole;
                string sResult = "ok|Roles Adicionados| ROLES";

                foreach (string roleId in roles)
                {
                    userrole = new UserRole() { UserId = userId, RoleId = roleId };
                    resp = _userservices.AddRole(userrole);
                    if (!resp.response)
                    {
                        sResult += "wn|Problemas parciales en Roles|ROLES";
                    }
                }
                if (resp.response)
                {
                    List<RoleForm> uroles = new List<RoleForm>();
                    uroles = _userservices.GetRolesForm(userId);
                    sResult = Objeto.SerializarLista(uroles, '|', '~', false);
                }
                else
                {
                    sResult = resp.message + "wn|Problemas al adicionar Roles|ROLES";
                }

                return sResult;
            }


            public string RemoveRole(string userId, string loginId, string roleId)
            {
                ResponseModel resp = new ResponseModel();
                UserRole userrole;

                userrole = new UserRole() { UserId = userId, RoleId = roleId };

                string sResult = "";

                resp = _userservices.RemoveRole(userrole);

                if (resp.response)
                {
                    if (string.IsNullOrEmpty(resp.message))
                    {

                        List<RoleForm> uroles = new List<RoleForm>();
                        uroles = _userservices.GetRolesForm(userId);
                        if (uroles.Count > 0)
                            sResult = Objeto.SerializarLista(uroles, '|', '~', false);
                        else
                            sResult = "ok|No existen Roles asignados|ROLES|1000";
                }
                }
                else
                {
                    if (string.IsNullOrEmpty(resp.message))
                    {
                        sResult = "wn|Problemas al Remover en Role|ROLES|1000";
                    }
                }

                return sResult;
            }

        #endregion


        #region USERS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        /// <returns></returns>


        private async Task<IdentityResult> CreateUser(AppUser user, string Password)
            {
                IdentityResult identityResult = await userManager.CreateAsync(user, Password);
                return identityResult;
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


            [HttpPost]
            [ValidateAntiForgeryToken]
            private void AddErrorsFromResult(IdentityResult result)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

        }
    #endregion


   


}