using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Configuration;
using SharedEntities;
using Serilog;
using Serilog.Core;

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
            _logger.Information("Marvin Initialised fetching results...");
        }
    }
}





