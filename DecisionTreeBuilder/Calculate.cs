using System;

namespace DecisionTreeBuilder
{
    public static class Calculate
    {
        public static double Entropy(int positives, int negatives)
        {
            double entropy = 0;

            if (positives == 0 || negatives == 0)
                return entropy;

            int cardinality = positives + negatives;

            double s1 = ((double) positives) / cardinality;
            double s2 = ((double) negatives) / cardinality;

            entropy = - s1 * Math.Log(s1, 2.0) - s2 * Math.Log(s2, 2.0);

            return entropy;
        }
    }
}

