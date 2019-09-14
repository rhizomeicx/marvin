using Newtonsoft.Json;
using Serilog;
using SharedEntities;
using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Marvin_Windows
{
    internal class MarvinService : ServiceBase
    {
        private System.Timers.Timer timer;
        private Marvin.Marvin _marvin;
  
        public MarvinService()
        {
            ServiceName = "Marvin";
        }

        protected override void OnStart(string[] args)
        {
            var appSettings = GetAppSettings();
            var logger = new LoggerConfiguration()
                              .MinimumLevel.Information()
                              .WriteTo.File(appSettings.LogPath, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                              .CreateLogger();

            _marvin = new Marvin.Marvin(appSettings, logger, args[0]);
            //adding this within a Task thread or Windows Services keeps Marvin stuck in 'Starting'
            Task.Run(() => Initialise(appSettings.Price_Increment));
        }

        private void Initialise(double priceIncrement)
        {
            timer = new System.Timers.Timer(priceIncrement); //refresh transaction data every 10 minutes
            timer.Elapsed += Timer_Elapsed;

            timer.Start();

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                _marvin.Run();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                timer.Start();
            }
        }


        protected override void OnStop()
        {
            _marvin = null;
        }
        AppSettings GetAppSettings()
        {
            var appSettingsFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\appsettings.json";

            if (!File.Exists(appSettingsFilePath))
            {
                throw new FileNotFoundException("appsettings.json was not found or is not accessible");
            }

            var appSettingsContent = File.ReadAllText(appSettingsFilePath);
            if (string.IsNullOrEmpty(appSettingsContent))
                throw new Exception("Appsettings file is empty");

            return JsonConvert.DeserializeObject<AppSettings>(appSettingsContent);
        }
    }
}