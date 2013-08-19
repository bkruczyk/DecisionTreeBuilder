using System;
using QuickGraph;

namespace DecisionTreeBuilder
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string outputFileName = "graph.dot";

            if (args.Length == 0)
            {
                printHelp();

                return;
            }

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--output-file-name":
                        i++;

                        outputFileName = args[i];

                        break;

                    default:

                        break;
                }
            }

            string path = args[0];

            var ds = new DecisionSystem();

            ds.Load(path);

            var tree = new DecisionTree();

            tree.Build(ds);

            var graph = new DecisionTreeGraph(tree);

            graph.BuildGraph();
            graph.DrawGraph(outputFileName);
        }

        private static void printHelp()
        {
            Console.WriteLine("DecisionTreeBuilder - " 
                + "Builds decision tree from decision system and produce "
                + "dot file for Graphviz\n" 
                + "usage: DecisionTreeBuilder <path to decision system> "
                + "[--output-file-name <name>]");
        }
    }
}