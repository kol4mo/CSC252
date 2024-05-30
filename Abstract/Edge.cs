using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract {
	public class Edge {
		public Node nodeFrom;
		public Node nodeTo;
		public float weight;

		public Edge(Node nodeFrom, Node nodeTo, float weight) {
			this.nodeFrom = nodeFrom;
			this.nodeTo = nodeTo;
			this.weight = weight;
		}
	}
}
