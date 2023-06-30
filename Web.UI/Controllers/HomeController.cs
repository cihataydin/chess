using Chess.Rules;
using Chess.Rules.Taslar;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Chess.Rules.Sabitler;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;


namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        public List<Kare> Kareler { get; set; }
        public HomeController()
        {
            Kareler = new List<Kare>();
            TaslarıYarat();
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Tahta()
        {    
            return View(Kareler);
        }
       
        public  void  TaslarıYarat()
        {
            Kale.Yerlestir(this.Kareler);
            At.Yerlestir(this.Kareler);
            Fil.Yerlestir(this.Kareler);
            Piyon.Yerlestir(this.Kareler);
            Sah.Yerlestir(this.Kareler);
            Vezir.Yerlestir(this.Kareler);
        }
    }



}
