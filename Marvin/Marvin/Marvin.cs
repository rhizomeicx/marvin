using IconSDK;
using IconSDK.Account;
using IconSDK.Blockchain;
using IconSDK.Crypto;
using IconSDK.Helpers;
using IconSDK.RPCs;
using IconSDK.Types;
using Marvin.bots;
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
        public Marvin(AppSettings appSetting, Logger logger)
        {
            _appsetting = appSetting;
            _logger = logger;
        }

        public void Run(bool testNet = false)
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
            var getTransactionByHash = new GetTransactionByHash(_appsetting.Network_Url);
            var postResult = getTransactionByHash.Invoke(result).Result;
            _logger.Information("Completed");
        }

        private double GetMeanPrices()
        {
            List<double> price = new List<double> { PriceFrom.Binance("ICXUSDT"),
                                                PriceFrom.CoinMarketCap("icon"),
                                                PriceFrom.Coingecko("icon"),
                                                PriceFrom.Velic()}.Where(x => x != 0).ToList();

            return Math.Round(price.Select(d => d / price.Count).Sum());
        }

        private Transaction CreateTransaction(double price, bool testNet = false)
        {
            var builder = new CallTransactionBuilder
            {
                NID = !_appsetting.testTransactions ? 1 : 3, //Pass 3 for testNet
                PrivateKey = GetPrivateKey(),
                To = _appsetting.Daedric_Address,
                StepLimit = NumericsHelper.ICX2Loop("0.000000001"),
                Method = "post"
            };
            //For a general purpose solution to remove scientific notation on a double to string value you need to preserve 339 places
            //https://stackoverflow.com/questions/1546113/double-to-string-conversion-without-scientific-notation
            builder.Params["value"] = price.ToString("0." + new string('#', 339));

            var tax = builder.Build();

           return builder.Build();
        }

        private Hash32 UpdateDaedric(Transaction tx)
        {
            var transactionRequest = SendTransaction.Create(_appsetting.Network_Url);
            var sendResponse = transactionRequest.Invoke(tx).Result;

            return sendResponse;
        }

        private string GetPrivateKey()
        {
            if (!string.IsNullOrEmpty(_appsetting.Keystore))
            {
                try
                {
                    var keyStore = KeyStore.Load(_password, _appsetting.Keystore);
                    return keyStore.PrivateKey.ToString();
                }
                catch (Exception e)
                {
                    _logger.Error($"Marvin was unable to open the KeyStore, did you enter the correct password? \n {e}");
                    throw new Exception(e.ToString());
                }
            }
            else
            {
                return _appsetting.PrivateKey;
            }
        }
    }
}





