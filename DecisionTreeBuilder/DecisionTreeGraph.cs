using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace DecisionTreeBuilder
{
    class DecisionTreeGraph
    {
        public DecisionTreeGraph(DecisionTree tree)
        {
            this.tree = tree;

            graph = new AdjacencyGraph<Leaf, TaggedEdge<Leaf, string>>();
        }

        public void BuildGraph()
        {
            buildGraph(tree.Root);
        }

        public void DrawGraph(string outputFileName)
        {
            var graphviz = 
                new GraphvizAlgorithm<Leaf, TaggedEdge<Leaf, string>>(graph);

            graphviz.CommonVertexFormat.Shape = GraphvizVertexShape.Box;
            graphviz.FormatVertex += 
                new FormatVertexEventHandler<Leaf>(graphviz_FormatVertex);
            graphviz.FormatEdge += (sender, e) => {
                e.EdgeFormatter.Label.Value = e.Edge.Tag; 
            };
            graphviz.Generate(new FileDotEngine(), outputFileName);
        }

        private void graphviz_FormatVertex(object sender, 
                                           FormatVertexEventArgs<Leaf> e)
        {
            // decision
            if (e.Vertex.DecisionClass != null)
            {
                e.VertexFormatter.Label = e.Vertex.DecisionClass.Name;

                return;
            }

            e.VertexFormatter.Shape = GraphvizVertexShape.Diamond;

            if (e.Vertex.ParentLink.Negatives == 0)
                e.VertexFormatter.Label = "Yes";
            else 
                e.VertexFormatter.Label = "No";
        }

        private void buildGraph(Leaf parentVertex)
        {
            foreach (var childVertex in parentVertex.Children)
            {
                var edge = new TaggedEdge<Leaf, string>(
                    parentVertex, 
                    childVertex, 
                    childVertex.ParentLink.AttributeName);

                graph.AddVerticesAndEdge(edge);
      
                buildGraph(childVertex);
            }
        }

        private DecisionTree tree;
        private AdjacencyGraph<Leaf, TaggedEdge<Leaf, string>> graph;
    }

    /// <summary>
    /// Default dot engine implementation, writes dot code to disk
    /// </summary>
    public sealed class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, 
                          string dot, 
                          string outputFileName)
        {
            string output = outputFileName; // +".dot";
            
            using (var sw = new StreamWriter(output))
            {
                sw.Write(dot);
                sw.Close();
            }
            
            return output;
        }
    }
}
