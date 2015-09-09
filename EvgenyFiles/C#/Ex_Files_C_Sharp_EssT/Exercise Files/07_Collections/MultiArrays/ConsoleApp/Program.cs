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
            string[,] sections = new string[3, 3];

            sections[0, 0] = "topleft"; // row, colomn
            sections[0, 1] = "topcenter";
            sections[0, 2] = "topright";
            sections[1, 0] = "middle left";
            sections[1, 1] = "middle center";
            sections[1, 2] = "middle right";
            sections[2, 0] = "Bottom left";
            sections[2, 1] = "Bottom center";
            sections[2, 2] = "Bottom right";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine(sections[i,j] + "\t");
                    if ((j+1)%3==0)
                    {
                        Console.WriteLine("");
                    }
                }
            }
        }

    }
}
