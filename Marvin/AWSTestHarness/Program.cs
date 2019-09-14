using AWSMarvin_Lambda;
using System;

namespace AWSTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Function func = new Function();
            func.FunctionHandler(null, null);
        }
    }
}
