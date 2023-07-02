using Chess.Rules;
using Chess.Rules.Taslar;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Chess.Rules.Sabitler;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using Web.UI.Models;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public List<Kare> Kareler { get; set; }
        public HomeController()
        {
            Kareler = new List<Kare>();
            KareleriYarat();
            TaslarıYerlestir();
        }

        public IActionResult Tahta()
        {    
            return View(Kareler);
        }

        [HttpPost]
        public IActionResult OnClick(OnClickModel onClickModel)
        {
            return RedirectToAction("Tahta");
        }

        private void TaslarıYerlestir()
        {
            Kale.Yerlestir(this.Kareler, true);
            At.Yerlestir(this.Kareler, true);
            Fil.Yerlestir(this.Kareler, true);
            Piyon.Yerlestir(this.Kareler, true);
            Sah.Yerlestir(this.Kareler, true);
            Vezir.Yerlestir(this.Kareler, true);
        }

        private void KareleriYarat()
        {
            int x = 0;
            int y = 0;
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {             
                    Kare kare = new Kare();               
                    kare.Koordinat.X = i;
                    kare.Koordinat.Y = j;
                    // TODO:click olayı eklenmeli.

                    if (j % 2 == 0)
                    {
                        if (i % 2 == 0)
                        {                           
                            kare.Renk = Renk.Beyaz;
                        }
                        else
                        {                    
                            kare.Renk = Renk.Siyah;
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {                        
                            kare.Renk = Renk.Siyah;
                        }
                        else
                        {                         
                            kare.Renk = Renk.Beyaz;
                        }
                    }

                    kare.Durum = KareDurum.Bos;
                    Kareler.Add(kare);
                 
                    y += 100;
                }
                x += 100;
                y = 0;
            }
        }
    }
}
