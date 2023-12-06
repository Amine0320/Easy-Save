using System;
using System.Collections.Generic;
using System.IO;

class LogMetier
{
    public static bool CheckAppsInDirectory(string sourceDirectory)
    {
        try
        {
            // Read the list of apps from the file
            string DossierPro = Directory.GetCurrentDirectory();
            string[] ListDossier = DossierPro.Split(@"\");
            string Dir = "";
            for (int k = 0; k < ListDossier.Length - 3; k++)
            {
                Dir += ListDossier[k] + @"\";
            }
            string filePath = Dir + "logicielmetier.txt";
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
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return false;
    }

}
