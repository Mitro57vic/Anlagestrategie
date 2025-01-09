using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anlagestrategie_IDPA;
using System.Collections.Generic;

namespace Anlagestrategien_Test
{
    [TestClass]
    public class StrategyEvaluatorTest
    {
        [TestMethod]
        public void TestSelectSafestStrategy()
        {
            // Arrange
            var strategyA = new InvestmentStrategy("Test Strategy A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var strategyB = new InvestmentStrategy("Test Strategy B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var strategyC = new InvestmentStrategy("Test Strategy C", new List<double> { 0.3, 0.4, 0.5, 0.6, 0.7 }, 0.4, 30);
            var strategies = new List<InvestmentStrategy> { strategyA, strategyB, strategyC };
            var evaluator = new StrategyEvaluator();

            // Act
            var safestStrategy = evaluator.SelectSafestStrategy(strategies);

            // Assert
            Assert.AreEqual(strategyA, safestStrategy, "Die sicherste Strategie wurde nicht korrekt ausgewählt.");
        }
        [TestMethod]
        public void TestIsStrategyRealistic()
        {
            // Arrange
            var strategy = new InvestmentStrategy("Test Strategy", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var evaluator = new StrategyEvaluator();

            // Act
            var isRealistic = evaluator.IsStrategyRealistic(strategy, 5);

            // Assert
            Assert.IsTrue(isRealistic, "Die Strategie sollte realistisch sein.");
        }

    }
}
