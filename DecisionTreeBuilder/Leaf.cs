using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DecisionTreeBuilder
{
    public class Leaf
    {
        public Leaf()
        {
            Children = new List<Leaf>();
        }

        public List<Leaf> Children { get; set; }

        public DecisionClass DecisionClass { get; set; }

        public DecisionClassAttribute ParentLink { get; set; }
    }
}
