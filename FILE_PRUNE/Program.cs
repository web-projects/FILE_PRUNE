using FILE_PRUNE.Configuration;
using FILE_PRUNE.Extensions;
using FILE_PRUNE.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static FILE_PRUNE.Configuration.SettingsConfigurationConstants;

namespace FILE_SORT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"\r\n==========================================================================================");
            Console.WriteLine($"{Assembly.GetEntryAssembly().GetName().Name} - Version {Assembly.GetEntryAssembly().GetName().Version}");
            Console.WriteLine($"==========================================================================================\r\n");

            IConfiguration configuration = ConfigurationLoad();

            FileGroup fileGroup = GetConfigurationSettings(configuration);
            FilePruner.FilePrune(fileGroup);
        }

        static IConfiguration ConfigurationLoad()
        {
            // Get appsettings.json config.
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }

        static FileGroup GetConfigurationSettings(IConfiguration configuration)
        {
            return configuration.GetSection(FileGroupKey).GetByEnv<FileGroup>();
        }
    }
}
