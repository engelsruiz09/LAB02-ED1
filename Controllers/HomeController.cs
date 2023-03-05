using LAB02_ED1_G.Models;
using LAB02_ED1_G.Models.Datos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAB02_ED1_G.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return View();
            return View(Singleton.Instance.ArbolVehiculos.ObtenerLista());
            
        }

        public IActionResult Privacy()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}