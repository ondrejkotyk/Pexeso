using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso
{
    public class GenerateField
    {
        public static Card[] cards = new Card[12];
        public static void Generate()
        {
            var nums = Enumerable.Range(1, 4).ToArray(); // Vytvoří pole s prvky 1,2,3,4
            ShuffleAnArray(4, nums); // Funkce zamíchá náhodně prvky v poli
            var nums1 = Enumerable.Range(1, 3).ToArray();
            ShuffleAnArray(3, nums);

            Card c1 = new Card(1, nums1[2], nums[0]);
            Card c2 = new Card(2, nums1[2], nums[2]);
            Card c3 = new Card(3, nums1[2], nums[1]);
            Card c4 = new Card(4, nums1[2], nums[3]); // vytvaření karet
            Card c5 = new Card(5, nums1[1], nums[0]);
            Card c6 = new Card(6, nums1[1], nums[3]);
            Card c7 = new Card(1, nums1[1], nums[2]);
            Card c8 = new Card(2, nums1[1], nums[1]);
            Card c9 = new Card(3, nums1[0], nums[3]);
            Card c10 = new Card(4, nums1[0], nums[1]);
            Card c11 = new Card(5, nums1[0], nums[2]);
            Card c12 = new Card(6, nums1[0], nums[0]);

            var supportArrayCards = new[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 };

            for (int u = 0; u <= 11; u++)
            {
                cards[GiveCardIndex(supportArrayCards[u].Row, supportArrayCards[u].Column)] = supportArrayCards[u]; // každá karta se přiřadí do místečka na poli hraní
            }
            //cards[GiveCardIndex(c1.Row, c1.Column)] = c1;
        }
        private static int GiveCardIndex(int a, int b)
        {
            int index = 0;
            for (int i = 1; i <= 3; i++)
            {
                for (int y = 1; y <= 4; y++) // metoda zajistujici na jake pozici má karta být na hracím poli
                {
                    if (a == i && b == y)
                    {
                        return index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            return index;
        }
        static void ShuffleAnArray(int a, int[] arr) //naše čísla v poli se navzájem prohodí
        {
            var rnd = new Random();
            for (int i = 0; i < a; ++i)
            {
                int randomIndex = rnd.Next(a); //vygeneruje náhodné číslo
                int temp = arr[randomIndex]; //pomocná proměnná pro uložení hodnoty
                arr[randomIndex] = arr[i]; //prohození hodnoty
                arr[i] = temp; // nahrání hodnoty z pomocné prommné zpět
            }
        }

    }


}
