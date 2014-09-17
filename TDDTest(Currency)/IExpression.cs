using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDTest_Currency_
{
    public interface IExpression
    {
        Money Reduce(Bank bank, string to);

        IExpression Plus(IExpression addend);

        IExpression Times(int multiplier);
    }
}