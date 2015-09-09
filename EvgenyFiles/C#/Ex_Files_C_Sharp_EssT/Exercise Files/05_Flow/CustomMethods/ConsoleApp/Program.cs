using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {

        const int Value1 = 12;
        const int Value2 = 24;

        static void Main(string[] args)
        {
            int myVar = MyMethod(Value1, Value2);
            Console.WriteLine(myVar);

            int myresult = 0 ;
            MyMethodparam(myresult, myresult, out myresult);
            Console.WriteLine(myresult);

        }

        static int MyMethod(int value1, int value2)
        {
            return value1 + value2;

        }
        static void MyMethodparam(int value1, int value2, out int val)
        {
            val = value1 + value2;
        }
        
    }
}
