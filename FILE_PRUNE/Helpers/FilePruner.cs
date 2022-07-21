using FILE_PRUNE.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static FILE_PRUNE.Helpers.SupportedApps;

namespace FILE_PRUNE.Helpers
{
    public static class FilePruner
    {
        private static List<string> NoiseLines = new List<string>()
        {
            "Flyweight Worker"
        };

        public static bool FilePrune(SettingsConfig settingsConfig)
        {
            bool result = false;

            try
            {
                string exeLocation = Assembly.GetExecutingAssembly().GetName().CodeBase;
                UriBuilder uri = new UriBuilder(exeLocation);
                string targetDir = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path)), "in");
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                string fileInPath = Path.Combine(targetDir, settingsConfig.Input);

                if (File.Exists(fileInPath))
                {
                    List<string> appsToAdd = GetApplicationsToAdd(settingsConfig.AppsToAdd);

                    string[] logFile = File.ReadAllLines(fileInPath);
                    List<string> logList = new List<string>(logFile);

                    // target file
                    string fileOutPath = Path.Combine(targetDir, settingsConfig.Output);

                    // Prune out unwanted lines
                    using (StreamWriter wr = new StreamWriter(fileOutPath, false))
                    {
                        wr.AutoFlush = true;
                        int linesProcessed = 0;
                        int linesAccepted = 0;
                        foreach (string line in logFile)
                        {
                            if ((appsToAdd.Any(x => line.Contains(x, StringComparison.OrdinalIgnoreCase)) ||
                                line.Contains(settingsConfig.GUID, StringComparison.OrdinalIgnoreCase)) &&
                                !NoiseLines.Any(x => line.Contains(x, StringComparison.OrdinalIgnoreCase)))
                            {
                                wr.WriteLine(line);
                                linesAccepted ++;
                            }
                            linesProcessed++;
                        }
                        Console.WriteLine($"LINES PROCESSED={linesProcessed} - ACCEPTED={linesAccepted}");
                    }

                    result = true;
                }
                else
                {
                    Console.WriteLine($"FILE [{fileInPath}] - not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in fileReader={ex.Message}");
            }
            return result;
        }

        public static List<string> GetApplicationsToAdd(List<string> applications)
        {
            List<string> apps = new List<string>();

            foreach(string item in applications)
            {
                if (Enum.TryParse<SupportedApplications>(item, true, out SupportedApplications value))
                {
                    string appValue = value.GetStringValue();
                    apps.Add(appValue);
                }
            }

            return apps;
        }
    }
}
