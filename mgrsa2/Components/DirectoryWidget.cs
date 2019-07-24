using mgrsa2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mgrsa2.Components
{
    public class DirectoryWidget : ViewComponent
    {
        private IUserServices _directoryService;

        public DirectoryWidget(IUserServices directoryService)
        {
            _directoryService = directoryService;
        }

        //public IViewComponentResult Invoke()
        //{

        //    var directory = _directoryService.GetDirectory();

        //    return View(directory);
        //}

        //public IViewComponentResult Invoke(string search)
        //{

        //    var directory = _directoryService.GetDirectoryProfile(search);

        //    return View(directory);
        //}

        public async Task<IViewComponentResult> InvokeAsync(string search)
        {
            var directory = _directoryService.GetDirectoryProfile(search);

            return View(directory);
        }


    }
    public class DirectoryItem
    {
        public DirectoryItem()
        {
        }

        public int Id { get; set; }
        public string Areas { get; set; }

        public string Codigo { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }

        public string Phone { get; set; }

        public string Extension { get; set; }

        public string LoginDept { get; set; }
        
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedIn { get; set; }



    }

}
