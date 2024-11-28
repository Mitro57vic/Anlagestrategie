using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anlagestrategie_IDPA
{
    public class Portfolio
    {


  
            public List<Investment> Investments { get; }

            public Portfolio(List<Investment> investments)
            {
                Investments = investments;
            }

            public double CalculateCorrelation(string investmentA, string investmentB)
            {
                var returnsA = Investments.First(i => i.Name == investmentA).HistoricalReturns;
                var returnsB = Investments.First(i => i.Name == investmentB).HistoricalReturns;
                double avgA = returnsA.Average();
                double avgB = returnsB.Average();

                double covariance = returnsA.Zip(returnsB, (a, b) => (a - avgA) * (b - avgB)).Average();
                double varianceA = returnsA.Average(r => Math.Pow(r - avgA, 2));
                double varianceB = returnsB.Average(r => Math.Pow(r - avgB, 2));

                return covariance / Math.Sqrt(varianceA * varianceB);
            }
        }
    }

