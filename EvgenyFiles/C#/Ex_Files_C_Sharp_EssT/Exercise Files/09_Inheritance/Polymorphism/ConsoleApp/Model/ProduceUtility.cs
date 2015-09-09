using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    class ProduceUtility
    {
        public static double GetItemWeight(Produce item)
        {
            return item.Quantity * item.Weight;
        }

        public static double GetTotalWeight(List<Produce> produce)
        {
            double total = 0;
            foreach (var item in produce)
            {
                total = total + item.Quantity * item.Weight;
            }
            return total;
        }
    }
}
