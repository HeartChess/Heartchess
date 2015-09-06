using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using CalcNameSpace;

// shft + cmd + p   --> dnx run
//to run an app on command line

// FOR SOME FUCK REASONS Program1.cs is the main file...

namespace ConsoleApp
{
    /**
    *   Main Class to run all programs through reference to main method
    **/
    public class Program
    {
        public static void Main(string[] arg)
        {
            System.Console.WriteLine("staff works");
            //init from Program2
            Calculator1 instanceOf = new Calculator1();
                        
            //Draw mountains function
            //  ProgramToDrawMountains.LoopingStaff(120);

            
            //Calculator
            instanceOf.MainRun();
        }

        public void Output(string stf)
        {
            Console.WriteLine("\n {0} \n", stf);
        }
    }
    
    
    
    
    
    /**
    *   Just tests of different concepts
    **/
    class testStaff
    {
        public void MainRun()
        {
            int test = (byte)daysOfweek.Saturday;

            // nice sweet prog to find input char in the array
            string getS = Console.ReadLine();
            bool found = false;
            string[] staff = { "*", "/", "+", "-", "%" };
            for (int i = 0; i < staff.Count(); i++)
            {
                if (staff[i].Equals(getS)) // StringComparison.OrdinalIgnoreCase for words
                {
                    found = true;
                    Output("Your chosen operator is: ");
                    Output(staff[i]);
                }
            }

            if (!found)
            {
                Output("Nope didnt find your operator");
            }

            Output(test.ToString());

            DateTime dt = new DateTime();
            string c = DateTime.Now.ToString("MMMM d, yyyy, hh:mm:ss tt"); // sweet date to string conversion
            Output(String.Format("{0}, \n, {1}", dt, c));

            DangerOfCrossing dangers = new DangerOfCrossing(TrafficLights.Red);

            Output(String.Format("The current Danger is {0}", (TrafficLights.Red.ToString())));
            // T? is shorthand for Nullable<T> 
            int? nullable = null;
            int eval = nullable ?? 34; // eval == 34
            Output(new StringBuilder().Append(eval).ToString());
        }
        
        private void Output(string str)
        {
            Program s = new Program();
            s.Output(str);
        } 
    }
    
    
    
    
    
   
    /**
    *   Prog to draw mountains out of chars
    *   LoopingStaff  ---> main function
    **/
    class ProgramToDrawMountains 
    {
        private static void OutputLoop(string stf, int times = 1, int a = 1)
        {
            string result = "";
            if (a > 1)
            {
                for (int i = 0; i < a; i++)
                {
                    stf += stf;
                }
            }
            
            for (int i = 0; i < times; i++)
            {
                result += stf;
            }

            Console.WriteLine("{0}", result);
        }
        
        public static void LoopingStaff(
            byte length, 
            byte mountainWidth = 30, 
            byte mountainDimentions = 1, 
            string left_side = "/", 
            string right_side = "\\")
        {
            // mountain width
            byte[] arr = new byte[mountainWidth];
            for(int i = 0; i < mountainWidth; i ++){
                arr[i] = (byte)i;
            };
           
            byte counter = 0;
            bool change = true;
            
            do {
                counter++;
                // switch from one mountain side to another 
                string sideSwitcher = change ? left_side : right_side;
                arr = arr.Reverse().ToArray();
                OutKeywordTry(change, out change); // quite nice way to modify vars on a run without return
                //  change = !change;
                
                // one loop => one mountain
                for (int i = 0; i < arr.Count(); i++)
                {
                    OutputLoop(sideSwitcher, arr[i], (byte)mountainDimentions);
                }               
            } while (counter <= length);
                        
        }
        
        private static void OutKeywordTry (bool b, out bool result) 
        {
            result = !b;
        }
    }
    
    
    
    
    
    
    
    // enum declaration, the point is to make the declaration more expressive
    public enum TrafficLights
    {
        Red,
        Amber,
        Green,
        RedAmber
    }

    public enum daysOfweek : byte
    {
        Monday = 1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday = 0
    }

    public class DangerOfCrossing
    {
        private TrafficLights _danger;
        public DangerOfCrossing(TrafficLights light)
        {
            _danger = light;
        }
    }
 
}









