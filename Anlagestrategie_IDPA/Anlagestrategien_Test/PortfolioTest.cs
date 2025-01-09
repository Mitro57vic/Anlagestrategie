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
    public class PortfolioTest
    {
        [TestMethod]
        public void TestPortfolioCreation()
        {
            var investmentA = new Investment("Test Investment A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var investmentB = new Investment("Test Investment B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            CollectionAssert.AreEqual(new List<Investment> { investmentA, investmentB }, portfolio.Investments);
        }

        [TestMethod]
        public void TestCalculateCorrelation()
        {
            var investmentA = new Investment("Test Investment A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var investmentB = new Investment("Test Investment B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            Assert.AreEqual(1.0, portfolio.CalculateCorrelation("Test Investment A", "Test Investment A"));
            Assert.AreEqual(1.0, portfolio.CalculateCorrelation("Test Investment B", "Test Investment B"));
            Assert.AreEqual(1.0, portfolio.CalculateCorrelation("Test Investment A", "Test Investment B"));
            
        }
       

        [TestMethod]
        public void TestCalculateGrowthStrategyWeights()
        {
            // Arrange
            var investmentA = new Investment("Test Investment A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var investmentB = new Investment("Test Investment B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Act
            var weights = portfolio.CalculateGrowthStrategyWeights();

            // Assert
            Assert.AreEqual(2, weights.Count, "Die Anzahl der Gewichtungen sollte der Anzahl der Investments entsprechen.");
            Assert.IsTrue(Math.Abs(weights.Sum() - 1) < 1e-10, "Die Gewichtungen sollten insgesamt 1 ergeben.");
            CollectionAssert.AreEqual(new List<double> { 0.4, 0.6 }, weights, "Die Growth-Strategie-Gewichtungen sind falsch.");
        }

        [TestMethod]
        public void TestCalculateValueStrategyWeights()
        {
             // Arrange
            var investmentA = new Investment("Test Investment A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var investmentB = new Investment("Test Investment B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });

            // Act
            var weights = portfolio.CalculateValueStrategyWeights();

            // Assert
            Assert.AreEqual(2, weights.Count, "Die Anzahl der Gewichtungen sollte der Anzahl der Investments entsprechen.");
            Assert.IsTrue(Math.Abs(weights.Sum() - 1) < 1e-10, "Die Gewichtungen sollten insgesamt 1 ergeben.");
            CollectionAssert.AreEqual(new List<double> { 0.6666666666666666, 0.3333333333333333 }, weights, "Die Value-Strategie-Gewichtungen sind falsch.");

        }

        [TestMethod]
        public void TestCalculatePortfolioVarianceWithExpectedValues()
        {
            // Arrange
            var investmentA = new Investment("Test Investment A", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            var investmentB = new Investment("Test Investment B", new List<double> { 0.2, 0.3, 0.4, 0.5, 0.6 }, 0.3, 20);
            var portfolio = new Portfolio(new List<Investment> { investmentA, investmentB });
            var weights = new List<double> { 0.5, 0.5 };

            // Manuelle Berechnung der Varianz
            double varianceA = investmentA.CalculateVariance();
            double varianceB = investmentB.CalculateVariance();
            double covarianceAB = portfolio.CalculateCorrelation("Test Investment A", "Test Investment B")
                                * Math.Sqrt(varianceA * varianceB);

            double expectedVariance = weights[0] * weights[0] * varianceA +
                                      weights[1] * weights[1] * varianceB +
                                      2 * weights[0] * weights[1] * covarianceAB;

            // Act
            var variance = portfolio.CalculatePortfolioVariance(weights);

            // Assert
            Assert.AreEqual(expectedVariance, variance, 1e-10, "Die Portfolio-Varianz ist falsch.");
        }






    }
}
