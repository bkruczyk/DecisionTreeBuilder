using System;
using System.Collections.Generic;
using System.IO;

namespace DecisionTreeBuilder
{
    public class DecisionSystem
    {
        public void Load(string path)
        {
            using (var sr = new StreamReader(path))
            {
                int height = 0;
				
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                                        
                    height++;
                }
				
                Records = new string[height][];
				
                sr.BaseStream.Position = 0;
                sr.BaseStream.Flush();
				
                for (int i = 0; i < height; i++)
                    Records[i] = sr.ReadLine().Split(new char[] { ' ', '\t' });
            }
        }

        public string[] GetDecisionClassVector(int decisionClass)
        {
            string[] decisionClassVector = new string[Records.Length];
			
            for (int i = 0; i < Records.Length; i++)
                decisionClassVector[i] = Records[i][decisionClass];
            			
            return decisionClassVector;
        }

        public DecisionSystem GetSubset(string decisionClassName, 
                                        string attributeName)
        {
            int classNumber;

            for (classNumber = 1; 
                 classNumber < Records[0].Length - 1; 
                 classNumber++)
                if (Records[0][classNumber] == decisionClassName)
                    break;
            
            var recordsList = new List<string[]>();

            recordsList.Add(Records[0]);

            for (int i = 1; i < Records.Length; i++)
                if (Records[i][classNumber] == attributeName)
                    recordsList.Add(Records[i]);

            var subset = new DecisionSystem();

            subset.Records = recordsList.ToArray();

            return subset;
        }

        public string[][] Records
        { 
            get
            { 
                return records; 
            } 

            set
            { 
                records = value; 
            } 
        }

        private string[][] records;
    }
}

