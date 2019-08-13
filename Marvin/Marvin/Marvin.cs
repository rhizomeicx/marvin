using IconSDK;
using IconSDK.Account;
using IconSDK.Blockchain;
using IconSDK.Crypto;
using IconSDK.Helpers;
using IconSDK.RPCs;
using IconSDK.Types;
using Marvin.bots;
using Newtonsoft.Json;
using Serilog.Core;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Marvin
{
    public class Marvin
    {
        private System.Timers.Timer timer;
        private string FilePath;
        private AppSettings _appsetting;
        private Logger _logger;
        private string _password;

        public Marvin(AppSettings appSetting, Logger logger, string pass)
        {
            _appsetting = appSetting;
            _logger = logger;
            _password = pass;
        }

        public void Run()
        {
            var meanPrices = GetMeanPrices();
            var tx = CreateTransaction(meanPrices);
            var result = UpdateDaedric(tx);
            var getTransactionByHash = new GetTransactionByHash(_appsetting.Yeouido_url);
            var postResult = getTransactionByHash.Invoke(result).Result;
            _logger.Information(JsonConvert.SerializeObject(postResult));
        }

        private long GetMeanPrices()
        {
            List<long> price = new List<long> { PriceFrom.Binance("ICXUSDT"),
                                                PriceFrom.CoinMarketCap("icon"),
                                                PriceFrom.Coingecko("icon"),
                                                PriceFrom.Velic()}.Where(x => x != 0).ToList();

            return Convert.ToInt64(price.Select(d => (double)d / price.Count).Sum());
        }

        private Transaction CreateTransaction(long price)
        {
            var builder = new CallTransactionBuilder
            {
                NID = 3,
                PrivateKey = GetPrivateKey(),
                To = _appsetting.Yeouido_Daedric_Address,
                StepLimit = NumericsHelper.ICX2Loop("0.000000001"),
                Method = "post"
            };
            builder.Params["value"] = price.ToString();

            var tax = builder.Build();



           return builder.Build();
        }

        private Hash32 UpdateDaedric(Transaction tx)
        {
            var transactionRequest = SendTransaction.Create(_appsetting.Yeouido_url);
            var sendResponse = transactionRequest.Invoke(tx).Result;

            return sendResponse;
        }

        private string GetPrivateKey()
        {
            var keyStore = KeyStore.Load(_password, _appsetting.Yeouido_Keystore);
            return keyStore.PrivateKey.ToString();
        }
    }
}





