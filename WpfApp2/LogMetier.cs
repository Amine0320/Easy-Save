using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WpfApp2
{
    class LogMetier
    {
        public static void CheckAppsInDirectory(string sourceDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(sourceDirectory))
                {
                    string filePath = Path.Combine(GlobalVariables.Dir, "logicielmetier.txt");
                    HashSet<string> apps = new HashSet<string>(File.ReadAllLines(filePath));
                    string[] alldirs = Directory.GetDirectories(sourceDirectory, "", SearchOption.AllDirectories);

                    foreach (string dir in alldirs)
                    {
                        foreach (string app in apps)
                        {
                            string appPath = Path.Combine(dir, app);

                            if (Directory.Exists(appPath) || File.Exists(appPath))
                            {
                                string appName = Path.GetFileName(appPath);
                                GlobalVariables.Active = IsActive(appName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static bool IsActive(string appName)
        {
            Process[] processes = Process.GetProcessesByName(appName);
            return processes.Length > 0;
        }


    }
}