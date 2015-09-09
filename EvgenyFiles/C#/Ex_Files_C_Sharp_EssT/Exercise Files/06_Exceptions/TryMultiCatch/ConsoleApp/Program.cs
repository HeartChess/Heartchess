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

            try
            {
                string[] fruit = { "Apples", null, "Grapes" };
                for (int i = 0; i < fruit.Length; i++)
                {
                    if (fruit[i]== null)
                    {
                        throw (new ArgumentNullException());
                    }
                    Console.WriteLine(fruit[i]);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);                
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                 Console.WriteLine("Finnaly end is reached!");
            }
        }
    }
}
