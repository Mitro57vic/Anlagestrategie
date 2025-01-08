using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anlagestrategie_IDPA
{
    public class StrategyEvaluator
    {
        // Wählt die sicherste Strategie basierend auf Risiko
        public InvestmentStrategy SelectSafestStrategy(List<InvestmentStrategy> strategies)
        {
            return strategies.OrderBy(s => s.Risk).First();
        }

        // Überprüfung, ob eine Strategie das Ziel realistisch erreichen kann
        public bool IsStrategyRealistic(InvestmentStrategy strategy, double requiredReturn)
        {
            return strategy.Return >= requiredReturn;
        }
    }
}
