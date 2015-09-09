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
            var inventory = new Dictionary<string, double>();

            inventory.Add("Apple", 56);
            inventory.Add("Figs", 23);
            inventory.Add("Oranges", 12);

            var keys = inventory.Keys;

            Console.WriteLine("Number of items " + keys.Count);

            foreach (var key in keys)
            {
                Console.WriteLine(key + ": " + inventory[key]);
            }

            string[] keyArray = keys.ToArray();
            Array.Sort(keyArray);

            foreach (var key in keyArray)
            {
                Console.WriteLine(key + ": " + inventory[key]);
            }

            double value;
            if (inventory.TryGetValue("Figs", out value))
            {
                Console.WriteLine("Figs inventory: " + value);
            }
            else
            {
                Console.WriteLine("Number of figs is not avaluable");
            }
           
        }

    }
}
