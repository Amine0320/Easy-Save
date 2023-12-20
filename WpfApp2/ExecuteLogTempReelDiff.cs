using System.Linq;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.ComponentModel;

namespace WpfApp2
{
    // Class that follows the Singleton Design Pattern to copy files
    public sealed class SaveDiff
    {
        private static SaveDiff instance;
        private static readonly object lockObject = new object();

        // Property to get the Singleton instance
        public static SaveDiff Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SaveDiff();
                    }
                    return instance;
                }
            }
        }

        // Method to copy the files
        public async void CopyFiles(int ID, string sourcePath, string destinationPath, SemaphoreSlim semaphore)
        {

            // Get the file limit dictated by the user
            string filePath = GlobalVariables.Dir + "limite.txt";
            int limite = Convert.ToInt32(File.ReadAllText(filePath));

            // Initiation
            int fichier = 0;
            long totalFilesSize = 0;

            // Get an array of all the files
            string[] sourceFiles = Directory.GetFiles(sourcePath);

            // Create State Object
            EtatTempsReel currentState = new EtatTempsReel();

            // Get the list of files to copy
            List<string> filesToCopy = new List<string>();

            //Extensions to prioritize
            foreach (var file in sourceFiles.Where(file => ExtensionsPriori.ExtPriorite(file)))
            {
                string fileName = Path.GetFileName(file);
                string targetFilePath = Path.Combine(destinationPath, fileName);


                if (File.Exists(targetFilePath))
                {
                    DateTime sourceLastModified = File.GetLastWriteTime(file);
                    DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);

                    if (targetLastModified == sourceLastModified)
                    {
                        continue; // The file already exists, do not copy
                    }
                    else
                    {
                        // Add the file to be copied if modified
                        filesToCopy.Add(file);
                        fichier++;
                        totalFilesSize += new FileInfo(file).Length;
                    }
                }
                else
                {
                    // The file doesn't exist, to be copied
                    filesToCopy.Add(file);
                    fichier++;
                    totalFilesSize += new FileInfo(file).Length;
                }
            }

            //Extensions not prioritized
            foreach (var file in sourceFiles.Where(file => !ExtensionsPriori.ExtPriorite(file)))
            {
                string fileName = Path.GetFileName(sourcePath);
                string targetFilePath = Path.Combine(destinationPath, fileName);

                // Same Logic
                if (File.Exists(targetFilePath))
                {
                    DateTime sourceLastModified = File.GetLastWriteTime(file);
                    DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);

                    if (targetLastModified == sourceLastModified)
                    {
                        continue;
                    }
                    else
                    {
                        filesToCopy.Add(file);
                        fichier++;
                        totalFilesSize += new FileInfo(file).Length;
                    }
                }
                else
                {
                    filesToCopy.Add(file);
                    fichier++;
                    totalFilesSize += new FileInfo(file).Length;
                }
            }

            // Initialize progress
            int progress = 0;
            int totalFiles = fichier;

            //Get the total file size for the logs
            GlobalVariables.FileSize = (int)totalFilesSize;

            // Check if directory exists
            if (!Directory.Exists(destinationPath))
            {
                // If not, create it
                Directory.CreateDirectory(destinationPath);
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
                    foreach (var file in files)
                    {
                        progress++;
                        double progressPercentage = (double)progress / totalFiles * 100;

                        // Current State 
                        currentState.IdEtaTemp = ID;
                        currentState.NomETR = "Save " + ID;
                        currentState.SourceFilePath = sourcePath;
                        currentState.TargetFilePath = destinationPath;
                        currentState.State = "Active";
                        currentState.TotalFilesToCopy = totalFiles;
                        currentState.TotalFilesSize = (int)totalFilesSize;
                        currentState.NbFilesLeftToDo = fichier - progress;
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
                        File.WriteAllText("C:\\LOGJ\\state.json", jsonString);
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
            string finalJsonString = JsonConvert.SerializeObject(currentState, Formatting.Indented);

            // Write final state to file without overwritting
            File.AppendAllText("C:\\LOGJ\\state2.json", finalJsonString);

        }

        // Private Constructor to avoid direct instanciation
        private SaveDiff() { }
    }
}