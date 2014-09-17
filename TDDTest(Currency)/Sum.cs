using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDTest_Currency_
{
    public class Sum : IExpression
    {
        public IExpression _augend;
        public IExpression _addend;

        public Sum(IExpression augend, IExpression addend)
        {
            this._augend = augend;
            this._addend = addend;
        }

        public Money Reduce(Bank bank, string to)
        {
            int amount = _augend.Reduce(bank, to)._amount + _addend.Reduce(bank, to)._amount;
            return new Money(amount, to);
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public IExpression Times(int multiplier)
        {
            return new Sum(_augend.Times(multiplier), _addend.Times(multiplier));
        }
    }
}