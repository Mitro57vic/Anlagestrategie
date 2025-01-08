using System;
using System.Collections.Generic;

namespace Anlagestrategie_IDPA
{
    class Program
    {
        static void Main(string[] args)
    {
        // Beispielhafte Investments
        var investmentA = new Investment("Aktienfonds A", new List<double> { 0.08, 0.10, 0.12, -0.05, 0.07 }, 0.10, 20);
        var investmentB = new Investment("Staatsanleihen B", new List<double> { 0.03, 0.025, 0.032, 0.028, 0.031 }, 0.025, 10);


        var portfolio = new Portfolio(new List<Investment>
            {
                investmentA,
                investmentB,

            });
        // Übersicht der Investments ausgeben
        Console.WriteLine("Verfügbare Investments:");
        Console.WriteLine("-----------------------");
        foreach (var investment in portfolio.Investments)
        {
            Console.WriteLine($"Name: {investment.Name}");
            Console.WriteLine($"  Historische Renditen: {string.Join(", ", investment.HistoricalReturns.Select(r => $"{r:P2}"))}");
            Console.WriteLine($"  Wachstumsrate: {investment.GrowthRate:P2}");
            Console.WriteLine($"  Bewertung: {investment.Valuation:F2}");
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nKorrelationen zwischen Anlagen:");
        Console.ResetColor();
        Console.WriteLine("--------------------------------");
        for (int i = 0; i < portfolio.Investments.Count; i++)
        {
            var outerInvestment = portfolio.Investments[i];
            for (int j = i + 1; j < portfolio.Investments.Count; j++)
            {
                var innerInvestment = portfolio.Investments[j];
                double correlation = portfolio.CalculateCorrelation(outerInvestment.Name, innerInvestment.Name);

                // Farbige Ausgabe basierend auf der Korrelation
                if (correlation > 0.5)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Hohe positive Korrelation
                }
                else if (correlation < -0.5)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Hohe negative Korrelation
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Moderate oder keine starke Korrelation
                }

                Console.WriteLine($"Korrelation zwischen {outerInvestment.Name} und {innerInvestment.Name}: {correlation:F4}");
                Console.ResetColor(); // Zurücksetzen der Farbe
            }
        }

        // Methode für sicheres Einlesen von Double
        double GetValidDouble(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt);
            Console.ResetColor();
            double value;
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Zahl ein.");
                Console.ResetColor();
            }
        }

        // Eingabe des Anfangskapitals, Zielkapitals und Anlagezeitraums
        double startCapital = GetValidDouble("Geben Sie das Anfangskapital ein: ");
        double targetCapital = GetValidDouble("Geben Sie das Zielkapital ein: ");
        int investmentPeriod = (int)GetValidDouble("Geben Sie den Anlagezeitraum (in Jahren) ein: ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nBerechnungen abgeschlossen!");
        Console.ResetColor();
        Console.WriteLine("Jetzt geht es an die Strategien...");

        while (true)
        {
            // Berechnung der benötigten jährlichen Rendite
            double requiredReturn = Math.Pow(targetCapital / startCapital, 1.0 / investmentPeriod) - 1;
            Console.WriteLine($"\nDie benötigte jährliche Rendite, um das Ziel zu erreichen, beträgt: {requiredReturn:P2}");

            // Strategieberechnungen
            var strategies = new List<InvestmentStrategy>();

            // Standardstrategie
            var standardWeights = portfolio.CalculateStandardStrategyWeights(startCapital, targetCapital, investmentPeriod);
            double standardRisk = portfolio.CalculatePortfolioVariance(standardWeights);
            double standardReturn = portfolio.Investments
                .Zip(standardWeights, (investment, weight) => investment.HistoricalReturns.Average() * weight)
                .Sum();
            strategies.Add(new InvestmentStrategy("Standardstrategie", standardWeights, standardRisk, standardReturn));

            // Growth-Strategie
            var growthWeights = portfolio.CalculateGrowthStrategyWeights();
            double growthRisk = portfolio.CalculatePortfolioVariance(growthWeights);
            double growthReturn = portfolio.Investments
                .Zip(growthWeights, (investment, weight) => investment.HistoricalReturns.Average() * weight)
                .Sum();
            strategies.Add(new InvestmentStrategy("Growth-Strategie", growthWeights, growthRisk, growthReturn));

            // Value-Strategie
            var valueWeights = portfolio.CalculateValueStrategyWeights();
            double valueRisk = portfolio.CalculatePortfolioVariance(valueWeights);
            double valueReturn = portfolio.Investments
                .Zip(valueWeights, (investment, weight) => investment.HistoricalReturns.Average() * weight)
                .Sum();
            strategies.Add(new InvestmentStrategy("Value-Strategie", valueWeights, valueRisk, valueReturn));

            // Auswahl der besten Strategie basierend auf Risiko
            var evaluator = new StrategyEvaluator();
            var safestStrategy = evaluator.SelectSafestStrategy(strategies);
            bool isRealistic = evaluator.IsStrategyRealistic(safestStrategy, requiredReturn);

            // Debug-Ausgabe für Renditen
            foreach (var strategy in strategies)
            {
                Console.WriteLine($"{strategy.Name} - Durchschnittliche Rendite: {strategy.Return:P2}, Risiko: {strategy.Risk:F4}");
            }

            if (isRealistic)
            {
                Console.WriteLine("\nEmpfohlene Strategie:");
                Console.WriteLine($"Strategie: {safestStrategy.Name}");
                Console.WriteLine($"Risiko (Varianz): {safestStrategy.Risk:F4}");
                Console.WriteLine($"Durchschnittliche Rendite: {safestStrategy.Return:P2}");
                Console.WriteLine("Diese Strategie ist realistisch und kann Ihr Zielvermögen erreichen.");
                break;
            }
            else
            {
                Console.WriteLine("\nKeine der Strategien kann Ihr Zielvermögen erreichen.");
                foreach (var strategy in strategies)
                {
                    double gap = requiredReturn - strategy.Return;
                    Console.WriteLine($"Die {strategy.Name} verfehlt die benötigte Rendite um {gap:P2}.");
                }

                Console.WriteLine("Möglichkeiten:");
                Console.WriteLine("1. Zielkapital senken");
                Console.WriteLine("2. Anlagezeitraum verlängern");
                Console.WriteLine("3. Programm beenden");

                Console.Write("\nWählen Sie eine Option (1/2/3): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    targetCapital = GetValidDouble("Geben Sie ein neues Zielkapital ein: ");
                }
                else if (choice == "2")
                {
                    investmentPeriod = (int)GetValidDouble("Geben Sie einen neuen Anlagezeitraum (in Jahren) ein: ");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Programm beendet.");
                    break;
                }
                else
                {
                    Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie erneut.");
                }
            }
        }
    }
}
}
