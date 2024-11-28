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

            // Methode für sicheres Einlesen von Integer innerhalb eines Bereichs
            int GetValidInt(string prompt, int min, int max)
            {
                int value;
                while (true)
                {
                    Console.Write(prompt);
                    if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                        return value;
                    Console.WriteLine($"Ungültige Eingabe. Bitte geben Sie eine Zahl zwischen {min} und {max} ein.");
                }
            }

            // Eingabe des Anfangskapitals und Zielkapitals
            double startCapital = GetValidDouble("Geben Sie das Anfangskapital ein: ");
            double targetCapital = GetValidDouble("Geben Sie das Zielkapital ein: ");
            int investmentPeriod = GetValidInt("Geben Sie den Anlagezeitraum (in Jahren) ein: ", 1, 100);

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
            int choice = GetValidInt("Ihre Wahl: ", 1, 3);

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
                    // Dieser Fall wird nie eintreten, da GetValidInt nur 1-3 erlaubt
                    throw new InvalidOperationException("Ungültige Strategieauswahl.");
            }

            // Ausgabe der Gewichtungen
            for (int i = 0; i < weights.Count; i++)
            {
                Console.WriteLine($"{portfolio.Investments[i].Name}: {weights[i] * 100:F2}%");
            }
        }
    }
}
