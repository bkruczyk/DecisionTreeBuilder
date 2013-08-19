using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTreeBuilder
{
    public class DecisionTree
    {
        public void Build(DecisionSystem ds)
        {
            Root = build(ds, null);
        }

        public Leaf Root { get { return root; } set { root = value; } }

        private Leaf build(DecisionSystem ds, DecisionClassAttribute parentLink)
        {
            var leaf = new Leaf { ParentLink = parentLink };

            // an end of recursion
            // if entropy equals 0 decision is deterministic
            if (parentLink != null && parentLink.Entropy == 0)
                return leaf;

            // divide decision system into decision classes, assuming that
            // first row contains labels, 
            // first column contains ordinal,
            // last column contains decision
            var records = ds.Records;
                        
            var decisionClasses = new List<DecisionClass>();
                        
            var decisionsVector = ds.
                GetDecisionClassVector(ds.Records[0].Length - 1);

            for (int i = 1; i < records[0].Length - 1; i++)
            {
                var decisionClassVector = ds.GetDecisionClassVector(i);
                                
                var dc = new DecisionClass(decisionClassVector, 
                                           decisionsVector);
                                
                decisionClasses.Add(dc);
            }

            decisionClasses.ForEach(x => x.BuildAttributesList());

            // sort decision classes by their gain
            decisionClasses.Sort();

            // get class with the highest gain as a leaf
            leaf.DecisionClass = decisionClasses.Last();

            // we continue to build tree, 
            // searching for the next decision class for leaf
            foreach (var attribute in leaf.DecisionClass.Attributes)
            {
                var subset = ds.GetSubset(leaf.DecisionClass.Name, 
                                          attribute.AttributeName);

                leaf.Children.Add(build(subset, attribute));
            }

            return leaf;
        }

        private Leaf root;
    }
}

