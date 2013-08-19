using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTreeBuilder
{
    public class DecisionClass : IComparable<DecisionClass>
    {
        public DecisionClass(string[] decisionClassVector, 
                             string[] decisionsVector)
        {
            name = decisionClassVector[0];
            entropy = GetEntropy(decisionsVector);

            this.decisionsVector = decisionsVector;
            this.decisionClassVector = decisionClassVector;
        }

        public int CompareTo(DecisionClass other)
        {
            if (this.Gain > other.Gain)
                return 1;
            else if (this.Gain < other.Gain)
                return -1;
            else
                return 0;
        }

        public void BuildAttributesList()
        {
            Attributes = new List<DecisionClassAttribute>();
			
            for (int i = 1; i < this.decisionClassVector.Length; i++)
            {
                string attributeName = this.decisionClassVector[i];
                string decision = this.decisionsVector[i];
				
                var attribute = Attributes
                    .Where(x => x.AttributeName == attributeName)
                    .FirstOrDefault();
				
                if (attribute == null)
                {
                    attribute = new DecisionClassAttribute(attributeName);

                    Attributes.Add(attribute);
                }

                if (decision == "No")
                    attribute.Negatives++;
                else
                    attribute.Positives++;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public List<DecisionClassAttribute> Attributes
        { 
            get;
            set; 
        }

        public double Entropy
        {
            get
            {
                return entropy;
            }
        }

        public double? Gain
        {
            get
            {
                return GetGain();
            }
        }

        private double? GetGain()
        {
            if (Attributes == null)
                return null;

            double gain = Entropy;

            int quantity = 0;

            foreach (var attribute in Attributes)
                quantity += attribute.Cardinality;
            
            foreach (var attribute in Attributes)
                gain -= (((double)attribute.Cardinality) / quantity)
                    * attribute.Entropy;
            
            return gain;
        }

        private double GetEntropy(string[] decisionsVector)
        {
            int positives = 0;
            int negatives = 0;

            for (int i = 1; i < decisionsVector.Length; i++)
            {
                if (decisionsVector[i] == "No")
                    negatives++;
                else
                    positives++;
            }

            return Calculate.Entropy(positives, negatives);
        }

        private readonly string name;
        private readonly string[] decisionClassVector;
        private readonly string[] decisionsVector;
        private readonly double entropy;
    }
}

