using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp;

namespace Program5
{
    // settings constractor
    class mapSettings
    {
        public int size { get; set; }
        public string exist { get; set; }
        public string not_exist { get; set; }
        public int wallsWidth { get; set; }
        public bool addRowSeparator { get; set; }
    }
    class mapGenerator
    {
        Program refer = new Program();
        public void generateMap()
        {
            try
            {
                char[] cool_chars = { '\u25A0' };
                // Settings Example 1
                Dictionary<int, mapSettings> settings = new Dictionary<int, mapSettings>(){
                    {0, new mapSettings{ size = 20, exist = "0", not_exist = " ", wallsWidth = 1, addRowSeparator = true}},
                    {1, new mapSettings{ size = 30, exist = "o", not_exist = " ", wallsWidth = 3, addRowSeparator = true}},
                    {2, new mapSettings{ size = 25, exist = cool_chars[0].ToString(), not_exist = " ", wallsWidth = 2, addRowSeparator = true}},
                    {3, new mapSettings{ size = 50, exist = cool_chars[0].ToString(), not_exist = " ", wallsWidth = 2, addRowSeparator = false}}
                };
                // Settings Example 2

                // settings switch
                Console.Write("What settings want to use (1 - 4) >>  ");
                byte n = byte.Parse(Console.ReadLine());
                n--;

                // settings
                int size = settings[n].size;
                string exist = settings[n].exist;
                string not_exist = settings[n].not_exist;
                int wallsWidth = settings[n].wallsWidth;
                bool addRowSeparator = settings[n].addRowSeparator;
                string rowSeparator = "-";

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

                GetRidOfThinWalls(wallsWidth, size, exist, not_exist, ref ar);

                // main loop to generate the grid
                for (byte s = 0; s < ar.GetLength(0); s++)
                {
                    string lineseparator = "|";
                    for (byte i = 0; i < ar.GetLength(1); i++)
                    {
                        if (i != 0)
                        {
                            printPointSeparator(addRowSeparator, rowSeparator, ar[s, i], s, i, ar, not_exist, ref lineseparator);
                        }
                        else
                        { Console.Write(ar[s, i]); }
                    }

                    // if not the last line
                    if (s != (ar.GetLength(0) - 1))
                    {
                        Console.WriteLine();
                        if (addRowSeparator)
                        {
                            // pipe and display logic
                            Console.WriteLine(lineseparator);
                        }
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
                Output("");
                Console.Write("Wanna Try Again? (y/n)");
                string answer = Console.ReadLine();
                if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    generateMap();
                }
            }

        }

        private void GetRidOfThinWalls(
            int wallsWidth,
            int size,
            string exist,
            string not_exist,
            ref string[,] arr)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (WallSpace(x, y, wallsWidth, size))
                    {
                        if(arr[y+1, x] == not_exist && arr[y-1, x] == not_exist)
                        arr[y, x] = " ";

                    }
                }
            }
        }

        private void printPointSeparator(
            bool addRowSeparator,
            string rowSeparator,
            string cs,
            byte x,
            byte y,
            string[,] ar,
            string not_exist,
            ref string lineseparator)
        {
            string result = "";
            if (addRowSeparator)
            {

                result = String.Format(ar[x, y] == " " || ar[x, y - 1] == " " ? "   {0}" : " - {0}", ar[x, y]);
            }
            else
            {
                result = String.Format(" {0}", ar[x, y]);
            }

            Console.Write(result);

            if (x + 1 < ar.GetLength(0))
            {
                if (ar[x, y] == not_exist || ar[x + 1, y] == not_exist)
                {
                    lineseparator += "    ";
                }
                else
                {
                    lineseparator += "   |";
                }
            }
            else
            {
                lineseparator += "   |";
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