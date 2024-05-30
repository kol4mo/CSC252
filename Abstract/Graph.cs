using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract {
	public class Graph {
		public Node[] nodes;
		/// <summary>
		/// Creates Graph BigO (n)
		/// </summary>
		/// <param name="nodeEdgesString"> a list of string that identifies each edges</param>
		/// <param name="nodeValues">the values used for each node</param>
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

		/// <summary>
		/// adds all the edges to a node BigO (n^2)
		/// </summary>
		/// <param name="node">the node edges are added to</param>
		/// <param name="edgesDict">the key is the values contained by nodes, and the value is the weight of the edge</param>
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

		/// <summary>
		/// creates a dictionary out of string information for each edge BigO (n)
		/// </summary>
		/// <param name="data">the string of information formated as K:V,K:V,K:V</param>
		/// <returns>a dictionary continaing information for each edge</returns>
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

		/*
		 *float prims()
		 *	initilize variables
		 *	mst nodes = first node
		 *	avaiable edges = first node edges
		 *	while searching {
		 *		for each edge in avaiable edges {
		 *			check if edge contains nodes not introduced
		 *				if the edge to be added hasnt been set, set edge
		 *				else check if edge to be added weight less then edge
		 *		}
		 *		
		 *		if current edge is not null {
		 *			add current edge to mst edges
		 *			remove current edge from available edges
		 *			mst nodes adds edge connecting node
		 *			add all edges from connecting node
		 *		}
		 *	}
		 *	
		 *	count up mst edges weights
		 *	
		 *	return weights
		 */

		/// <summary>
		/// finds the Minimum spanning tree using the prims algorithm BigO(n^2)
		/// </summary>
		/// <returns>the total length of the found minimum spanning tree</returns>
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
