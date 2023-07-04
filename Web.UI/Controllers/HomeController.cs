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
using Web.UI.Services;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;

namespace Web.UI.Controllers
{
    public class HomeController : Controller 
    {
        public List<Kare> Kareler { get; set; }
        public string OncekiKareKoordinat { get; set; }
        public int? Sayac { get; set; }
        public string TahtaId { get; set; }

        public TahtaModel TahtaModel { get; set; }

        private readonly TahtaService _tahtaService;
        public HomeController(TahtaService tahtaService)
        {
            Kareler = new List<Kare>();
            KareleriYarat();
            TaslarıYerlestir();

            _tahtaService = tahtaService;
        }

        public IActionResult Tahta()
        {
            TahtaId = HttpContext.Session.GetString("TahtaId");

            if (string.IsNullOrEmpty(TahtaId))
            {
                TahtaId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("TahtaId", TahtaId);

                TahtaModel = new TahtaModel() { Id = TahtaId, Kareler = Kareler };
            }
            else
            {
                var sonuc = Get(TahtaId).Result.Value;

                if(sonuc is not null)
                {
                    // TODO: Interface hatası burada gerçekleşiyor. Deserilize ederken hata alıyor.
                    TahtaModel = new TahtaModel() { Id = TahtaId, Kareler = JsonConvert.DeserializeObject<List<Kare>>(sonuc.Kareler) };
                }
                
            }

            TahtaModel ??= new TahtaModel() { Id = TahtaId, Kareler = Kareler };

            return View(TahtaModel);
        }

        [HttpPost]
        public async Task<IActionResult> OnClick(OnClickModel onClickModel)
        {
            await TasıHareketEttir(onClickModel);

            return RedirectToAction("Tahta");
        }

        [HttpGet]
        public async Task<List<Tahta>> Get() =>
        await _tahtaService.GetAsync();

        [HttpGet]
        public async Task<ActionResult<Tahta>> Get(string id)
        {
            var tahta = await _tahtaService.GetAsync(id);

            if (tahta is null)
            {
                return NotFound();
            }

            return tahta;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tahta newTahta)
        {
            await _tahtaService.CreateAsync(newTahta);

            return CreatedAtAction(nameof(Get), new { id = newTahta.Id }, newTahta);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Tahta updatedTahta)
        {
            var tahta = await _tahtaService.GetAsync(id);

            if (tahta is null)
            {
                return NotFound();
            }

            updatedTahta.Id = tahta.Id;

            await _tahtaService.UpdateAsync(id, updatedTahta);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var tahta = await _tahtaService.GetAsync(id);

            if (tahta is null)
            {
                return NotFound();
            }

            await _tahtaService.RemoveAsync(id);

            return NoContent();
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

        private async Task TasıHareketEttir(OnClickModel onClickModel)
        {
            Sayac = HttpContext.Session.GetInt32("Sayac") is null ? 0 : HttpContext.Session.GetInt32("Sayac");

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
            else if (Sayac == 1 && !string.IsNullOrEmpty(onClickModel.Id))
            {
                OncekiKareKoordinat = HttpContext.Session.GetString("OncekiKareKoordinat");

                Kare oncekiKare = Kareler.Where(kare => kare.Koordinat.X.ToString() + kare.Koordinat.Y.ToString() == OncekiKareKoordinat).FirstOrDefault();

                if (oncekiKare.Tas is not null && onClickModel.X.ToString() + onClickModel.Y.ToString() != OncekiKareKoordinat)
                {
                    Kare hedefKare = Kareler.Where(kare => kare.Koordinat.X.ToString() + kare.Koordinat.Y.ToString() == onClickModel.X.ToString() + onClickModel.Y.ToString()).FirstOrDefault();

                    oncekiKare.Tas.HareketEt(oncekiKare, hedefKare, this.Kareler, oncekiKare.Tas.UygunKareleriHesapla);

                    // TODO: Kareler tas tipi Intreface şu an. Tas tipi class olmalı. Tas tipi class olmayınca jsona çevirirken hata veriyor. Çözüm bulunmalı.
                    CastAll();
                    TakeBackCastAll();

                    var tahtaKoleksiyon = new Tahta
                    {
                        Id = onClickModel.Id,
                        Kareler = JsonConvert.SerializeObject(Kareler)
                    };

                    await Create(tahtaKoleksiyon);
                }

                Sayac--;
            }
        }

        //private void TaslariSinifaCevir()
        //{
        //    foreach (var kare in Kareler)
        //    {
        //        if (kare.Durum == KareDurum.Dolu)
        //        {
        //            kare.Tas = kare.Tas.SınıfaCevir();
        //        }
        //    }
        //}

        public void CastAll()
        {

            List<Kare> kareler = Kareler.Select(t => t).Where(t => t.Tas != null).ToList();

            foreach (var kare in kareler)
            {
                switch (kare.Tas.Resim)
                {
                    case "Fil":
                        kare.TasTipleri.Fil = (Fil)kare.Tas;

                        break;
                    case "Sah":
                        kare.TasTipleri.Sah = (Sah)kare.Tas;

                        break;
                    case "At":
                        kare.TasTipleri.At = (At)kare.Tas;

                        break;
                    case "Piyon":
                        kare.TasTipleri.Piyon = (Piyon)kare.Tas;

                        break;
                    case "Vezir":
                        kare.TasTipleri.Vezir = (Vezir)kare.Tas;

                        break;
                    case "Kale":
                        kare.TasTipleri.At = (At)kare.Tas;

                        break;
                    default:
                        break;
                }
            }


        }

        public void TakeBackCastAll()
        {
            List<Kare> kareler = Kareler.Select(t => t).Where(t => t.TasTipleri.Fil != null || t.TasTipleri.Sah != null || t.TasTipleri.At != null || t.TasTipleri.Piyon != null || t.TasTipleri.Vezir != null || t.TasTipleri.Kale != null).ToList();

            foreach (var kare in kareler)
            {
                if (kare.TasTipleri.Fil != null)
                {
                    kare.Tas = kare.TasTipleri.Fil;
                    kare.TasTipleri.Fil = null;
                }
                else if (kare.TasTipleri.Sah != null)
                {
                    kare.Tas = kare.TasTipleri.Sah;
                    kare.TasTipleri.Sah = null;
                }
                else if (kare.TasTipleri.At != null)
                {
                    kare.Tas = kare.TasTipleri.At;
                    kare.TasTipleri.At = null;
                }
                else if (kare.TasTipleri.Piyon != null)
                {
                    kare.Tas = kare.TasTipleri.Piyon;
                    kare.TasTipleri.Piyon = null;
                }
                else if (kare.TasTipleri.Vezir != null)
                {
                    kare.Tas = kare.TasTipleri.Vezir;
                    kare.TasTipleri.Vezir = null;
                }
                else if (kare.TasTipleri.Kale!= null)
                {
                    kare.Tas = kare.TasTipleri.Kale;
                    kare.TasTipleri.Kale = null;
                }

            }
        }
    }
}
