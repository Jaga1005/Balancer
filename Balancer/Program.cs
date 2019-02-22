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

            Console.WriteLine("Before");
            PrintCreditorsBefore(g);

            Console.WriteLine();
            Console.WriteLine("After");
            ReadDictionary(g.GetSummary());
        }

        private static void PrintCreditorsBefore(Graph g)
        {
            var creditors = g.Creditors.OrderBy(c => c.Key);
            foreach (var creditor in creditors)
            {
                Console.WriteLine(creditor.Key + " " + creditor.Value + "");
            }
        }

        public static void ReadDictionary(Dictionary<String, List<Tuple<String, decimal>>> res)
        {
            foreach (var key in res.Keys)
            {
                Console.WriteLine(key +  " -" + res[key].Sum(r => r.Item2) + ":");
                res[key].ForEach(r =>
                {
                    Console.WriteLine("\t" + r.Item1 + " " + r.Item2 + "");
                });
            }
        }
    }
}
