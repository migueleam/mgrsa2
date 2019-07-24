using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mgrsa2.Models;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Services;
using mgrsa2.Common;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mgrsa2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserServices _userservices;

        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        public AccountController(UserManager<AppUser> userMgr,
                SignInManager<AppUser> signinMgr,
                IUserServices userservices
                , IUserValidator<AppUser> userValid
                , IPasswordValidator<AppUser> passValid
                , IPasswordHasher<AppUser> passwordHash)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            _userservices = userservices;

            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        [AllowAnonymous]       
        public IActionResult Login()
        {           
            LoginModel details = new LoginModel();

            return View(details);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details)
        {
            details.Email = details.Username + "@elaviso.com";
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, details.Password, false, false);

                    if (result.Succeeded)
                    {
                       
                        //string fullname2 = principal.FindFirst("FullName").Value;

                        //var xroles = userManager.GetRolesAsync(user).Result;
                        //bool bRoles = xroles.Contains("Admin") || xroles.Contains("Diseño") || xroles.Contains("Editorial") || xroles.Contains("Contratacion");


                        //CREAR VIEWBAG...

                        //var userId = user.Id;
                        //Profile prof = _userservices.GetCurrentUser(userId);

                        //ViewBag.myProfile = new myProfile()
                        //{
                        //    ProfileId = prof.ProfileId,
                        //    LoginId = prof.LoginId,
                        //    Nombre = prof.Nombre,
                        //    Phone = prof.Phone,
                        //    Extension = prof.Extension,
                        //    UserName = prof.UserName,

                        //    Email = prof.Email,
                        //    PhoneProviderId = prof.PhoneProviderId,
                        //    twitter = prof.twitter,
                        //    facebook = prof.facebook,
                        //    instagram = prof.instagram,
                        //    linkedIn = prof.linkedIn
                        //};

                        //if (bRoles)
                        return RedirectToAction("Index", "Home");
                        //else {
                        //    details.resp.message = "in|Area de trabajo NO PERMITIDA|CheckList";
                            //return View(details); //return RedirectToAction("Login", "Account", details);
                        //}
                    }
                    
                }
                
                ModelState.AddModelError(nameof(LoginModel.Email),
                    "Invalid user or password");
                details.resp.message = "wn|User o Password Invalido| Revise sus datos";


            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

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

            ResponseModel response = new ResponseModel();
            response.message = "er|My Profile NO Actualizado| Users";

            response = _userservices.UpdateMyProfile(model);

            user = _userservices.GetCurrentUser(userId);
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

        public string UpdateAuth(int profileId, int loginId, string newAuth)
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

            return resp.message;

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

    }
}
