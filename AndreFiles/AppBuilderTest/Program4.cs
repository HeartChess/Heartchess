using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp;


namespace Program4
{
	// settings constractor
	class mapSettings
	{
		public int size { get; set; }
		public string exist { get; set; }
		public string not_exist { get; set; }
		public int wallsWidth { get; set; }
	}
	
    class FirstClass
    {
        Program refer = new Program();
        
        public void TestingTuts()
        {
            try
            {
                int[] weights = new int[] { 23, 434, 445343, 4534, 123 };
                //  weights[weights.Count()] = 17; // this will apparantly throw exeption too
                //string[] fruit = {"apple", "orange", "watermellon", null};
                string[] fruit = new string[4];
                fruit[0] = "apple";
                fruit[1] = "orange";
                fruit[2] = "watermellon";
                fruit[3] = null;
                //  fruit[4] = "Will throw out of range";
                foreach (string f in fruit)
                {
                    if (f != null)
                    {
                        Output(f);
                    }
                    else
                    {
                        throw (new NullReferenceException());
                    }
                }

            }
            catch (ArgumentNullException err)
            {
                Output(String.Format("{0} : The null problem", err.Message));
            }
            catch (IndexOutOfRangeException err)
            {
                Output(String.Format("{0} : The index problem", err.Message));
            }
            catch (Exception err)
            {
                Output(String.Format("There is a problem: {0}", err.Message));
            }
            finally
            {
                Output("Final block is reached");
            }

        }

        private void Output(params string[] str)
        {
            refer.Output(str);
        }
    }
}