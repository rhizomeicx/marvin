using System;
using Serilog;

namespace Marvin_Ubuntu
{
    class Program
    {


        public static void Main(string[] args)
        {
            try
            {
                var logPath = "/var/tmp/log.txt";

                Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Information()
                                    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                                    .CreateLogger();

                Log.Information($"Starting PublishCron");
               

                Log.Information($"PublishCron finished. Closing & flushing logs...");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"GoogleCron app failed with error: {ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
