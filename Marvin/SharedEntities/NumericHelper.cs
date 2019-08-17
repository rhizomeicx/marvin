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

            BigInteger numericBase = BigInteger.Parse("10");

            BigInteger result = icxloop / BigInteger.Pow(numericBase, 18);

            return result.ToString();
        }
    }
}
