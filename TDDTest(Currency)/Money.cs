using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDTest_Currency_
{
    public class Money : IExpression
    {
        internal int _amount;
        private string _currency;

        public Money(int amount, string currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public override bool Equals(object obj)
        {
            Money money = (Money)obj;
            return _amount == money._amount && GetCurrency().Equals(money.GetCurrency());
        }

        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public string GetCurrency()
        {
            return _currency;
        }

        public IExpression Times(int multiplier)
        {
            return new Money(_amount * multiplier, _currency);
        }

        public override string ToString()
        {
            return _amount + " " + _currency;
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(_currency, to);
            return new Money(_amount / rate, to);
        }
    }
}