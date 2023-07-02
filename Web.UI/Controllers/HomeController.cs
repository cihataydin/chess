using Chess.Rules;
using Chess.Rules.Taslar;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Chess.Rules.Sabitler;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using Web.UI.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public List<Kare> Kareler { get; set; }
        public string OncekiKareKoordinat { get; set; }
        public int? Sayac { get; set; }
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
            TasıHareketEttir(onClickModel);

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

        private void TasıHareketEttir(OnClickModel onClickModel)
        {
            Sayac = HttpContext.Session.GetInt32("Sayac") is null ? 0: HttpContext.Session.GetInt32("Sayac");

            if (Sayac == 0)
            {
                this.OncekiKareKoordinat = onClickModel.X.ToString() + onClickModel.Y.ToString();
                HttpContext.Session.SetString("OncekiKareKoordinat", OncekiKareKoordinat);

                if (Convert.ToBoolean(onClickModel.KareDolumu))
                {
                    Sayac++;
                    HttpContext.Session.SetInt32("Sayac", (int)Sayac);
                }
            }
            else if (Sayac == 1)
            {
                OncekiKareKoordinat = HttpContext.Session.GetString("OncekiKareKoordinat");

                if (Convert.ToBoolean(onClickModel.KareDolumu) && onClickModel.X.ToString() + onClickModel.Y.ToString() != OncekiKareKoordinat)
                {
                    Kare oncekiKare = Kareler.Where(kare => kare.Koordinat.X.ToString() + kare.Koordinat.Y.ToString() == OncekiKareKoordinat).FirstOrDefault();
                    Kare hedefKare = Kareler.Where(kare => kare.Koordinat.X.ToString() + kare.Koordinat.Y.ToString() == onClickModel.X.ToString() + onClickModel.Y.ToString()).FirstOrDefault();

                    oncekiKare.Tas.HareketEt(oncekiKare, hedefKare, this.Kareler, oncekiKare.Tas.UygunKareleriHesapla);
                }

                Sayac--;
            }
        }
    }
}
