using Chess.Rules.Sabitler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Rules.Taslar
{
    public class Sah : ITas
    {
        public string Resim { get; set; }
        public Renk Renk { get; set; }

        public bool HareketEt(Kare baslangıcKare, Kare hedefKare, List<Kare> kareler)
        {
            List<Kare> uygunKareler = UygunKareleriHesapla(baslangıcKare.Koordinat, kareler);

            bool hareketEdilebilir = uygunKareler.Contains(hedefKare);

            if (hareketEdilebilir)
            {
                ITas tas = baslangıcKare.Tas;

                baslangıcKare.Tas = null;
                baslangıcKare.Durum = KareDurum.Bos;
                baslangıcKare.Image = null;

                hedefKare.Tas = tas;
                hedefKare.Durum = KareDurum.Dolu;
                hedefKare.Image = Image.FromFile(Resim);
            }

            return hareketEdilebilir;
        }

        public List<Kare> UygunKareleriHesapla(Koordinat koordinat, List<Kare> kareler)
        {
            List<Kare> _kareler = new List<Kare>();

            Kare kare = kareler.Where(k => k.Koordinat.X == koordinat.X + 1 && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X + 1 && k.Koordinat.Y == koordinat.Y).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X + 1 && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X - 1 && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X - 1 && k.Koordinat.Y == koordinat.Y).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X - 1 && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
            KareyiEkle(_kareler, kare);

            return _kareler;
        }

        public static void Yerlestir(List<Kare> kareler)
        {
            foreach (Kare kare in kareler)
            {
                if ((kare.Koordinat.X == 4 && kare.Koordinat.Y == 1))
                {
                    kare.Tas = new Sah{ Renk = Renk.Beyaz, Resim = $"{Environment.CurrentDirectory}{TasResimleri.BEYAZSAH}" };
                    kare.Image = Image.FromFile($"{Environment.CurrentDirectory}{TasResimleri.BEYAZSAH}");
                    kare.Durum = KareDurum.Dolu;
                }

                if((kare.Koordinat.X == 4 && kare.Koordinat.Y == 8))
                {
                    kare.Tas = new Sah { Renk = Renk.Siyah, Resim = $"{Environment.CurrentDirectory}{TasResimleri.SIYAHSAH}" };
                    kare.Image = Image.FromFile($"{Environment.CurrentDirectory}{TasResimleri.SIYAHSAH}");
                    kare.Durum = KareDurum.Dolu;
                }
            }
        }

        private void KareyiEkle(List<Kare> kareler, Kare kare)
        {
            if (kare?.Tas is null)
            {
                kareler.Add(kare);
            }
            else if (kare?.Tas != null && kare?.Tas.Renk != this.Renk)
            {
                kareler.Add(kare);
            }
        }
    }
}
