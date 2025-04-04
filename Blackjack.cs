using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BlackJack
{
    internal class lap
    {
        public string szín { get;private set; }
        public string színbólum { get;private set; }
        public int érték { get;private set; }

        public lap(string line)
        {
            szín = line.Split(';')[0];
            színbólum = line.Split(';')[1];
            érték = int.Parse(line.Split(';')[2]);

        }


        static void Main(string[] args)
        {
            string[] fileadat = File.ReadAllLines("\\..\\..\\..\\pakli.txt", Encoding.UTF8).Skip(1).ToArray();

            List<lap> pakli = new List<lap>();
            foreach (var item in fileadat) pakli.Add(new lap(item));
            Random oszto = new Random();

            List<lap> gep = new List<lap>();
            List<lap> ember = new List<lap>();

            int lapSorszam = oszto.Next(pakli.Count);
            ember.Add(pakli[lapSorszam]);
            pakli.Remove(pakli[lapSorszam]);

            lapSorszam = oszto.Next(pakli.Count);
            ember.Add(pakli[lapSorszam]);
            pakli.Remove(pakli[lapSorszam]);


            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    lapSorszam = oszto.Next(pakli.Count);
                    ember.Add(pakli[lapSorszam]);
                    pakli.Remove(pakli[lapSorszam]);

                    lapSorszam = oszto.Next(pakli.Count);
                    ember.Add(pakli[lapSorszam]);
                    pakli.Remove(pakli[lapSorszam]);




                }
                Console.WriteLine($"Gép pontja: {} - Ember pontja: {}");
            }




            Console.ReadKey();
        }
    }
}
