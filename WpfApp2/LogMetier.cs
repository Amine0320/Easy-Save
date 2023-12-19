using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Represents the business logic for handling application checks and activity.
    /// </summary>
    class LogMetier
    {
        /// <summary>
        /// Checks for the presence of specific applications in the given directory.
        /// </summary>
        /// <param name="sourceDirectory">The source directory to check for applications.</param>
        public static void CheckAppsInDirectory(string sourceDirectory) 
        {
            try
            {
                if (!string.IsNullOrEmpty(sourceDirectory))
                {
                    // Define the file path for the list of applications
                    string filePath = Path.Combine(GlobalVariables.Dir, "logicielmetier.txt");

                    // Read the list of applications from the file
                    HashSet<string> apps = new HashSet<string>(File.ReadAllLines(filePath));

                    // Get all directories in the source directory and its subdirectories
                    string[] allDirs = Directory.GetDirectories(sourceDirectory, "", SearchOption.AllDirectories);

                    // Iterate through each directory and check for each application
                    foreach (string dir in allDirs)
                    {
                        foreach (string app in apps)
                        {
                            // Combine the directory path and application name
                            string appPath = Path.Combine(dir, app);

                            // Check if the application directory or file exists
                            if (Directory.Exists(appPath) || File.Exists(appPath))
                            {
                                // Get the application name from the path
                                string appName = Path.GetFileName(appPath);

                                // Update the global variable to indicate the application is active
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

        /// <summary>
        /// Checks if the given application is currently active (running).
        /// </summary>
        /// <param name="appName">The name of the application.</param>
        /// <returns><c>true</c> if the application is active, otherwise <c>false</c>.</returns>
        public static bool IsActive(string appName)
        {
            // Get all processes with the given application name
            Process[] processes = Process.GetProcessesByName(appName);

            // Return true if there are processes, indicating the application is active
            return processes.Length > 0;
        }
    }
}
 