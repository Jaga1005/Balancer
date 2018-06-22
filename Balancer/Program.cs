using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(new List<Debt>()
            {
                new Debt("G", (decimal) 25, "A"),
                new Debt("I", (decimal) 25, "A"),
                new Debt("I", (decimal) 5, "B"),
                new Debt("E", (decimal) 18, "B"),
                new Debt("F", (decimal) 2, "B"),
                new Debt("H", (decimal) 10, "C"),
                new Debt("H", (decimal) 5, "D"),
            });

            var list = g.GetSummary();
          //  list.ForEach(l=> Console.WriteLine(l));

            Console.WriteLine("\n\n");
            ReadDictionary(list);
        }

        public static void ReadDictionary(Dictionary<String, List<Tuple<String, decimal>>> res)
        {
            foreach (var key in res.Keys)
            {
                Console.WriteLine(key + ":");
                res[key].ForEach(r =>
                {
                    Console.WriteLine("\t" + r.Item1 + " " + r.Item2 + "zl");
                });
            }
        }
    }
}
