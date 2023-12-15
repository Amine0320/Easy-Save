using System;
using System.IO;
using System.Diagnostics;

namespace WpfApp2
{
    class LogMetier
    {
        public static void CheckAppsInDirectory(string sourceDirectory)
        {
            try
            {
                // Read the list of apps from the file
                string filePath = GlobalVariables.Dir + "logicielmetier.txt";
                List<string> apps = new List<string>(File.ReadAllLines(filePath));
                string[] alldirs = Directory.GetDirectories(sourceDirectory, "", SearchOption.AllDirectories);
                string[] dirs = new string[alldirs.Length + 1];
                Array.Copy(alldirs, dirs, alldirs.Length);
                dirs[alldirs.Length] = sourceDirectory;
                foreach (string dir in dirs)
                {
                    foreach (string app in apps)
                    {
                        string appPath = Path.Combine(dir, app);

                        if (Directory.Exists(appPath) || File.Exists(appPath))
                        {
                            string appName = Path.GetFileName(appPath);
                            while (true) 
                            {
                                GlobalVariables.Active = IsActive(appName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static bool IsActive(string appName) 
        {
            Process[] processes = Process.GetProcessesByName(appName);
            return processes.Length > 0;
        }

    }
}