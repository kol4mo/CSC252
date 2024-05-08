using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchical {
    public class Node<T> where T : IComparable<T> {
        public Node(T value) {
            this.value = value;
        }
        public T value { get; set; }
        public Node<T> left { get; set; }
        public Node<T> right { get; set; }


    }
}
