using AWSMarvin_Lambda;
using System;

namespace AWSTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Daedric_Address = "cx58ca994194cf0c6a2a68b789d81c70484a5675b3",
            //Test Network_Url = "https://bicon.net.solidwallet.io/api/v3",
                       
            Function func = new Function();
            func.FunctionHandler(null, null);
        }
    }
}
