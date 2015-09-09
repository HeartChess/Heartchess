using System;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get values to calculate
            double double1 = calcutility.GetValue("Enter value 1: ");
            double double2 = calcutility.GetValue("Enter value 2: ");

            //Declare variable to hold result
            double result = 0;

            //Do math operation
            while (true)
            {
                Console.Write("(A)dd (S)ubtract (M)ultiply (D)ivide : ");
                
                ConsoleKeyInfo info = Console.ReadKey();
                string operation = info.Key.ToString();

                switch (operation.ToUpper()) {
                    case "A" :
                        result = calcutility.Add(double1, double2);
                        break;
                    case "S":
                        result = calcutility.Subtract(double1, double2);
                        break;
                    case "M":
                        result = calcutility.Multiply(double1, double2);
                        break;
                    case "D":
                        result = calcutility.Divide(double1, double2);
                        break;
                    default :
                        Console.WriteLine("Choose from supported operations");
                        continue;
                }
                Console.WriteLine("\nResult: " + result);
                Console.Read();
                break;
            }

        }



    }
}
