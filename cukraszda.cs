using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace cukraszda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serverKapscolat = new MySqlConnectionStringBuilder { Server = "127.0.0.1", Database = "cukraszda", UserID = "root", Password = "" };
            MySqlConnection kapcsolat = new MySqlConnection(serverKapscolat.ConnectionString);
            kapcsolat.Open();
            var lekerdezes = kapcsolat.CreateCommand();
            Console.WriteLine("Hiányzó kalória érték: ");
            lekerdezes.CommandText = "SELECT COUNT(*) AS \"Hiányzó kalória érték\"\r\nfrom termek\r\nWHERE termek.kaloria IS NULL;";
            var olvaso = lekerdezes.ExecuteReader();
            while (olvaso.Read())
            {
                Console.WriteLine($"{olvaso.GetInt64(0)} ");
            }
            olvaso.Close();
            Console.WriteLine();
            lekerdezes.CommandText = "SELECT DISTINCT termek.nev, kiszereles.mennyiseg from termek inner join kiszereles on termek.kiszerelesId = kiszereles.id WHERE kiszereles.mennyiseg LIKE \" % g\";";
            olvaso = lekerdezes.ExecuteReader();
            while(olvaso.Read())
            {
                Console.WriteLine($"{olvaso.GetString(0)}");
            }
            olvaso.Close();
            Console.WriteLine();
            lekerdezes.CommandText = "SELECT allergen.nev, count(*)\r\nFROM termek inner JOIN  allergeninfo ON termek.id = allergeninfo.termekId inner JOIN allergen ON allergeninfo.allergenId = allergen.id\r\nGROUP BY allergen.nev desc\r\norder by count(*) DESC limit 3;";
            olvaso = lekerdezes.ExecuteReader();
            while(olvaso.Read())
            {
                Console.WriteLine($"{olvaso.GetString(0)} {olvaso.GetInt64(1)}");
            }
            olvaso.Close();
            Console.WriteLine();
            lekerdezes.CommandText = "SELECT termek.nev, termek.ar FROM termek WHERE termek.laktozmentes = 1 and termek.tejmentes = 1 AND termek.tojasmentes = 1 and termek.id not in (select allergeninfo.termekId from allergeninfo) ";
            olvaso = lekerdezes.ExecuteReader();
            while (olvaso.Read())
            {
                Console.WriteLine($"{olvaso.GetString(0)} {olvaso.GetInt32(1)}");
            }

            olvaso.Close();
            Console.WriteLine();
            lekerdezes.CommandText = "SELECT termek.nev, (termek.ar-100)*12\r\nfrom termek\r\nWHERE termek.nev like \"Paleo%\";";
            olvaso = lekerdezes.ExecuteReader();
            while (olvaso.Read())
            {
                Console.WriteLine($"{olvaso.GetString(0)} torta fizetendő ár: {olvaso.GetInt64(1)}");
            }

            Console.ReadKey();
        }
    }
}
