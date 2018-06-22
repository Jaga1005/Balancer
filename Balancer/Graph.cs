using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balancer
{
    /// <summary>
    /// struct for user
    /// Name - name of paying one, balance - full amount, creditors - list of creditors 
    /// </summary>
    public struct Debt
    {
        public readonly String Name;
        public readonly decimal Balance;
        public readonly List<String> Creditors;

        public Debt(string name, decimal balance, string creditor)
        {
            this.Name = name;
            this.Balance = balance;
            Creditors = new List<string>() {creditor};
        }

        public Debt(string name, decimal balance, string creditor, List<string> creditors) : this(name, balance, creditor)
        {
            Creditors = creditors;
        }
    }

    public class Graph
    {
        private Dictionary<string, decimal> _creditors;
        public Graph()
        {
            _creditors = new Dictionary<string, decimal>();
        }

        public Graph(List<Debt> transactions)
        {
            _creditors = new Dictionary<string, decimal>();
            transactions.ForEach(t =>
            {
                    t.Creditors.ForEach(c =>
                    {
                        decimal balance = t.Balance /(decimal) t.Creditors.Count;
                        AddTransaction(t.Name, balance, c);
                    });
                
            });
        }

        public void Add(String nick, String creditor, decimal balance)
        {
            AddTransaction(nick, balance, creditor);
        }

        public void Add(String nick, List<String> creditors, decimal balance)
        {
            decimal newBalance = balance / (decimal)creditors.Count;
            creditors.ForEach(c=> AddTransaction(nick, newBalance, c));
        }

        private void AddTransaction(String name, decimal balance, String creditor)
        {
            if (_creditors.ContainsKey(name))
            {
                _creditors[name] += balance;
            }
            else
            {
                _creditors.Add(name, balance);
            }
            if (_creditors.ContainsKey(creditor))
            {
                _creditors[creditor] -= balance;
            }
            else
            {
                _creditors.Add(creditor, -balance);
            }
        }

        public Dictionary<String, List<Tuple<String, decimal>>> GetSummary()
        {
           List<Node> plus = new List<Node>();
           List<Node> minus = new List<Node>();

            foreach (var key in _creditors.Keys)
            {
                if (_creditors[key] < 0)
                {
                    minus.Add(new Node(key, _creditors[key]));
                }
                else if (_creditors[key] > 0)
                {
                    plus.Add(new Node(key, _creditors[key]));
                }
            }
            return RecalculateDebs(plus, minus);
        }

        private static Dictionary<String, List<Tuple<String, decimal>>> RecalculateDebs(List<Node> plus, List<Node> minus)
        {
            List<Debt> results = new List<Debt>();
            while (plus.Count > 0 && minus.Count > 0)
            {
                plus = plus.OrderByDescending(p => p.Balance).ToList();
                minus = minus.OrderBy(m => m.Balance).ToList();
                Node plusNode = plus.First();
                Node minusNode = minus.First();
                if (plusNode.Balance > -minusNode.Balance)
                {
                    plusNode.Balance += minusNode.Balance;
                    results.Add(new Debt(minusNode.Name, -minusNode.Balance, plusNode.Name));
                    minusNode.Balance = 0;
                    minus.RemoveAt(0);
                    if (plusNode.Balance == 0)
                    {
                        plus.RemoveAt(0);
                    }
                }
                else
                {
                    results.Add(new Debt(minusNode.Name, plusNode.Balance, plusNode.Name));
                    minusNode.Balance += plusNode.Balance;
                    plusNode.Balance = 0;
                    plus.RemoveAt(0);
                    if (minusNode.Balance == 0)
                    {
                        minus.RemoveAt(0);
                    }
                }
            }
            return ConvertListToDictionary(results);
        }

        private static Dictionary<String, List<Tuple<String, decimal>>> ConvertListToDictionary(List<Debt> debts)
        {
            Dictionary<String, List<Tuple<String, decimal>>> results = new Dictionary<string, List<Tuple<string, decimal>>>();
            debts.ForEach(debt =>
            {
                List<Tuple<String, decimal>> transactions = new List<Tuple<string, decimal>>();
                var creditors = debts.FindAll(d => d.Name == debt.Name);

                creditors.ForEach(creditor =>
                {
                    transactions.Add(new Tuple<string, decimal>(creditor.Creditors[0], creditor.Balance));
                });
                if (!results.ContainsKey(debt.Name))
                {
                    results.Add(debt.Name, transactions);
                }
            });

            return results;
        }

        public void ClearAll()
        {
            _creditors.Clear();
        }
    }
}
