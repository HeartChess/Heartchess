using System;
using System.Text;

// shft + cmd + p   --> dnx run
//to run an app on command line

namespace YellowBookTut
{
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
        Monday=1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday = 0
    }

    public class DangerOfCrossing
    {
        private TrafficLights _danger;
        public DangerOfCrossing(TrafficLights light)
        {
            _danger = light;
        }
    }

    public class Program
    {
        public static void Main()
        {
            int test = (byte)daysOfweek.Saturday;
                        
            string col = "/";
            for(int y = 0; y <= 10; y ++) {
                col += "/";
                Output(col);
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

        public static void Output(string stf)
        {
            Console.WriteLine("\n {0} \n", stf);
        }

    }
}