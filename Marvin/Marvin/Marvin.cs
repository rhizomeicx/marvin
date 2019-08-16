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
            _logger.Information("------------------------------");
            _logger.Information("Starting Marvin...");
            _logger.Information("Getting current price...");
            var meanPrices = GetMeanPrices();
            _logger.Information($"ICXUSD current price: {meanPrices}");
            var tx = CreateTransaction(meanPrices);
            _logger.Information($"Updating Daedric...");
            var result = UpdateDaedric(tx);
            _logger.Information($"Deadric updated tx hash: {result}");
            var getTransactionByHash = new GetTransactionByHash(_appsetting.Yeouido_url);
            var postResult = getTransactionByHash.Invoke(result).Result;
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
            try
            {
                var keyStore = KeyStore.Load(_password, _appsetting.Yeouido_Keystore);
                return keyStore.PrivateKey.ToString();
            }
            catch(Exception e)
            {
                _logger.Error($"Marvin was unable to open the KeyStore, did you enter the correct password? \n {e}");
                throw new Exception(e.ToString());
            }
        }
    }
}





