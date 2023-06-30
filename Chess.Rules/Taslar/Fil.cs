using Chess.Rules.Sabitler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Rules.Taslar
{
    public class Fil : Tas, ITas
    {
        public List<Kare> UygunKareleriHesapla(Koordinat koordinat, List<Kare> kareler)
        {
            List<Kare> koordinatlar = new List<Kare>();

            for (int i = koordinat.Y + 1, j = koordinat.X + 1; 0 < i && i < 9 && j > 0 && j < 9; i++, j++)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == j && k.Koordinat.Y == i).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }

            }

            for (int i = koordinat.Y - 1, j = koordinat.X - 1; 0 < i && i < 9 && j > 0 && j < 9; i--, j--)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == j && k.Koordinat.Y == i).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }


            }

            for (int i = koordinat.Y + 1, j = koordinat.X - 1; 0 < i && i < 9 && j > 0 && j < 9; i++, j--)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == j && k.Koordinat.Y == i).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }


            }

            for (int i = koordinat.Y - 1, j = koordinat.X + 1; 0 < i && i < 9 && j > 0 && j < 9; i--, j++)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == j && k.Koordinat.Y == i).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }


            }

            return koordinatlar;
        }

        public static void Yerlestir(List<Kare> kareler)
        {
            foreach (Kare kare in kareler)
            {
                if ((kare.Koordinat.X == 3 && kare.Koordinat.Y == 1) || (kare.Koordinat.X == 6 && kare.Koordinat.Y == 1))
                {
                    kare.Tas = new Fil { Renk = Renk.Beyaz, Resim = $"{Environment.CurrentDirectory}{TasResimleri.BEYAZ_FIL}" };
                    kare.Resim = $"{Environment.CurrentDirectory}{TasResimleri.BEYAZ_FIL}";
                    kare.Durum = KareDurum.Dolu;
                }
                if (kare.Koordinat.X == 3 && kare.Koordinat.Y == 8 || (kare.Koordinat.X == 6 && kare.Koordinat.Y == 8))
                {
                    kare.Tas = new Fil { Renk = Renk.Siyah, Resim = $"{Environment.CurrentDirectory}{TasResimleri.SIYAH_FIL}" };
                    kare.Resim = $"{Environment.CurrentDirectory}{TasResimleri.SIYAH_FIL}";
                    kare.Durum = KareDurum.Dolu;
                }

            }
        }
    }
}
