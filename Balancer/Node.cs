using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer
{
    public class Node
    {
        public readonly String Name;
        public decimal Balance;

        public Node(String name, decimal balance = 0)
        {
            this.Name = name;
            this.Balance = balance;     
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   Name == node.Name &&
                   Balance == node.Balance;
        }

        public override int GetHashCode()
        {
            var hashCode = -1374444185;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Balance.GetHashCode();
            hashCode = hashCode * -1521134295;
            return hashCode;
        }
    }
}
