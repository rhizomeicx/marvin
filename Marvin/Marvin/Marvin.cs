using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Configuration;
using SharedEntities;
using Serilog;
using Serilog.Core;
using SharedEntities;
using IconSDK;
using IconSDK.RPCs;

namespace Marvin
{
    public class Marvin
    {
        private System.Timers.Timer timer;
        private string FilePath;
        private AppSettings _appsetting;
        private Logger _logger;

        public Marvin(AppSettings appSetting, Logger logger)
        {
            _appsetting = appSetting;
            _logger = logger;
        }

        public void Run()
        {
            var getLastBlock = GetLastBlock.Create(Consts.ApiUrl.TestNet);
            var lastBlock = getLastBlock().Result;

            var getBlockByHeight = GetBlockByHeight.Create(Consts.ApiUrl.TestNet);
            var blockByHeight =  getBlockByHeight(lastBlock.Height.Value).Result;

            _logger.Information("Marvin Initialised fetching results...");

            _logger.Information($"Lastest Block Hash:  {blockByHeight.Hash}");
            _logger.Information($"Lastest Block Height:  {blockByHeight.Height}");
            _logger.Information($"Lastest Block Signature:  {blockByHeight.Signature}");
        }
    }
}





