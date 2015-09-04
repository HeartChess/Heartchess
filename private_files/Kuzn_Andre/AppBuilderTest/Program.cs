using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Programss
{
    using LibraryOfMethods;
    public partial class Program
    {
        LibraryOfMethods.CalcMethods transf = new LibraryOfMethods.CalcMethods();
        
        public const long MeaningOfLife = 4532434245453;
        LibraryOfMethods.CalcMethods staff = new LibraryOfMethods.CalcMethods();
        public void Main()
        {
            
            var transf = new CalcMethods();
            Console.WriteLine(transf.ToString());
            
            Output("//////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            int staff = 4;
            int staff2 = new Int32(); // explisit declaration
            var staff3 = 45f + 100;

            char c1 = 's'; // single col '' for the char double "" are for the sting
            char c2 = '\u0062'; // char can be the unicode or just one letter
            char symbol = '%';

            char[] ArrayStaff = { 's', 'u', 'p' }; // type of array, [], name, {}, and values
            string firstSting = new System.String(ArrayStaff, 1, 2);
            // hover into  () and press up/down to see options
            string stafffs = "Here we go brozz";
            string stafffs2 = stafffs.Substring(4);

            //sting builder System.Text
            StringBuilder ones = new StringBuilder().Append("staff").Append(" is awesome").Append("!!");
            string str = "345";
            string str2 = "45.3254";
            int ParsedInt = Int32.Parse(str);
            int ParsedDoubled = ParsedInt * 3;

            double parsedDouble = Double.Parse(str2);
            Output(parsedDouble.ToString());

            string staffsf = "45653.34xxx";
            double hereWeGoDouble; // TryParse is sweet one
            if (Double.TryParse(staffsf, out hereWeGoDouble)) // out is seems to assign the output value if it is successfull 
            {
                Output("It worked : {0}", hereWeGoDouble.ToString());
            }
            else
            {
                char[] opt = { 'n', 'o', 'p', 'e' };
                Output(new System.String(opt));
            }
            
            sbyte sre = 56;
            Output("staff" + (sre++).ToString()); // interastingly give me result --> do math (56)
            Output("staff" + (++sre).ToString()); // do math --> give me result (58)
            Output("(56)Here is value after " + sre.ToString());

            Output(ones.ToString());
            
            bool stafss = !false; // just to use  
            int? nope = null; // ? --> does the job...
            
            int news = nope ?? 45; // very notty with data types
            
            Output("Equals is " + ("staff".Equals("staff")).ToString());

            Console.WriteLine("{0}, {1}", Char.IsLetterOrDigit(symbol).ToString(), Char.IsSymbol(symbol).ToString());
            Console.WriteLine("{0}, {1}", firstSting.ToUpper(), stafffs2);
            Console.WriteLine(staff2);
            Console.WriteLine(staff3.GetType().ToString());
            Console.WriteLine(staff3);
            Output("Hello World", firstSting.ToLower(), stafffs2);
            Output("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//////////////////////////");
        }
        // classes are passed by reference
        // primitives are passed by value
        // int string char bool
        // .NET -->  System.Boolen System.Char  
        // uint --> just positives
        // sbyte is int of uint of byte
        // floats to know are float, double, decimal(better for currency)
        // string is immutable, can not change after creation
        // can pass the obj with object

        public void Output(string v, string s = " ", string m = " ")
        {
            Console.WriteLine("{0}, {1}, {2}", v, s, m);
        }
        private void OutputN(int v)
        {
            Console.WriteLine(v.ToString());
        }
    }
}

namespace LibraryOfMethods
{
    public class CalcMethods
    {
        public static void callVal()
        {
            // if method is static then can do staff.Output
            // if not then need to init it first new Classname()
            
            Console.Write("Yeap///////////////////////");
        }
    }
}
