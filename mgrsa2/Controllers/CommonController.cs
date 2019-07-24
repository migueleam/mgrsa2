using mgrsa2.Common;
using mgrsa2.Models;
using mgrsa2.Services;
using mgrsa2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace mgrsa2.Controllers
{

    [Authorize]
    public class CommonController : Controller
    {
        private UserManager<AppUser> userManager;
        private ICommonServices _commonservices;
        private IUserServices _userservices;
        private AppIdentityDbContext _db;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private IHostingEnvironment _hostingEnvironment;

        public CommonController(UserManager<AppUser> userMgr
            , ICommonServices commonservices
            , IUserServices userservices
            , AppIdentityDbContext db

            , IUserValidator<AppUser> userValid
            , IPasswordValidator<AppUser> passValid
            , IPasswordHasher<AppUser> passwordHash
            , IHostingEnvironment hostingEnvironment
            )
        {
            userManager = userMgr;
            _commonservices = commonservices;
            _userservices = userservices;
            _db = db;

            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;  
            _hostingEnvironment = hostingEnvironment;
        }

             

        public string GetSupervisores()
        {
            List<SelectListItem> supervisores = new List<SelectListItem>();
            List<SelectListItemPlain> supervisoresP = new List<SelectListItemPlain>();
            supervisores = _commonservices.GetSupervisores();

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (supervisores.Count > 0)
            {
                supervisoresP = _commonservices.ListPlain(supervisores);
                sdata = Objeto.SerializarLista(supervisoresP, '|', '~', false);
            }
            return sdata;
        }

        public string GetPhoneProveedores()
        {
            List<SelectListItem> listI = new List<SelectListItem>();
            List<SelectListItemPlain> listIP = new List<SelectListItemPlain>();
            listI = _commonservices.GetPhoneProvidersSel();

         
            string sdata = "";
            if (listI.Count > 0)
            {
                listIP = _commonservices.ListPlain(listI);
                sdata = Objeto.SerializarLista(listIP, '|', '~', false);
            }
            return sdata;
        }

        public JsonResult GetPhoneProveedoresJ()
        {
            List<SelectListItem> listI = new List<SelectListItem>();
            listI = _commonservices.GetPhoneProvidersSel();           
            return Json(listI);
        }

        public string GetContactosPermitidos()
        {
            List<SelectListItem> listI = new List<SelectListItem>();
            List<SelectListItemPlain> listIP = new List<SelectListItemPlain>();
            listI = _commonservices.GetContactosPermitidos();
                       
            string sdata = "";
            if (listI.Count > 0)
            {
                listIP = _commonservices.ListPlain(listI);
                sdata = Objeto.SerializarLista(listIP, '|', '~', false);
            }
            return sdata;
        }

        public string GetRoles()
        {
            List<SelectListItem> listI = new List<SelectListItem>();
            List<SelectListItemPlain> listIP = new List<SelectListItemPlain>();
            listI = _commonservices.GetRolesSelect();

            string sdata = "";
            if (listI.Count > 0)
            {
                listIP = _commonservices.ListPlain(listI);
                sdata = Objeto.SerializarLista(listIP, '|', '~', false);
            }
            return sdata;
        }

        public string GetRolesGps()
        {
            List<SelectListItem> listI = new List<SelectListItem>();
            List<SelectListItemPlain> listIP = new List<SelectListItemPlain>();
            listI = _commonservices.GetRoleGroups();

            string sdata = "";
            if (listI.Count > 0)
            {
                listIP = _commonservices.ListPlain(listI);
                sdata = Objeto.SerializarLista(listIP, '|', '~', false);
            }
            return sdata;
        }


        public string GetCodigos(string texto)
        {
            string codigos = "<br/><span class='f-s-12 f-w-700 text-success'>COD NO REPETIDO</span>";
            List<Profile> list = new List<Profile>();

            list = _db.Profiles
                   .Where(p => p.Codigo.Contains(texto))
                   .OrderBy(p => p.Codigo)
                   .ToList();

            if (list.Count > 0)
            {
                codigos = "<label >Codigos</label>";
                codigos += "<select id='codigos' class='form-control' data-size='5' data-allow-clear='true'>";
                //codigos += "<option></option>";
                foreach (Profile item in list)
                {
                    codigos += "<option value='" + item.Codigo + "'>" + item.Codigo + "</option>";
                }
                codigos += "</select>";
            }

            return codigos;
        }
        public string GetEdiciones()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetEdiciones(32, -1, DateTime.Now);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public JsonResult GetEdActual()
        {
            EdicionForm ed = new EdicionForm();

            ed = _commonservices.GetEdicionActual();

            return Json(ed);
        }


        public string GetEdicionesPmt(int total, int future)
        {
            //--Obtiene edicones:
            //-- actual es: 1,2,null
            //-- la que se entrega: 1,1,null
            //-- dos anteriores...1,-1,null

            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetEdiciones(total, future, DateTime.Now);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }
        /* -------------- distribucion */

        #region Distribucion     

        public string GetUsersByRole(string role)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetUsersByRole(role);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }
        public string GetUsersByRoleSel(string role)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetUsersByRoleSel(role);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public string GetUsersByRoleSelCod(string role)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetUsersByRoleSelCod(role);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public string GetTipoUbic()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetTipoUbic();

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }                                    
        
        public string GetAreas()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetAreas();

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }
        public string GetAreasAv()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetAreasAv();

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public string GetRoutes()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetRoutesSelect("");

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public string GetRoutesPmt(string areaid)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetRoutesSelect(areaid);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
        }

        public string GetSchedulesPmt(int routeid)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItemPlain> listP = new List<SelectListItemPlain>();
            list = _commonservices.GetSchedulesSelect(routeid);

            ResponseModel resp = new ResponseModel();
            string sdata = "";
            if (list.Count > 0)
            {
                listP = _commonservices.ListPlain(list);
                sdata = Objeto.SerializarLista(listP, '|', '~', false);
            }
            return sdata;
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


        /* UPDALOAD FILES */

                   
        [HttpPost]
        public async Task<IActionResult> AUploadFile(List<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot",
                            file.FileName);

                //file.GetFileName() x file.FileName

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return RedirectToAction("Files");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, _commonservices.GetContentType(path), Path.GetFileName(path));


        }
        [HttpPost]
        public string UploadFile(IFormFile file)                 
        {
              
            ResponseModel resp = new ResponseModel();                    


            if (file == null || file.Length == 0)
                return "Sin Información";

            //IFileProvider provider = new PhysicalFileProvider(_hostingEnvironment.ContentRootPath);
            //IDirectoryContents contents = provider.GetDirectoryContents(""); // the applicationRoot contents
            //IFileInfo fileInfo = provider.GetFileInfo("wwwroot/js/site.js");

            //var path = Path.Combine(
            //            Directory.GetCurrentDirectory()
            //          , @"directorio\trafico",
            //           file.FileName);

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, @"editorial", file.FileName);
            //var path = @"~/editorial/" + file.FileName + _commonservices.GetExtension(file.ContentType);
        
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                resp.message = "Archivo Uploaded";
            }
            catch (Exception ex)
            {
                resp.message = "upload error:" + ex.Message;
            }

            return resp.message;
        }

        //[HttpGet("DownloadDocument")]
        //public FileStreamResult DownloadDocument(string fileName)
        //{
        //    //var path = Path.Combine(_hostingEnvironment.WebRootPath, @"directorio\editorial", fileName);
        //    //var path = Path.Combine(@"D:\websites\elaviso.com\www\", @"directorio\editorial", fileName);
        //    var path = Path.Combine(@"C:\", @"directorio\editorial\", fileName);

        //    var net = new System.Net.WebClient();
        //    var data = net.DownloadData(path);
        //    var content = new System.IO.MemoryStream(data);
               
        //    return File(content, _commonservices.GetContentType(path), fileName);
        //}

        //public FileStreamResult DownloadDocumentA(string fileName)
        //{                  

        //    //var filePath = Path.Combine(_hostingEnvironment.WebRootPath, @"editorial", fileName);
        //    //var path = Path.Combine(@"D:\websites\elaviso.com\www\", @"directorio\editorial", fileName);
        //    var path = Path.Combine(@"C:\", @"directorio\editorial\", fileName);
            

        //    //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        //    //return File(fileBytes, "application/force-download", fileName);

        //    var memory = new MemoryStream();

        //    try
        //    {
        //        using (var stream = new FileStream(path, FileMode.Open))
        //        {

        //            await stream.CopyToAsync(memory);

        //        }

        //        memory.Position = 0;
        //    }
        //    catch(Exception ex)
        //    {
        //        var msg = ex.Message;
        //    }


        //    string ct = _commonservices.GetContentType(path);
        //    string sfileName = Path.GetFileName(path);

        //    Response.ContentType = ct;
           

        //    return File(memory, _commonservices.GetContentType(path), fileName);
            


        //}

        public string GetWebRootPath()
        {

            //string filePath = _hostingEnvironment.WebRootPath;
            string filePath = @"D:\websites\elaviso.com\www\" + @"directorio\editorial";

            return filePath;

        }

    }


}
