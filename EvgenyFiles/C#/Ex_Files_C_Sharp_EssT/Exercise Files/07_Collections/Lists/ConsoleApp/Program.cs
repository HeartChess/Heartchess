using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fruitList = new List<string>();

            fruitList.Add("Apple");
            fruitList.Add("orange");
            fruitList.Add("fi1g");

            foreach (var item in fruitList)
            {
                Console.WriteLine(item);
            }

            fruitList.Sort();

            foreach (var item in fruitList)
            {
                Console.WriteLine(item);
            }

            listpass(fruitList);
        }

        static void listpass(List<string> items)
        {
            string figReport = items.Contains("fig", StringComparer.OrdinalIgnoreCase) ?
                "yes there is some figs" :
                "No figs there";
            Console.WriteLine(figReport);

        }
    }
}
