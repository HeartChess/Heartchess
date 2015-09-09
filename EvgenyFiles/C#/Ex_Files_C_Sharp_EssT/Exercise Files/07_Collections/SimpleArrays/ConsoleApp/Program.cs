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

            string[] fruit = { "Apples", "Oranges", "Grapes" };

            string[] names = new string[3];
            names[0] = "jones";
            names[1] = "zalupa";
            names[2] = "squrt";

            int[] weight = { 1, 43, 54, 12, 4, 2 };
            int sum = weight.Sum();
            double ave = weight.Average();
            Console.WriteLine(sum +"/"+ ave);

            useArrey(weight);

        }

        static void useArrey(int[] values)
        {
            Console.WriteLine("Values in method:");
            foreach (var item in values)
            {
                Console.WriteLine(item);
            }

        }


    }
}
