using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anlagestrategie_IDPA;
using System.Collections.Generic;

namespace Anlagestrategien_Test
{
    [TestClass]
    public class InvestmentUnitTests
    {
        [TestMethod]
        public void CalculateVariance_ShouldReturnCorrectVariance()
        {
            // Arrange
            var investment = new Investment("TestInvestment", new List<double> { 0.1, 0.2, 0.15, 0.1 }, 0.05, 10);

            // Erwartete Varianz (manuelle Berechnung):
            // Mittelwert = (0.1 + 0.2 + 0.15 + 0.1) / 4 = 0.1375
            // Varianz = [(0.1 - 0.1375)^2 + (0.2 - 0.1375)^2 + ...] / 4 = 0.00171875
            double expectedVariance = 0.00171875;

            // Act
            var variance = investment.CalculateVariance();

            // Assert
            Assert.AreEqual(expectedVariance, variance, 0.0001);
        }

        [TestMethod]
        public void CalculateCorrelation_ShouldReturnCorrectValue()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1, 0.2, 0.15 }, 0.05, 10);
            var investmentB = new Investment("B", new List<double> { 0.05, 0.1, 0.1 }, 0.04, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Act
            var correlation = portfolio.CalculateCorrelation("A", "B");

            // Assert
            Assert.AreEqual(0.866, correlation, 0.001);
        }

        [TestMethod]
        public void CalculateStandardStrategyWeights_ShouldReturnCorrectWeights()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1, 0.15 }, 0.12, 15);
            var investmentB = new Investment("B", new List<double> { 0.05, 0.08 }, 0.08, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Erwartete Gewichtungen (manuelle Berechnung):
            // Durchschnittliche Renditen:
            // A: (0.1 + 0.15) / 2 = 0.125
            // B: (0.05 + 0.08) / 2 = 0.065
            // Total: 0.125 + 0.065 = 0.19
            // Gewichtungen: A = 0.125 / 0.19, B = 0.065 / 0.19
            double expectedWeightA = 0.125 / 0.19;
            double expectedWeightB = 0.065 / 0.19;

            // Act
            var weights = portfolio.CalculateStandardStrategyWeights(1000, 2000, 5);

            // Assert
            Assert.AreEqual(expectedWeightA, weights[0], 0.01);
            Assert.AreEqual(expectedWeightB, weights[1], 0.01);
        }


        [TestMethod]
        public void CalculateGrowthStrategyWeights_ShouldReturnCorrectWeights()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1 }, 0.12, 15);
            var investmentB = new Investment("B", new List<double> { 0.1 }, 0.08, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Act
            var weights = portfolio.CalculateGrowthStrategyWeights();

            // Assert
            Assert.AreEqual(0.6, weights[0], 0.01);
            Assert.AreEqual(0.4, weights[1], 0.01);
        }

        [TestMethod]
        public void CalculateValueStrategyWeights_ShouldReturnCorrectWeights()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1 }, 0.05, 5);
            var investmentB = new Investment("B", new List<double> { 0.1 }, 0.04, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Act
            var weights = portfolio.CalculateValueStrategyWeights();

            // Assert
            Assert.AreEqual(0.67, weights[0], 0.01);
            Assert.AreEqual(0.33, weights[1], 0.01);
        }

        [TestMethod]
        public void CalculatePortfolioVariance_ShouldReturnCorrectValue()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1, 0.15, 0.2 }, 0.05, 10);
            var investmentB = new Investment("B", new List<double> { 0.05, 0.1, 0.08 }, 0.04, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            var weights = new List<double> { 0.6, 0.4 };

            // Erwartete Berechnung (manuell):
            // Var(A) = 0.0025, Var(B) = 0.001944
            // Cov(A, B) (manuelle Berechnung der Kovarianz)
            double expectedCovariance = 0.0003; // Beispielwert, tatsächliche Berechnung hängt von Returns ab
            double expectedVariance = (weights[0] * weights[0] * investmentA.CalculateVariance()) +
                                      (weights[1] * weights[1] * investmentB.CalculateVariance()) +
                                      (2 * weights[0] * weights[1] * expectedCovariance);

            // Act
            var variance = portfolio.CalculatePortfolioVariance(weights);

            // Assert
            Assert.AreEqual(expectedVariance, variance, 0.0001);
        }
    }

}
