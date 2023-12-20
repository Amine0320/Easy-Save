using System;
using System.Data;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace WpfApp2
{

    // Classe qui suit le mod�le Singleton pour g�rer la copie de fichiers
    public sealed class CompleteSave
    {
        private static CompleteSave instance;
        private static readonly object lockObject = new object();

        // Propri�t� pour obtenir l'instance du Singleton
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

        // M�thode pour effectuer la copie des fichiers
        public async Task CopyFiles(int ID, string sourcePath, string destinationPath, SemaphoreSlim semaphore)
        {
            //Lire la limite des fichiesr impos�e par l'utilisateur
            string filePath = GlobalVariables.Dir + "limite.txt";
            int limite = Convert.ToInt32(File.ReadAllText(filePath));

            // Lire le fichier d'arr�t
            string stopFilePath = "C:\\LOGJ\\stop.txt";

            int fichier = 0;
            long totalFilesSize = 0;

            // Obtenir la liste des fichiers � copier
            string[] sourceFiles = Directory.GetFiles(sourcePath);
            //FileInfo[] filesToCopy = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

            // Initialiser la barre de progression
            int progress = 0;
            int totalFiles = sourceFiles.Length;

            // V�rifier si le dossier de destination existe
            if (!Directory.Exists(destinationPath))
            {
                // S'il n'existe pas, le cr�er
                Directory.CreateDirectory(destinationPath);
                //Console.WriteLine("Dossier cr�� � {0}", destinationPath);
            }

            // Copier les fichiers avec une barre de progression
            EtatTempsReel currentState = new EtatTempsReel();
            List<string> filesToCopy = new List<string>();
            foreach (var File in sourceFiles.Where(file => ExtensionsPriori.ExtPriorite(file)))
            {
                filesToCopy.Add(File);
            }
            foreach (var File in sourceFiles.Where(file => !ExtensionsPriori.ExtPriorite(file)))
            {
                filesToCopy.Add(File);
            }
            while (filesToCopy.Count > 0)
            {
                if (File.Exists(stopFilePath) && File.ReadAllText(stopFilePath).Equals("go"))
                {
                    List<string> files = new List<string>(filesToCopy);
                    foreach (string file in files)
                    {
                        progress++;
                        double progressPercentage = (double)progress / totalFiles * 100;

                        // �tat actuel
                        currentState.IdEtaTemp = ID;
                        currentState.NomETR = "Save " + ID;
                        currentState.SourceFilePath = sourcePath;
                        currentState.TargetFilePath = destinationPath;
                        currentState.State = "Active";
                        currentState.TotalFilesToCopy = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Length;
                        currentState.TotalFilesSize = (int)new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Sum(f => f.Length);
                        currentState.NbFilesLeftToDo = (int)new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories).Length - new DirectoryInfo(destinationPath).GetFiles("*.*", SearchOption.AllDirectories).Length;
                        currentState.Progression = (int)progressPercentage;


                        // Convertir l'�tat actuel en JSON
                        string jsonString = JsonConvert.SerializeObject(currentState, Formatting.Indented);

                        // Copier le fichier
                        string fileName = Path.GetFileName(file);
                        string targetFilePath = Path.Combine(destinationPath, fileName);
                        long fileSize = new FileInfo(file).Length;

                        if (fileSize > limite * 1024)
                        {
                            //Ne permet pas de copier 2 fichiers de taille de plus de la limite en m�me temps
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

                        // �crire l'�tat actuel dans le fichier JSON
                        File.WriteAllText(@"C:\LOGJ\state.json", jsonString);
                    }

                }
                else { await Task.Delay(1000); }
            }
            // �tat final
            currentState.IdEtaTemp = ID;
            currentState.NomETR = "Save " + ID;
            currentState.SourceFilePath = "";
            currentState.TargetFilePath = "";
            currentState.State = "End";
            currentState.TotalFilesToCopy = 0;
            currentState.TotalFilesSize = 0;
            currentState.NbFilesLeftToDo = 0;
            currentState.Progression = 0;

            // Convertir l'�tat final en JSON
            string finalJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(currentState, Newtonsoft.Json.Formatting.Indented);

            // �crire l'�tat final dans le fichier JSON2 (en mode Append)
            File.AppendAllText("C:\\LOGJ\\state2.json", finalJsonString);
        }

        // Constructeur priv� pour �viter une instanciatisation directe
        private CompleteSave() { }
    }
}