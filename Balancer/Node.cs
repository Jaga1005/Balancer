using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer
{
    public class Node
    {
        private readonly String name;
        public decimal Balance;
    
        public string Name => name;
        public Node(String name)
        {
            this.name = name;
            this.Balance = 0;
       
        }

        public Node(String name, decimal balance)
        {
            this.name = name;
            this.Balance = balance;     
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   name == node.name &&
                   Balance == node.Balance;
        }

        public override int GetHashCode()
        {
            var hashCode = -1374444185;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + Balance.GetHashCode();
            hashCode = hashCode * -1521134295;
            return hashCode;
        }
    }
}
