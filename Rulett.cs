using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace rulett
{
    class mezo
    {
        public int ertek { get; private set; }
        public string szin { get; private set; }
        public mezo(string line)
        {
            ertek = int.Parse(line.Split(' ')[0]);
            szin = line.Split(' ')[1];
        }
    }


    internal class Program
    {
        static public Random rnd = new Random();
        static void Main(string[] args)
        {
            mezo[] kerek = new mezo[37];
            mezo[] tabla = new mezo[37];

            string[] fileAdat = File.ReadAllLines("..\\..\\..\\szamok_szinek.txt", Encoding.UTF8);
            for (int i = 0; i < fileAdat.Length; i++)
            {
                kerek[i] = new mezo(fileAdat[i]);
                tabla[kerek[i].ertek] = kerek[i];
            }
            string[,] tehetoTetek = new string[61, 3];
            StreamReader sr = new StreamReader("..\\..\\..\\TehetoTetek.txt", Encoding.UTF8);
            List<string> ellenorzo = new List<string>();
            for (int i = 0; i < 61; i++)
            {
                string line = sr.ReadLine();
                tehetoTetek[i, 0] = line.Split('\t')[0];
                ellenorzo.Add(line.Split('\t')[0]);
                tehetoTetek[i, 1] = line.Split('\t')[1];
                tehetoTetek[i, 2] = line.Split('\t')[2];

            }

            Console.WriteLine("Tehető tétek: \nSzám( 0, 1, 2, 3, 4 ... 36");

            sr.Close();
            int zseton = 1000;
            List<mezo> tetek = new List<mezo>();
            //játék

            bool jatszik = true;
            while (jatszik && zseton > 0)
            {
                bool tesz = true;
                tetek.Clear();
                while (tesz && zseton > 0)
                {
                    Console.Write("Kérem a következő tétet (Exit, Pass, P-20): ");
                    string nextTet = Console.ReadLine();
                    if (nextTet.ToLower() == "exit") { jatszik = false; break; }
                    if (nextTet.ToLower() == "pass") { break; }
                    if (!ellenorzo.Contains(nextTet.Split('-')[0])) continue;
                    try
                    {
                        if (int.Parse(nextTet.Split('-')[1]) < 1) continue;
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                    if (zseton - int.Parse(nextTet.Split('-')[1]) < 0) continue;

                    int tetSorszam = ellenorzo.IndexOf(nextTet.Split('-')[0]);

                    //tetek.Add(new mezo(nextTet.Split('-')[1] + " " + nextTet.Split('-')[0]));
                    tetek.Add(new mezo(nextTet.Split('-')[1] + " " + tetSorszam.ToString()));
                    zseton -= int.Parse(nextTet.Split('-')[1]);

                   

                    Console.WriteLine($"\nÖn a(z) {tehetoTetek[tetSorszam, 1]} mezőre tett {int.Parse(nextTet.Split('-')[1])} egységet. (Maradt {zseton} egysége.)");


                }

                //Pörgetés
                if (tetek.Count != 0) { 

                List<int> nyert = new List<int>();
                int a = rnd.Next(0, 37);
                Console.WriteLine($"A pörgetett szám: {kerek[a].ertek}, színe: {kerek[a].szin}");
                nyert.Add(kerek[a].ertek);

                int[] ujSZr = { 51, 49, 50 };


                if (kerek[a].ertek != 0)
                {
                    //páros-páratlan
                    if (kerek[a].ertek % 2 == 0) nyert.Add(59);
                    else nyert.Add(60);
                    //szín
                    if (kerek[a].szin == "R") nyert.Add(55);
                    else nyert.Add(56);
                    //harmad
                    if (kerek[a].ertek < 13) nyert.Add(52);
                    else if (kerek[a].ertek < 25) nyert.Add(53);
                    else nyert.Add(54);
                    //felező
                    if (kerek[a].ertek < 19) nyert.Add(57);
                    else nyert.Add(58);

                    int b = (kerek[a].ertek + 2) / 3;
                    nyert.Add(b + 36);

                    b = (kerek[a].ertek) % 3;
                    nyert.Add(ujSZr[b]);
                }
                Console.Write("Nyert: ");
                foreach (var item in nyert) { Console.WriteLine(tehetoTetek[item, 1]); }

                foreach (var item in tetek)
                {
                    foreach (var item1 in nyert)
                    {
                        {
                            if (int.Parse(item.szin) == item1)
                            {
                                zseton += int.Parse(tehetoTetek[item1, 2]) * item.ertek;
                                break;
                            }
                        }
                    }

                    }
                    Console.WriteLine($"Önnek {zseton} Pénze van");
                }
               
               
            }
            Console.WriteLine("viszlát");


            Console.ReadKey();
        }
    }
}
