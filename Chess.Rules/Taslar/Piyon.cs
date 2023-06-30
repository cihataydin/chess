using Chess.Rules.Sabitler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Rules.Taslar
{
    public class Piyon : Tas, ITas
    {
        public List<Kare> UygunKareleriHesapla(Koordinat koordinat, List<Kare> kareler)
        {
            List<Kare> _kareler = new List<Kare>();

            if (this.Renk == Renk.Beyaz)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == koordinat.X + 1 && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
                CarprazKareEkle(_kareler, kare);

                kare = kareler.Where(k => k.Koordinat.X == koordinat.X - 1 && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
                CarprazKareEkle(_kareler, kare);

                kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y + 1).FirstOrDefault();
                KareyiEkle(_kareler, kare);

                if (!Oynadı && kare.Durum == KareDurum.Bos)
                {
                    kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y + 2).FirstOrDefault();
                    KareyiEkle(_kareler, kare);
                }

            }
            else if (this.Renk == Renk.Siyah)
            {


                Kare kare = kareler.Where(k => k.Koordinat.X == koordinat.X + 1 && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
                CarprazKareEkle(_kareler, kare);

                kare = kareler.Where(k => k.Koordinat.X == koordinat.X - 1 && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
                CarprazKareEkle(_kareler, kare);

                kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y - 1).FirstOrDefault();
                KareyiEkle(_kareler, kare);

                if (!Oynadı && kare.Durum == KareDurum.Bos)
                {
                    kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == koordinat.Y - 2).FirstOrDefault();
                    KareyiEkle(_kareler, kare);
                }
            }

            return _kareler;
        }


        public static void Yerlestir(List<Kare> kareler)
        {
            foreach (Kare kare in kareler)
            {
                if (kare.Koordinat.Y == 2)
                {
                    kare.Tas = new Piyon { Renk = Renk.Beyaz, Resim = $"{Environment.CurrentDirectory}{TasResimleri.BEYAZPIYON}" };
                    kare.Resim = $"{Environment.CurrentDirectory}{TasResimleri.BEYAZPIYON}";
                    kare.Durum = KareDurum.Dolu;
                }

                if (kare.Koordinat.Y == 7)
                {
                    kare.Tas = new Piyon { Renk = Renk.Siyah, Resim = $"{Environment.CurrentDirectory}{TasResimleri.SIYAHPIYON}" };
                    kare.Resim = $"{Environment.CurrentDirectory}{TasResimleri.SIYAHPIYON}";
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
        }

        private void CarprazKareEkle(List<Kare> kareler, Kare kare)
        {
            if (kare?.Durum == KareDurum.Dolu && kare?.Tas.Renk != this.Renk)
                kareler.Add(kare);
        }
    }
}
