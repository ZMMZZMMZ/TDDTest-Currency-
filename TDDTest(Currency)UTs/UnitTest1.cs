using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDDTest_Currency_;
using TDDTest_Currency_.Fakes;

namespace TDDTest_Currency_UTs
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void MultiplicationTest()
        {
            Money five = Money.Dollar(5);
            Assert.AreEqual(Money.Dollar(10), five.Times(2));
            Assert.AreEqual(Money.Dollar(15), five.Times(3));
        }

        [TestMethod]
        public void EqualityTest()
        {
            Assert.IsTrue(Money.Dollar(5).Equals(Money.Dollar(5)));
            Assert.IsFalse(Money.Dollar(5).Equals(Money.Dollar(6)));
            Assert.IsFalse(Money.Franc(5).Equals(Money.Dollar(5)));
        }

        [TestMethod]
        public void CurrencyTest()
        {
            Assert.AreEqual("USD", Money.Dollar(1).GetCurrency());
            Assert.AreEqual("CHF", Money.Franc(1).GetCurrency());
        }

        [TestMethod]
        public void SimpleAdditionTest()
        {
            Money five = Money.Dollar(5);
            IExpression sum = five.Plus(five);
            Bank bank = new Bank();
            Money reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(10), reduced);
        }

        [TestMethod]
        public void PlusReturnsSumTest()
        {
            Money five = Money.Dollar(5);
            IExpression result = five.Plus(five);
            Sum sum = (Sum)result;
            Assert.AreEqual(five, sum._augend);
        }

        [TestMethod]
        public void ReduceSumTest()
        {
            IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Bank bank = new Bank();
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(7), result);
        }

        [TestMethod]
        public void ReduceMoneyTest()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.Dollar(1), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [TestMethod]
        public void ReduceMoneyDifferentCurrencyTest()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(Money.Franc(2), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [TestMethod]
        public void IdentityRateTest()
        {
            Assert.AreEqual(1, new Bank().Rate("USD", "USD"));
        }

        [TestMethod]
        public void MixedAdditionTest()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrans = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(fiveBucks.Plus(tenFrans), "USD");
            Assert.AreEqual(Money.Dollar(10), result);
        }

        [TestMethod]
        public void SumPlusMoneyTest()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrans = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            IExpression sum = new Sum(fiveBucks, tenFrans).Plus(fiveBucks);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(15), result);
        }

        [TestMethod]
        public void SumTimesTest()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrans = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            IExpression sum = new Sum(fiveBucks, tenFrans).Times(2);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(20), result);
        }

        [TestMethod]
        public void PlusSameCurrencyReturnsMoneyTest()
        {
            IExpression sum = Money.Dollar(1).Plus(Money.Dollar(1));
            Assert.IsFalse(sum is Money);
        }

        [TestMethod]
        public void NewTest()
        {
            // this is a new test method 
        }
    }
}