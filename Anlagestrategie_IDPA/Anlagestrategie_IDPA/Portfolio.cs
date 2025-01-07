using System;
using System.Collections.Generic;
using System.Linq;

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
            // Dynamische Berechnung der Gewichtungen basierend auf Zielvorgaben
            {
                // Berechne erforderliche jährliche Wachstumsrate, um das Zielkapital zu erreichen
                Console.WriteLine($"Startkapital: {startCapital}, Zielkapital: {targetCapital}, Jahre: {years}");
                double requiredGrowthRate = Math.Pow(targetCapital / startCapital, 1.0 / years) - 1;

                // Berechne Gewichtungen basierend auf der Differenz zwischen erwarteten Renditen und der erforderlichen Wachstumsrate
                var avgReturns = Investments.Select(i => i.HistoricalReturns.Average()).ToList();
                var differences = avgReturns.Select(r => Math.Abs(r - requiredGrowthRate)).ToList();
                double totalDifference = differences.Sum();

                var weights = differences.Select(d => 1 - (d / totalDifference)).ToList();
                Console.WriteLine("Gewichtungen für Standardstrategie:");
                for (int i = 0; i < weights.Count; i++)
                {
                    Console.WriteLine($"{Investments[i].Name}: {weights[i] * 100:F2}%");
                }

                return weights;
            }
        }

        public List<double> CalculateGrowthStrategyWeights(double startCapital, double targetCapital, int years)
            {
                // Berechne erforderliche jährliche Wachstumsrate, um das Zielkapital zu erreichen
                double requiredGrowthRate = Math.Pow(targetCapital / startCapital, 1.0 / years) - 1;

                // Berechne Gewichtungen basierend auf der Differenz zwischen Wachstumsraten und der erforderlichen Wachstumsrate
                var growthRates = Investments.Select(i => i.GrowthRate).ToList();
                var differences = growthRates.Select(gr => Math.Abs(gr - requiredGrowthRate)).ToList();
                double totalDifference = differences.Sum();
                return differences.Select(d => 1 - (d / totalDifference)).ToList();
            }

            public List<double> CalculateValueStrategyWeights(double startCapital, double targetCapital, int years)
            {
                // Berechne Gewichtungen basierend auf Bewertungen, angepasst an das Zielkapital
                double capitalFactor = targetCapital / startCapital;
                var valuations = Investments.Select(i => capitalFactor / i.Valuation).ToList();
                var totalValuation = valuations.Sum();
                return valuations.Select(val => val / totalValuation).ToList();
            }

            public double CalculatePortfolioVariance(List<double> weights)
            {
                Console.WriteLine("Berechnung der Portfolio-Varianz gestartet...");
                {
                    double variance = 0;

                    for (int i = 0; i < Investments.Count; i++)
                    {
                        for (int j = 0; j < Investments.Count; j++)
                        {
                            double covariance = i == j
                                ? Investments[i].CalculateVariance() // Varianz für gleiche Anlage
                                : CalculateCovariance(Investments[i].HistoricalReturns,
                                    Investments[j].HistoricalReturns);

                            variance += weights[i] * weights[j] * covariance;
                        }
                    }

                    Console.WriteLine($"Portfolio-Varianz basierend auf Gewichtungen: {variance:F4}");
                    return variance;
                }
            }

            private double CalculateCovariance(List<double> returnsA, List<double> returnsB)
            {
                    double avgA = returnsA.Average();
                    double avgB = returnsB.Average();

                    return returnsA.Zip(returnsB, (a, b) => (a - avgA) * (b - avgB)).Average();
            }
            
        
    }
}
