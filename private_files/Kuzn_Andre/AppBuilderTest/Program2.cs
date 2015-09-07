using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ConsoleApp;

namespace Program2
{
    public class Calculator1
    {

        Program part = new Program();

        public static string[] warning_message = new string[] {
                "Insert the expression! (ex. 45 * 5)",
                "This is the format: number-operator-number",
                "Too big number!",
                "Too small number!",
                "No zeros plz",
                "Theres literaly no input or its wrong...?",
                "Damn man, its just Y or N"
                };

        public void MainRun()
        {
            string line;
            bool check = true;

            var firstN = (decimal)0;
            var secondN = (decimal)0;

            do
            {
                Output(warning_message[0]);
                line = Console.ReadLine();

                string[] arr = line.Split();

                if (arr.Count() != 3)
                {
                    Output(warning_message[1]);
                    continue;
                }

                firstN = ParseInputV(arr[0]);
                secondN = ParseInputV(arr[2]);
                if (firstN != 0 && secondN != 0)
                {
                    ExpressionResult(firstN, secondN, arr[1]);
                }

                // prompt to continue
                bool contin = true;
                do
                {
                    Console.Write("Want to continue? (y/n) >>  ");
                    string cont = Console.ReadLine();
                    if (cont.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        contin = false;
                        check = true;
                    }
                    if (cont.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        contin = false;
                        check = false;
                    }
                    else
                    {
                        Output(warning_message[6]);
                    }
                } while (contin);


            } while (check);
        }

        private void Output(string str)
        {
            part.Output(str);
        }

        private decimal ParseInputV(string s)
        {
            decimal result;
            return Decimal.TryParse(s, out result) ? Decimal.Parse(s) : 0;
        }

        private void LuxuryOutput(decimal e)
        {
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("        {0}", e.ToString());
            Console.WriteLine("===================");
            Console.WriteLine("===================");
        }

        private decimal inPowerExp(decimal f, decimal s, bool p)
        {
            decimal initF = f;
            if (p)
            {
                for (decimal i = 0; i < s - 1; i++)
                {
                    f *= initF;
                }
            }
            else
            {
                f = Math.Floor(f / s);
            }

            return f;
        }

        private void ExpressionResult(decimal f, decimal s, string o)
        {
            switch (o)
            {
                case "+":
                    LuxuryOutput((f + s));
                    break;
                case "-":
                    LuxuryOutput((f - s));
                    break;
                case "*":
                    LuxuryOutput((f * s));
                    break;
                case "/":
                    LuxuryOutput((f / s));
                    break;
                case "%":
                    LuxuryOutput((f % s));
                    break;
                case "**":
                    LuxuryOutput(inPowerExp(f, s, true));
                    break;
                case "//":
                    LuxuryOutput(inPowerExp(f, s, false));
                    break;
            }
        }
    }
}