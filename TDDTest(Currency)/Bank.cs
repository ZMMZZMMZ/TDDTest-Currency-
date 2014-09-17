using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDTest_Currency_
{
    public class Bank
    {
        private Hashtable _rates = new Hashtable();

        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to))
            {
                return 1;
            }
            int rate = (int)_rates[new Pair(from, to)];
            return rate;
        }

        public void AddRate(string from, string to, int rate)
        {
            _rates.Add(new Pair(from, to), rate);
        }
    }
}