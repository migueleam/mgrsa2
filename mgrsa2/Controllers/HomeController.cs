using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Services;
using mgrsa2.Models;
using System.Collections.Generic;
using System.Linq;

namespace mgrsa2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserServices _userservices;
        private AppIdentityDbContext _db;

        public HomeController(
            UserManager<AppUser> userMgr
            , IUserServices userservices
            , AppIdentityDbContext db)
        {
            userManager = userMgr;
            _userservices = userservices;
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult InProcess()
        {
            return View();
        }
        public IActionResult Lock()
        {
            return View();
        }

        #region DIRECTORY

        [HttpPost]
        public IActionResult Directory(string texto)
        {
            //search = string.IsNullOrEmpty(search) ? "Miguel" : search;

            return ViewComponent("DirectoryWidget", new { search = texto });
        }

        #endregion

        public string GetCodigos(string texto)
        {
            string codigos = "<br/><span class='text-big text-success'>COD NO REPETIDO</span>";
            List<Profile> list = new List<Profile>();
            list = _db.Profiles
                   .Where(p => p.Codigo.Contains(texto))
                   .OrderBy(p => p.Codigo)
                   .ToList();

            if (list.Count > 0)
            {
                codigos = "<label class='form-label form-label-sm'>Codigos existentes</label>";
                codigos += "<select id='codigos' class='select2 form-control' data-size='15' data-style='btn-warning'>";
                //codigos += "<option value='0'>Códigos encontrados</option>";
                foreach (Profile item in list)
                {
                    codigos += "<option value='" + item.Codigo + "'>" + item.Codigo + "</option>";
                }
                codigos += "</select>";
            }

            return codigos;
        }
       
   

    }
}
