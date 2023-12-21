using System;
using System.Collections.Generic;
using System.IO;
using WpfApp2;

namespace WpfApp2
{
    // Class responsible for managing prioritized extensions during file backup
    public class ExtensionsPriori
    {
        // Method to copy prioritized files from source directory to target directory
        public static void ExtPriori(string RepSource, string RepCible) 
        {
            try
            {
                // Ensure that the target folder exists
                Directory.CreateDirectory(RepCible);

                // Read the list of prioritized extensions from the file
                string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
                List<string> prioritizedExtensions = new List<string>(File.ReadAllLines(filePath));

                // Get all files in the directory and its subdirectories
                string[] allFiles = Directory.GetFiles(RepSource, "*", SearchOption.AllDirectories);

                // Iterate through all files
                foreach (string file in allFiles)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(file);

                    // Check if the extension is in the list of prioritized extensions
                    if (prioritizedExtensions.Contains(fileExtension))
                    {
                        // Copy the file directly to the target directory with the original file name
                        string destinationFilePath = Path.Combine(RepCible, Path.GetFileName(file));
                        File.Copy(file, destinationFilePath, true);
                    }
                }

                Console.WriteLine("Prioritized extensions copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        } 

        // Method to check if a given file has a prioritized extension
        public static bool ExtPriorite(string file)
        {
            string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
            List<string> prioritizedExtensions = new List<string>(File.ReadAllLines(filePath));
            string fileExtension = Path.GetExtension(file);
            return prioritizedExtensions.Contains(fileExtension);
        }
    }
}
