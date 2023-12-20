using System;
using System.Data;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace WpfApp2
{

    // Class that follows the Singleton Design Pattern to copy files
    public sealed class CompleteSave
    {
        private static CompleteSave instance;
        private static readonly object lockObject = new object();

        // Property to get the Singleton instance
        public static CompleteSave Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new CompleteSave();
                    }
                    return instance;
                }
            }
        }

        // Method to copy the files
        public async Task CopyFiles(int ID, string sourcePath, string destinationPath, SemaphoreSlim semaphore)
        {
            // Get the file limit dictated by the user
            string filePath = GlobalVariables.Dir + "limite.txt";
            int limite = Convert.ToInt32(File.ReadAllText(filePath));

            // Initiation
            int fichier = 0;
            long totalFilesSize = 0;

            // Get an array of all the files
            string[] sourceFiles = Directory.GetFiles(sourcePath);

            // Initialize progress
            int progress = 0;
            int totalFiles = sourceFiles.Length;

            // Check if directory exists
            if (!Directory.Exists(destinationPath))
            {
                // If not, create it
                Directory.CreateDirectory(destinationPath);
                //Console.WriteLine("Dossier créé à {0}", destinationPath);
            }

            // Create State Object
            EtatTempsReel currentState = new EtatTempsReel();

            // Get the list of files to copy
            List<string> filesToCopy = new List<string>();
            //Extensions to prioritize
            foreach (var File in sourceFiles.Where(file => ExtensionsPriori.ExtPriorite(file)))
            {
                filesToCopy.Add(File);
            }
            //Extensions not prioritized
            foreach (var File in sourceFiles.Where(file => !ExtensionsPriori.ExtPriorite(file)))
            {
                filesToCopy.Add(File);
            }

            // Copy the files while updating the progress

            // If there are files to copy
            while (filesToCopy.Count > 0)
            {
                // Check if Play condition is valid
                if (GlobalVariables.Play)
                {
                    // Copy each file while updating the state
                    List<string> files = new List<string>(filesToCopy);
                    foreach (string file in files)
                    {
                        progress++;
                        double progressPercentage = (double)progress / totalFiles * 100;

                        // Current State 
                        currentState.IdEtaTemp = ID;
                        currentState.NomETR = "Save " + ID;
                        currentState.SourceFilePath = sourcePath;
                        currentState.TargetFilePath = destinationPath;
                        currentState.State = "Active";
                        currentState.TotalFilesToCopy = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Length;
                        currentState.TotalFilesSize = (int)new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Sum(f => f.Length);
                        currentState.NbFilesLeftToDo = (int)new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Length - new DirectoryInfo(destinationPath).GetFiles("*.*", SearchOption.AllDirectories).Length;
                        currentState.Progression = (int)progressPercentage;


                        // Convert the current State to JSON
                        string jsonString = JsonConvert.SerializeObject(currentState, Formatting.Indented);

                        // Copy the file
                        string fileName = Path.GetFileName(file);
                        string targetFilePath = Path.Combine(destinationPath, fileName);
                        long fileSize = new FileInfo(file).Length;

                        if (fileSize > limite * 1024)
                        {
                            //Doesn't allow two files more than the limit to be copied at the same time
                            semaphore.Wait();
                            try
                            {
                                if (!File.Exists(targetFilePath))
                                { File.Copy(file, targetFilePath); }
                            }
                            finally { semaphore.Release(); }
                        }
                        else
                        {
                            if (!File.Exists(targetFilePath))
                            { File.Copy(file, targetFilePath); }
                        }

                        filesToCopy.Remove(file);

                        // Write current state to file
                        File.WriteAllText(@"C:\LOGJ\state.json", jsonString);
                    }

                }
                else { await Task.Delay(1000); }
            }
            // Final State
            currentState.IdEtaTemp = ID;
            currentState.NomETR = "Save " + ID;
            currentState.SourceFilePath = "";
            currentState.TargetFilePath = "";
            currentState.State = "End";
            currentState.TotalFilesToCopy = 0;
            currentState.TotalFilesSize = 0;
            currentState.NbFilesLeftToDo = 0;
            currentState.Progression = 0;

            // Convert final state to JSON
            string finalJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(currentState, Newtonsoft.Json.Formatting.Indented);

            // Write final state to file without overwritting
            File.AppendAllText("C:\\LOGJ\\state2.json", finalJsonString);
        }

        // Private Constructor to avoid direct instanciation
        private CompleteSave() { }
    }
}