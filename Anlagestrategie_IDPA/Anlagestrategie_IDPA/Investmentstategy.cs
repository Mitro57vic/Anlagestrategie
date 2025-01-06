using System;
using System.Collections.Generic;
using System.Linq;

namespace Anlagestrategie_IDPA
{
    public class InvestmentStrategy
    {//
     //private Portfolio Portfolio { get; }
     //
     //public InvestmentStrategy(Portfolio portfolio)
     //{
     //    Portfolio = portfolio;
     //}
     //
     //
     //public List<double> CalculateOptimalWeights(double startCapital, double targetCapital, int years)
     //{
     //    double annualizedTargetReturn = Math.Pow(targetCapital / startCapital, 1.0 / years) - 1;
     //    var avgReturns = Portfolio.Investments.Select(i => i.HistoricalReturns.Average()).ToList();
     //    var totalReturn = avgReturns.Sum();
     //    var weights = avgReturns.Select(r => r / totalReturn).ToList();
     //
     //    return weights;
     //}
     //
     //// Growth-Strategie: Gewichtung basierend auf Wachstumsraten
     //public List<double> CalculateGrowthStrategyWeights()
     //{
     //    var growthRates = Portfolio.Investments.Select(i => i.GrowthRate).ToList();
     //    var totalGrowthRate = growthRates.Sum();
     //    var weights = growthRates.Select(gr => gr / totalGrowthRate).ToList();
     //
     //    return weights;
     //}
     //
     //
     //public List<double> CalculateValueStrategyWeights()
     //{
     //    var valuations = Portfolio.Investments.Select(i => 1 / i.Valuation).ToList(); 
     //    var totalValuation = valuations.Sum();
     //    var weights = valuations.Select(val => val / totalValuation).ToList();
     //
     //    return weights;
     //}
    }
}
