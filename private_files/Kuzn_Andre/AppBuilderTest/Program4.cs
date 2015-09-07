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
				
                // Settings Example 1
				Dictionary<int, mapSettings> settings = new Dictionary<int, mapSettings>(){
					{0, new mapSettings{ size = 20, exist = "O", not_exist = " ", wallsWidth = 1}},
					{1, new mapSettings{ size = 30, exist = "o", not_exist = " ", wallsWidth = 3}},
					{2, new mapSettings{ size = 25, exist = "0", not_exist = " ", wallsWidth = 2}}
				}; 
                // Settings Example 2

				// settings switch
				byte n = 2;
				
                // settings
                int size = settings[n].size;
                string exist = settings[n].exist;
                string not_exist = settings[n].not_exist;
                int wallsWidth = settings[n].wallsWidth;

                // Random method
                Random rnd = new Random();

				// populating the array with values
                string[,] ar = new string[size, size];
                for (int y = 0; y < size; y++)
                {
                    int rnd1 = rnd.Next(0, size);
                    int rnd2 = rnd.Next(rnd1, size);

                    for (int x = 0; x < size; x++)
                    {
                        DetermineIfWallOrNot(wallsWidth, x, y, size, rnd1, rnd2, exist, not_exist, ar);
                    }
                }

                // pipe and display logic
                string btw = "";
                for (int i = 0; i < ar.GetLength(0); i++)
                {
                    btw += i != 0 ? "   |" : "|";
                }

                for (byte s = 0; s < ar.GetLength(0); s++)
                {
                    for (byte i = 0; i < ar.GetLength(1); i++)
                    {
                        if (i != 0)
                        { Console.Write(" - {0}", ar[s, i]); }
                        else
                        { Console.Write(ar[s, i]); }
                    }
                    if (s != (ar.GetLength(0) - 1))
                    {
                        Console.WriteLine();
                        Console.WriteLine(btw);
                    }
                }

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

        private void DetermineIfWallOrNot(
			int wallsWidth, 
			int x, int y, 
			int size, 
			int rnd1, int rnd2, 
			string exist, 
			string not_exist, 
			string[,] ar2)
        {
            if (WallSpace(x, y, wallsWidth, size))
            {
				
                if (Contains(rnd1, rnd2, x))
                {
                    ar2[y, x] = not_exist;
                }
                else
                {
                    ar2[y, x] = exist;
                }
            }
            else
            {
                ar2[y, x] = exist;
            }
        }
		
		private bool WallSpace(int x, int y, int wallsWidth, int size)
		{
			return Contains(wallsWidth, (size - (wallsWidth * 2)), x) && 
				Contains(wallsWidth, (size - (wallsWidth * 2)), y);
		}

        private bool Contains(int start, int stop, int x)
        {
            return Enumerable.Range(start, stop).Contains(x);
        }

        private void Output(params string[] str)
        {
            refer.Output(str);
        }
    }
}