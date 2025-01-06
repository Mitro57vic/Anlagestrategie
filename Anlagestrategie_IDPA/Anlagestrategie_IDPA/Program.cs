using System;
using System.Collections.Generic;

namespace Anlagestrategie_IDPA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Methode für sicheres Einlesen von Double
            double GetValidDouble(string prompt)
            {
                double value;
                while (true)
                {
                    Console.Write(prompt);
                    if (double.TryParse(Console.ReadLine(), out value))
                        return value;
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Zahl ein.");
                }
            }

            // Eingabe des Anfangskapitals und Zielkapitals
            double startCapital = GetValidDouble("Geben Sie das Anfangskapital ein: ");
            double targetCapital = GetValidDouble("Geben Sie das Zielkapital ein: ");
            int investmentPeriod = (int)GetValidDouble("Geben Sie den Anlagezeitraum (in Jahren) ein: ");

            // Beispielhafte historische Renditen, Wachstumsraten und Bewertungen für zwei Anlagen
            var investmentA = new Investment("Anlage A", new List<double> { 0.07, 0.05, 0.08, 0.06, 0.09 }, 0.12, 15);
            var investmentB = new Investment("Anlage B", new List<double> { 0.04, 0.03, 0.05, 0.04, 0.03 }, 0.08, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            Console.WriteLine("\nBerechnete Werte für Variance und Korrelation:");

            foreach (var investment in portfolio.Investments)
            {
                double variance = investment.CalculateVariance();
                Console.WriteLine($"Varianz von {investment.Name}: {variance:F4}");
            }

            double correlation = portfolio.CalculateCorrelation("Anlage A", "Anlage B");
            Console.WriteLine($"Korrelation zwischen Anlage A und Anlage B: {correlation:F4}");

            Console.WriteLine("\nBerechnungen für Strategien:");

            // Standardstrategie
            var standardWeights = portfolio.CalculateStandardStrategyWeights(startCapital, targetCapital, investmentPeriod);
            double standardRisk = portfolio.CalculatePortfolioVariance(standardWeights);
            Console.WriteLine("Standardstrategie:");
            Console.WriteLine($"Risiko (Varianz): {standardRisk:F4}");
            for (int i = 0; i < standardWeights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {standardWeights[i] * 100:F2}%");
            }

            // Growth-Strategie
            var growthWeights = portfolio.CalculateGrowthStrategyWeights();
            double growthRisk = portfolio.CalculatePortfolioVariance(growthWeights);
            Console.WriteLine("\nGrowth-Strategie:");
            Console.WriteLine($"Risiko (Varianz): {growthRisk:F4}");
            for (int i = 0; i < growthWeights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {growthWeights[i] * 100:F2}%");
            }

            // Value-Strategie
            var valueWeights = portfolio.CalculateValueStrategyWeights();
            double valueRisk = portfolio.CalculatePortfolioVariance(valueWeights);
            Console.WriteLine("\nValue-Strategie:");
            Console.WriteLine($"Risiko (Varianz): {valueRisk:F4}");
            for (int i = 0; i < valueWeights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {valueWeights[i] * 100:F2}%");
            }

            // Auswahl der Strategie basierend auf Sicherheit
            var strategies = new List<(string Name, List<double> Weights, double Risk)>
            {
                ("Standardstrategie", standardWeights, standardRisk),
                ("Growth-Strategie", growthWeights, growthRisk),
                ("Value-Strategie", valueWeights, valueRisk)
            };

            var safestStrategy = strategies.OrderBy(s => s.Risk).First();

            Console.WriteLine("\nEmpfohlene Strategie:");
            Console.WriteLine($"Strategie: {safestStrategy.Name}");
            Console.WriteLine($"Risiko (Varianz): {safestStrategy.Risk:F4}");
            Console.WriteLine("Gewichtungen:");
            for (int i = 0; i < safestStrategy.Weights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {safestStrategy.Weights[i] * 100:F2}%");
            }
        }
    }
}
