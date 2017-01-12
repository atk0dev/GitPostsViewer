using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GitWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["WebRootPath"] = _environment.WebRootPath;

            var fs = new FileStore($@"{_environment.WebRootPath}\posts");
            var files = fs.GetFiles("*.md");
            
            ViewData["FilesAmount"] = files?.Count();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Fill free to contact me.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
