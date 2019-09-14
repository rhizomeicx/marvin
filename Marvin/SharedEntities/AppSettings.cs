using System;

namespace SharedEntities
{
    public class AppSettings
    {
        public string LogPath { get; set; }

        public bool testTransactions { get; set; }

        //You only have to supply either a keystore OR a PrivateKey
        public string Keystore { get; set; }
        public string PrivateKey { get; set; }

        public string Daedric_Address { get; set; }
        public string Network_Url { get; set; }
        public double Price_Increment { get; set; }


    }
}
