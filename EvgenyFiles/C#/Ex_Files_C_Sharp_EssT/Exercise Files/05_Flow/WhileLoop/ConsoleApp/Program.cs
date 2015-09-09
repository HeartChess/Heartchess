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
            int counter = 1;
            int max = 10;
            while (counter<=max)
            {
                Console.WriteLine(counter + "/" + max);
                counter++;
            }

            string welcome = "Hello World";
            counter=0;
            while (counter != welcome.Length)
            {
                Console.WriteLine(welcome[counter]);
                counter++;
            }
            counter = 0;
            do
            {
                Console.WriteLine(welcome[counter]);
                counter++;
            } while (counter != welcome.Length);
        }
    }
}
