using System.IO;
using System.Reflection;
using System.ServiceProcess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedEntities;

namespace Marvin_Windows
{
    static class Program
    {
        static void Main()
        {
            //  Marvin.Main.Start("Windows");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MarvinService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
