using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fajlKezeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] fileAdat = File.ReadAllLines("ajto.txt", Encoding.UTF8).Skip(1).ToArray();


            string filename = "ajto.txt";
            //adatok beolvasása str tömbbe
            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            string[] adatok = new string[int.Parse(sr.ReadLine())];
            for (int i = 0; i < adatok.Length; i++)
            {
                adatok[i] = sr.ReadLine();
            }
            sr.Close();

            //adatok beolvasása int mátrixba
            StreamReader sr2 = new StreamReader(filename, Encoding.UTF8);
            int[,] adatokMatrix = new int[int.Parse(sr2.ReadLine()), 4];
            for (int i = 0; i < adatok.Length; i++)
            {
                string tmp = sr2.ReadLine();
                string[]szet = tmp.Split(' ');
                adatokMatrix[i,0] = int.Parse(szet[0]);
                adatokMatrix[i,1] = int.Parse(szet[1]);
                adatokMatrix[i,2] = int.Parse(szet[2]);
                if (szet[3] == "be")
                {
                    adatokMatrix[i,3] = 1;
                }
                else adatokMatrix[i,3]= 0;

            }
            sr2.Close();

            //adatok beolvasása OOP
            StreamReader sr3 = new StreamReader(filename, Encoding.UTF8);
            adat[] adatokOOP = new adat[int.Parse(sr3.ReadLine())];
            for (int i = 0; i < adatokOOP.Length; i++)
            {
                string tmp = sr3.ReadLine();
                adatokOOP[i] = new adat(tmp);
            }
            sr3.Close();



            string[]a = File.ReadAllLines("ajto2.txt", Encoding.UTF8);
            StreamReader sr4 = new StreamReader("ajto2.txt", Encoding.UTF8);
            adat[] adatokOOP2 = new adat[a.Length];
            for (int i = 0; i < adatokOOP2.Length; i++)
            {
                string tmp = sr4.ReadLine();
                adat tm2 = new adat(tmp);
                if (tm2.direction != "error")
                {
                    adatokOOP2[i] = tm2;
                }
                adatokOOP2[i] = new adat(tmp);
               
            }
            sr4.Close();

            string[] b = File.ReadAllLines("ajtoHiba.txt", Encoding.UTF8);
            StreamReader sr5 = new StreamReader("ajtoHiba.txt", Encoding.UTF8);
            adat[] adatokOOP3 = new adat[a.Length];
            for (int i = 0; i < adatokOOP3.Length; i++)
            {
                string tmp = sr5.ReadLine();
                adat tm3 = new adat(tmp);
                if (tm3.direction != "error")
                {
                    adatokOOP3[i] = tm3;
                }
                adatokOOP2[i] = new adat(tmp);

            }
            sr4.Close();







            Console.ReadKey();
        }
    } 

    class adat
    {
        
        public int h;
        public int m;
        public int cn;
        public string direction;
        public adat(string sor)
        {
            try
            {
                string[] szet = sor.Split(' ');
                this.h = int.Parse(szet[0]);
                this.m = int.Parse(szet[1]);
                this.cn = int.Parse(szet[2]);
                this.direction = szet[3];
            }
            catch (Exception)
            {

                this.direction = "error";
            }
            

        }
    }
}
