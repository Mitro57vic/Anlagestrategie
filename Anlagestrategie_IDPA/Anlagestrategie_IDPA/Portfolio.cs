﻿using System;
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

        public List<double> CalculateStandardStrategyWeights(double startCapital, double targetCapital, int years)
        {
            var avgReturns = Investments.Select(i => i.HistoricalReturns.Average()).ToList();
            var totalReturn = avgReturns.Sum();
            return avgReturns.Select(r => r / totalReturn).ToList();
        }

        public List<double> CalculateGrowthStrategyWeights()
        {
            var growthRates = Investments.Select(i => i.GrowthRate).ToList();
            var totalGrowthRate = growthRates.Sum();
            return growthRates.Select(gr => gr / totalGrowthRate).ToList();
        }

        public List<double> CalculateValueStrategyWeights()
        {
            var valuations = Investments.Select(i => 1 / i.Valuation).ToList();
            var totalValuation = valuations.Sum();
            return valuations.Select(val => val / totalValuation).ToList();
        }

        public double CalculatePortfolioVariance(List<double> weights)
        {
            double variance = 0;

            for (int i = 0; i < Investments.Count; i++)
            {
                for (int j = 0; j < Investments.Count; j++)
                {
                    double covariance = i == j
                        ? Investments[i].CalculateVariance() // Varianz für gleiche Anlage
                        : CalculateCovariance(Investments[i].HistoricalReturns, Investments[j].HistoricalReturns);

                    variance += weights[i] * weights[j] * covariance;
                }
            }

            return variance;
        }
        private double CalculateCovariance(List<double> returnsA, List<double> returnsB)
        {
            double avgA = returnsA.Average();
            double avgB = returnsB.Average();

            return returnsA.Zip(returnsB, (a, b) => (a - avgA) * (b - avgB)).Average();
        }
    }
}
