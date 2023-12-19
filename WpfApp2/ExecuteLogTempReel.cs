using System;
using System.IO;
using System.Threading;

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
        public void CopyFiles(int ID, string sourcePath, string destinationPath)
        {
            // Lire le fichier d'arr�t
            string stopFilePath = "C:\\LOGJ\\stop.txt";
            /*
            while (!File.Exists(stopFilePath) || File.ReadAllText(stopFilePath) != "go")
            {
                Console.WriteLine("En attente...");
                Thread.Sleep(10000); // Attendre une seconde avant de v�rifier � nouveau
            }
            */

            // Logique sp�cifique � C#
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
            foreach (var file in sourceFiles)
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
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(currentState, Newtonsoft.Json.Formatting.Indented) + "/n";


                // Copier le fichier
                string fileName = Path.GetFileName(file);
                string targetFilePath = Path.Combine(destinationPath, fileName);
                File.Copy(file, targetFilePath);


                // �crire l'�tat actuel dans le fichier JSON
                File.WriteAllText("C:\\LOGJ\\state.json", jsonString);

                
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