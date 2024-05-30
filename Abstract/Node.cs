using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract {
	public class Node {
		public string Value;
		public Edge[] edges;
		//Node<T> previous;

		public Node(string value) {
			this.Value = value;
			edges = new Edge[0];
		}

		/// <summary>
		/// adds the edge to this node BigO (1)
		/// </summary>
		/// <param name="other">the node where this edge will lead</param>
		/// <param name="weight">the cost or weight of the edge</param>
		public void addEdge(Node other, float weight) {
			Edge edge = new Edge(this, other, weight);
			edges = edges.Append(edge).ToArray();
		}
	}
}
