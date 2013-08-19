using System;

namespace DecisionTreeBuilder
{
    public class DecisionClassAttribute
    {
        public DecisionClassAttribute(string attributeName)
        {
            this.attributeName = attributeName;
        }

        public string AttributeName
        { 
            get
            {
                return this.attributeName;
            }
        }

        public int Positives { get; set; }

        public int Negatives { get; set; }

        public int Cardinality
        { 
            get
            { 
                return Positives + Negatives; 
            } 
        }

        public double Entropy
        { 
            get
            { 
                return Calculate.Entropy(Positives, Negatives); 
            } 
        }

        private readonly string attributeName;
    }
}

