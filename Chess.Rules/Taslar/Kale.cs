using Chess.Rules.Sabitler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.Rules.Taslar
{
    public class Kale : ITas
    {
        public Kale()
        {

        }

        public Renk Renk { get ; set ; }
        public string Resim { get; set; }

        public void HareketEt()
        {
            throw new NotImplementedException();
        }
        public List<Koordinat> UygunKareleriHesapla(Koordinat koordinat, List<Kare> kareler)
        {
            List<Koordinat> koordinatlar = new List<Koordinat>();

            for (int i = koordinat.Y + 1; i < 9; i++)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == i).FirstOrDefault();

                if(kare.Tas is null)
                {
                    koordinatlar.Add(kare.Koordinat);
                }
                else if(kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare.Koordinat);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }

                // TODO: Yeniden Düzenle ve kısalt
            }

            for (int i = koordinat.Y - 1; i > 0; i--)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == koordinat.X && k.Koordinat.Y == i).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare.Koordinat);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare.Koordinat);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }

                // TODO: Yeniden Düzenle ve kısalt
            }

            for (int i = koordinat.X + 1; i < 9; i++)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == i && k.Koordinat.Y == koordinat.Y).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare.Koordinat);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare.Koordinat);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }

                // TODO: Yeniden Düzenle ve kısalt
            }

            for (int i = koordinat.X - 1; i > 0; i--)
            {
                Kare kare = kareler.Where(k => k.Koordinat.X == i && k.Koordinat.Y == koordinat.Y).FirstOrDefault();

                if (kare.Tas is null)
                {
                    koordinatlar.Add(kare.Koordinat);
                }
                else if (kare.Tas != null && kare.Tas.Renk != this.Renk)
                {
                    koordinatlar.Add(kare.Koordinat);
                    break;
                }
                else if (kare.Tas != null && kare.Tas.Renk == this.Renk)
                {
                    break;
                }

                // TODO: Yeniden Düzenle ve kısalt
            }

            return koordinatlar; 
        }

        public static Kare Yerlestir(List<Kare> kareler)
        {
            foreach (Kare kare in kareler)
            {
                if((kare.Koordinat.X == 1 && kare.Koordinat.Y == 1) || (kare.Koordinat.X == 8 && kare.Koordinat.Y == 1))
                {
                    kare.Tas = new Kale { Renk = Renk.Beyaz, Resim = Environment.CurrentDirectory + @"\..\..\Resimler\kale.png" };
                    kare.Button.Image = Image.FromFile(Environment.CurrentDirectory + @"\..\..\Resimler\kale.png");

                    return kare;
                }

                // TODO: Siyah iki kale (1,8) ve (8,8) kordinatlarına yerleştirilecek.
            }

            return null;

            // TODO: test için dönüş tipi değiştirilmiştir. Düzeltilecek.
        }
    }
}
