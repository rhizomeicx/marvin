using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SharedEntities
{
    public static class NumericHelper
    {
        public static string Loop2ICX(string bigInt, bool trimTrailing = true)
        {
            BigInteger icxloop = BigInteger.Parse(bigInt);

            double result = (double)icxloop;

            double icx = result / Math.Pow(10, 18);

            return icx.ToString();
        }
    }
}
