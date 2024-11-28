using System;
using System.Collections.Generic;

namespace Anlagestrategie_IDPA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Eingabe des Anfangskapitals und Zielkapitals
            Console.Write("Geben Sie das Anfangskapital ein: ");
            double startCapital = double.Parse(Console.ReadLine());
            Console.Write("Geben Sie das Zielkapital ein: ");
            double targetCapital = double.Parse(Console.ReadLine());
            Console.Write("Geben Sie den Anlagezeitraum (in Jahren) ein: ");
            int investmentPeriod = int.Parse(Console.ReadLine());

            // Beispielhafte historische Renditen, Wachstumsraten und Bewertungen für zwei Anlagen
            var investmentA = new Investment("Anlage A", new List<double> { 0.07, 0.05, 0.08, 0.06, 0.09 }, 0.12, 15);
            var investmentB = new Investment("Anlage B", new List<double> { 0.04, 0.03, 0.05, 0.04, 0.03 }, 0.08, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            var strategy = new InvestmentStrategy(portfolio);

            // Auswahl der Anlagestrategie
            Console.WriteLine("\nWählen Sie die Anlagestrategie:");
            Console.WriteLine("1 - Standardstrategie (basierend auf durchschnittlichen Renditen)");
            Console.WriteLine("2 - Growth-Strategie (basierend auf Wachstumsraten)");
            Console.WriteLine("3 - Value-Strategie (basierend auf Bewertungen)");
            int choice = int.Parse(Console.ReadLine());

            List<double> weights;
            switch (choice)
            {
                case 1:
                    weights = strategy.CalculateOptimalWeights(startCapital, targetCapital, investmentPeriod);
                    Console.WriteLine("\nGewichtungen der Standardstrategie:");
                    break;
                case 2:
                    weights = strategy.CalculateGrowthStrategyWeights();
                    Console.WriteLine("\nGewichtungen der Growth-Strategie:");
                    break;
                case 3:
                    weights = strategy.CalculateValueStrategyWeights();
                    Console.WriteLine("\nGewichtungen der Value-Strategie:");
                    break;
                default:
                    Console.WriteLine("Ungültige Wahl!");
                    return;
            }

            // Ausgabe der Gewichtungen
            for (int i = 0; i < weights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {weights[i] * 100:F2}%");
            }
        }
    }
}
