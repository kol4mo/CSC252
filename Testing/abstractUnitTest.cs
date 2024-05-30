using Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing {
	[TestClass]
	public class abstractUnitTest {
		string[] nodesValues = { "A", "B", "C", "D", "E", "F" };
		string[] nodesEdgeString = {
			"B:1",
			"A:1,C:1,D:2,F:1",
			"B:1",
			"B:1,E:1,F:1",
			"D:1,F:1",
			"B:1,D:1,E:1"
		};

		string[] nodesValues2 = { "AX1", "AX2", "AX3", "AX4", "AX5" };
		string[] nodesEdgeString2 = {
			"AX4:3,AX2:3,AX3:6",
			"AX1:3,AX3:3,AX4:6",
			"AX2:3,AX1:6,AX4:4",
			"AX1:3,AX2:6,AX3:4,AX5:15",
			"AX4:15"
		};


		[TestMethod]
		public void GraphCreate_happy() {
			Graph graph = new Graph(nodesEdgeString, nodesValues);

			Assert.IsTrue(graph.nodes[0].Value.Equals("A"));
			Assert.IsTrue(graph.nodes[0].edges[0].nodeTo.Value.Equals("B"));
			Assert.IsTrue(graph.nodes[0].edges[0].weight.Equals(1));
			Assert.IsTrue(graph.nodes[1].Value.Equals("B"));
			Assert.IsTrue(graph.nodes[1].edges[2].nodeTo.Value.Equals("D"));
			Assert.IsTrue(graph.nodes[1].edges[2].weight.Equals(2));
		}
		[TestMethod]
		public void GraphCreate_complicated() {
			Graph graph = new Graph(nodesEdgeString2, nodesValues2);

			Assert.IsTrue(graph.nodes[0].Value.Equals("AX1"));
			Assert.IsTrue(graph.nodes[0].edges[0].nodeTo.Value.Equals("AX4"));
			Assert.IsTrue(graph.nodes[0].edges[0].weight.Equals(3));
			Assert.IsTrue(graph.nodes[1].Value.Equals("AX2"));
			Assert.IsTrue(graph.nodes[1].edges[2].nodeTo.Value.Equals("AX4"));
			Assert.IsTrue(graph.nodes[1].edges[2].weight.Equals(6));
		}
		[TestMethod]
		public void GraphPrims_Happy() {
			Graph graph = new Graph(nodesEdgeString, nodesValues);

			float weight = graph.prims();

			Assert.AreEqual(5, weight);
		}

		[TestMethod]
		public void GraphPrims_complicatedGraph() {
			Graph graph = new Graph(nodesEdgeString2, nodesValues2);

			float weight = graph.prims();

			Assert.AreEqual(24, weight);
		}
	}
}
