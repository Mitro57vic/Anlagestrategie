using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anlagestrategie_IDPA
{
    public class InvestmentStrategy
    {
        public string Name { get; }
        public List<double> Weights { get; }
        public double Risk { get; }
        public double Return { get; }

        public InvestmentStrategy(string name, List<double> weights, double risk, double returnRate)
        {
            Name = name;
            Weights = weights;
            Risk = risk;
            Return = returnRate;
        }
    }
}
