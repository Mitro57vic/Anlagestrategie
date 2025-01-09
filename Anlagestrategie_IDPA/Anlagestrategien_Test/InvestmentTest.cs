using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anlagestrategie_IDPA;
using System.Collections.Generic;

namespace Anlagestrategien_Test
{
    [TestClass]
    public class InvestmentUnitTests
    {
        [TestMethod]
        public void TestInvestmentCreation()
        {
            var investment = new Investment("Test Investment", new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, 0.2, 10);
            Assert.AreEqual("Test Investment", investment.Name);
            Assert.AreEqual(0.2, investment.GrowthRate);
            Assert.AreEqual(10, investment.Valuation);
            CollectionAssert.AreEqual(new List<double> { 0.1, 0.2, 0.3, 0.4, 0.5 }, investment.HistoricalReturns);
        }

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


    }
}
