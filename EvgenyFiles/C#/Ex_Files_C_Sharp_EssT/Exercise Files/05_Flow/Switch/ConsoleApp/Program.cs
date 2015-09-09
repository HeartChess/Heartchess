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
            Console.Write("Enter an operation: + - * / ");
            string operation = Console.ReadLine();

            switch (operation)
            {

                case "+":
                    Console.WriteLine("You chose to add");
                    break;
                case "-":
                    Console.WriteLine("You chose to sub");
                    break;
                case "*":
                    Console.WriteLine("You chose to mulply");
                    break;
                case "/":
                    Console.WriteLine("You chose to devide");
                    break;

                default:
                    Console.WriteLine("Otherwise no such stuff");
                    break;
            }

        }
    }
}
