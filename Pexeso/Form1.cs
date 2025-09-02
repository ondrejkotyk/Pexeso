using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Form1 : Form
    {
        // Pomocné globální proměnné
        public int clickNumber = 0; // pomocná proměnná pro počet kliknutí
        public int totalNumberOfClicks = 1; // pomocná proměnná pro výpočet výhry
        public int player1 = 0;
        public int player2 = 0; //body
        public string card1;
        public string card2; // pomocné proměnné pro uložení karty, kterou si uživatel zaklikl
        public int row;
        public int column;
        public int position;
        public int remove2Card; // pomocná proměnná pro odebrání z karty z pole
        public int win;

        public Form1()
        {
            InitializeComponent();
            GenerateField.Generate();
            HideCards();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public int GetCardsIndex(Control c) // metoda získá číslo kartičky na kterou hráč klikl
        {
            // cislo radky na hraci plose
            row = (tableLayoutPanel1.GetRow(c) + 1);
            // cislo sloupce na hraci plose
            column = (tableLayoutPanel1.GetColumn(c) + 1);
            // vysledna position nebo-li index karticky
            position = (row * 4) - (4 - column);
            // vratime cislo karticky na dane pozici
            return position;
        }

        public void ShowCards(Control c) // samotne grafické přiřazení karet k obrázkům
        {
            GetCardsIndex(c);
            for (int i = 0; i < 12; i++) // ukáže kartikučku na kterou uživatel klikl, dá jí tag, podle kartičky na této pozici
            {
                if ((GenerateField.cards[i].Column == column) && (GenerateField.cards[i].Row == row)) // pozice hráče se shoduje s pozicí karty
                {
                    tableLayoutPanel1.Controls[i].BackgroundImage = imageList1.Images[GenerateField.cards[i].Id];
                    tableLayoutPanel1.Controls[i].Tag = GenerateField.cards[i].Id;
                    tableLayoutPanel1.Controls[i].Enabled = false; // Aby nešlo 2x zmáčknout stejnou klávesu
                }
            }
            card1 = ((Control)c).Tag.ToString(); //tag nahraje do promenné
        }
        private void HideCards()
        {
            //vyplnění panelu základním obrázkem pomocí imagelistu
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                c.BackgroundImage = imageList1.Images[0];
                c.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void CardAvailability(bool EnableDisable) // ošetření proti mačkaní jedné karty vícekrát
        {
            if (EnableDisable == false)
            {
                for (int i = 0; i < 12; i++)
                {
                    tableLayoutPanel1.Controls[i].Enabled = false; // na žádnóu kartu se nedá kliknout znovu (několikanásobné klikání)
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    tableLayoutPanel1.Controls[i].Enabled = true; // na všechny karty se dá kliknout znovu (několikanásobné klikání)
                }
            }
        }

        private void Points()
        {
            if (totalNumberOfClicks % 2 == 0) //počítaní bodů pro hráče
            {
                player1++;
            }
            else
            {
                player2++;
            }
        }
        private void Win()
        {
            if (player2 > player1)
            {
                MessageBox.Show("Hráč 2 Vyhrál!!");
            }
            if (player2 < player1)
            {
                MessageBox.Show("Hráč 1 Vyhrál!!");
            }
            if (player2 == player1)
            {
                MessageBox.Show("Remíza!!!");
            }
            Application.Restart();
        }
        private void OnClick(object sender, EventArgs e)
        {
            clickNumber++;
            card2 = card1; // pomocné uložení proménné neboli první karty na kterou uživatel klikl

            if (clickNumber > 2) // jestliže jsou otočené 2 karty
            {
                HideCards();
                CardAvailability(false); // oštetření proti několikanásobném klikání
                clickNumber = 0;
                CardAvailability(true);
                totalNumberOfClicks++;
            }
            else
            {
                ShowCards((Control)sender);
                GetCardsIndex((Control)sender);
            }

            if (card1 == card2 && clickNumber == 2) //jestliže se tagy karet shodují, tak obě kartičky schováme
            {
                win++;
                totalNumberOfClicks++;
                tableLayoutPanel1.Controls[GetCardsIndex((Control)sender) - 1].Visible = false;
                tableLayoutPanel1.Controls[remove2Card - 1].Visible = false;
                CardAvailability(false); //zablokuje ostatní karty
                clickNumber = 0;
                HideCards(); //všechny karty zakreje
                CardAvailability(true);
                Points(); // přidá body hráči
                label2.Text = player1.ToString();
                label3.Text = player2.ToString(); // zobrazí body
            }
            if (win >= 6)
            {
                Win(); // našlo se všech 6 párů, konec hry
            }
            remove2Card = GetCardsIndex((Control)sender); // pomocná proměnná pro schování první kliknuté karty
        }

        private void PravidlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(); // Podokno pro pravidla
            f2.ShowDialog();
        }
    }
}
