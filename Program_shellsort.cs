using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shellsort
{
    internal class Program
    {
        static void ShellRendezés(int[] a)
        {
            int gap;
            int n;
            int j;
            int x;
            int y;
            int i;
            gap = 1;
            n = a.Length;
            while (gap * 2 <= n)
            {
                gap *= 2;
            }
            gap -= 1;
            do
            {
                i = 0;
                while ((i <= gap) && (i +gap < n))
                {
                    j = i + gap;
                    while (j < n)
                    {
                        x = a[j];
                        y = j - gap;
                        while ((x > -1) && (x < a[y]))
                        {
                            a[y + gap] = a[y];
                            y -= gap;
                        }
                        a[y + gap] = x;
                        j += gap;
                    }
                    
                }
                gap = gap / 2;
            } while (gap > 0);

        }


        static void Main(string[] args)
        {
            int[] t = new int[10];
            t[0] = 63;
            t[1] = 54;
            t[2] = 33;
            t[3] = 45;
            t[4] = 23;
            t[5] = 99;
            t[6] = 43;
            t[7] = 10;
            t[8] = 35;
            t[9] = 87;
            ShellRendezés(t);
            for (int i = 0; i < 9; i++) Console.WriteLine(t[i]);
            Console.ReadKey();
        }
    }
}
