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
            string input = Console.ReadLine();

            if (input.Equals("qwerty", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You have choose" + input);
            }
            else if (input.Equals("qaz", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You have choose" + input);
            }
            else
            {
                Console.WriteLine("You have choose otherwise");
            }
        }
    }
}
