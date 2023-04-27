using Chess.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Rules.Sabitler;
using Chess.Rules.Taslar;

namespace Desktop.UI
{
    public partial class Tahta : Form
    {
        public string Konum { get; set; }

        public int Sayac { get; set; }

        public List<Kare> Kareler { get; set; }

        public Tahta()
        {
            Kareler = new List<Kare>();
            InitializeComponent();
            TahtayıYarat();
            TaslarıYarat();
        }
        
        private void TaslarıYarat()
        {
            Kale.Yerlestir(this.Kareler);
            Fil.Yerlestir(this.Kareler); 
        }

        private void TahtayıYarat()
        {
            int x = 0; //butonları düzgün yerşeltirmek için (x-y) değişkenleri kullandık
            int y = 0; //butonları düzgün yerşeltirmek için (x-y) değişkenleri kullandık
            for (int i = 1; i < 9; i++)
            {

                for (int j = 1; j < 9; j++)
                {
                    Button button = new Button();
                    Kare kare = new Kare();

                    button.AccessibleName = $"{i}{j}";
                    kare.Koordinat.X = i;
                    kare.Koordinat.Y = j;

                    button.Click += delegate (object sender, EventArgs e)
                    {
                        TasıHareketEttir(sender, e, button);
                    };

                    if (j % 2 == 0)
                    {
                        if (i % 2 == 0)
                        {
                            button.BackColor = SystemColors.ButtonHighlight;
                            kare.Renk = Renk.Beyaz;
                        }
                        else
                        {
                            button.BackColor = SystemColors.ActiveCaptionText;
                            kare.Renk = Renk.Siyah;
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            button.BackColor = SystemColors.ActiveCaptionText;
                            kare.Renk = Renk.Siyah;
                        }
                        else
                        {
                            button.BackColor = SystemColors.ButtonHighlight;
                            kare.Renk = Renk.Beyaz;
                        }
                    }

                    button.Location = new Point(x, y);
                    button.Name = (i + j).ToString();
                    button.Size = new Size(100, 100);
                    Controls.Add(button);


                    kare.Button = button;
                    kare.Durum = KareDurum.Bos;
                    Kareler.Add(kare);

                    y += 100;
                }
                x += 100;
                y = 0;
            }

        }

        // TODO: Bu metot artık sadece button resimlerini değiştirmiyor. Ekstra olarak karelerdeki durumları güncelleyip taşların yerini değiştiriyor.
        // Yeni isim vermeliyiz. İsmi TasıHareketEttir olsun.
        private void TasıHareketEttir(object sender, EventArgs e, Button button)
        {
            if (Sayac == 0)
            {
                this.Konum = button.AccessibleName;

                if (button.Image != null)
                {
                    Sayac++;
                }
            }
            else if (Sayac == 1)
            {
                foreach (var control in Controls)
                {
                    Button oncekiButon = (Button)control;

                    if (oncekiButon.AccessibleName == Konum && oncekiButon.Image != null && button.AccessibleName != oncekiButon.AccessibleName)
                    {
                        Kare oncekiKare = Kareler.Where(kare => kare.Button.Equals(oncekiButon)).FirstOrDefault();
                        Kare hedefKare = Kareler.Where(kare => kare.Button.Equals(button)).FirstOrDefault();

                        oncekiKare.Tas.HareketEt(oncekiKare, hedefKare, this.Kareler);

                        break;
                    }
                }

                Sayac--;
            }
        }
    }
}
