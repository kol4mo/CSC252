using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract {
	public class Graph {
		public Node[] nodes;

		public Graph(string[] nodeEdgesString, string[] nodeValues) {
			Dictionary<string, float>[] nodesEdges = new Dictionary<string, float>[0];
			foreach (var edgeString in nodeEdgesString) {
				nodesEdges = nodesEdges.Append(constructDictionary(edgeString)).ToArray();
			}

			nodes = new Node[0];
			foreach (var value in nodeValues) {
				nodes = nodes.Append(new Node(value)).ToArray();
			}

			for (int i = 0; i < nodesEdges.Length; i++) {
				addNodeEdge(nodes[i], nodesEdges[i]);
			}
		}

		public void addNodeEdge(Node node, Dictionary<string, float> edgesDict) {
			foreach (var value in edgesDict) {
				foreach (var edgeNode in nodes) {
					if (edgeNode.Value.Equals(value.Key)) {
						node.addEdge(edgeNode, value.Value);
						break;
					}
				}
			}
		}

		public Dictionary<string, float> constructDictionary(string data) {
			string[] pairs = data.Split(',');
			Dictionary<string, float> edgesDictionary = new Dictionary<string, float>();
			foreach (var pair in pairs) {
				string[] values = pair.Split(':');
				string key = values[0];
				float value = float.Parse(values[1]);
				edgesDictionary[key] = value;
			}
			return edgesDictionary;
		}

		public float prims() {
			Node[] currentNodes = { nodes[0] };
			Edge[] currentEdges = { };
			Edge[] availableEdges = new Edge[0];
			foreach (var edge in currentNodes[0].edges) {
				availableEdges = availableEdges.Append(edge).ToArray();
			}
			while (true) {
			Edge currentEdge = null;
				foreach (var edge in availableEdges) {
					if (!currentNodes.Contains(edge.nodeTo)) {
						if (currentEdge == null) {
							currentEdge = edge;
						} else if (currentEdge.weight > edge.weight) {
							currentEdge = edge;
						}
					}
				}
				if (currentEdge == null) {

				} else {
					currentEdges = currentEdges.Append(currentEdge).ToArray();
					var availEdges = availableEdges.ToList();
					availEdges.Remove(currentEdge);
					availableEdges = availEdges.ToArray();
					currentNodes = currentNodes.Append(currentEdge.nodeTo).ToArray();
					foreach (var edge in currentEdge.nodeTo.edges) {
						availableEdges = availableEdges.Append(edge).ToArray();
					}
				}

				if (currentNodes.Length == nodes.Length) {
					break;
				}
			}

			float distance = 0;
			foreach (Edge edge in currentEdges) {
				distance += edge.weight;
			}

			

			return distance;
		}
	}
}
