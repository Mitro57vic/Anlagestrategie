using System;
using System.Collections.Generic;
using System.Linq;

namespace Anlagestrategie_IDPA
{
    public class Investment
    {
        public string Name { get; }
        public List<double> HistoricalReturns { get; }
        public double GrowthRate { get; } // Wachstumsrate für Growth-Strategie
        public double Valuation { get; }  // Bewertung für Value-Strategie

        public Investment(string name, List<double> historicalReturns, double growthRate, double valuation)
        {
            Name = name;
            HistoricalReturns = historicalReturns;
            GrowthRate = growthRate;
            Valuation = valuation;
        }

        // Durchschnittliche Rendite
        public double AverageReturn => HistoricalReturns.Average();

        // Berechnung der Varianz
        public double CalculateVariance()
        {
            double average = HistoricalReturns.Average();
            return HistoricalReturns.Average(r => Math.Pow(r - average, 2));
        }
    }
}
