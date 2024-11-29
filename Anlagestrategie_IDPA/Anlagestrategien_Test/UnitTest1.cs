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

            // Act
            var variance = investment.CalculateVariance();

            // Assert
            Assert.AreEqual(0.00171875, variance, 0.0001);
        }


        [TestMethod]
        public void CalculateGrowthStrategyWeights_ShouldReturnCorrectWeights()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1 }, 0.12, 10);
            var investmentB = new Investment("B", new List<double> { 0.1 }, 0.08, 10);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            var strategy = new InvestmentStrategy(portfolio);

            // Act
            var weights = strategy.CalculateGrowthStrategyWeights();

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
            var strategy = new InvestmentStrategy(portfolio);

            // Act
            var weights = strategy.CalculateValueStrategyWeights();

            // Assert
            Assert.AreEqual(0.67, weights[0], 0.01);
            Assert.AreEqual(0.33, weights[1], 0.01);
        }

        [TestMethod]
        public void CalculateOptimalWeights_ShouldReturnCorrectWeights()
        {
            // Arrange
            var investmentA = new Investment("A", new List<double> { 0.1, 0.2 }, 0.05, 10);
            var investmentB = new Investment("B", new List<double> { 0.05, 0.1 }, 0.04, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            var strategy = new InvestmentStrategy(portfolio);

            // Act
            var weights = strategy.CalculateOptimalWeights(1000, 2000, 5);

            // Assert
            Assert.AreEqual(0.67, weights[0], 0.01);
            Assert.AreEqual(0.33, weights[1], 0.01);
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
            Assert.AreEqual(0.866, correlation, 0.01);
        }

    }
}