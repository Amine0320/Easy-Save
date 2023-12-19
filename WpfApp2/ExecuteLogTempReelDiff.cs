using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        // Obtenez l'instance de FileCopier (Singleton)
        FileCopier fileCopier = FileCopier.Instance;

        // Paramètres d'exemple 
        int ID = 1;
        string sourcePath = @"C:\Chemin\Source";
        string destinationPath = @"C:\Chemin\Destination";

        // Lancer la copie des fichiers
        fileCopier.CopyFiles(ID, sourcePath, destinationPath);

        Console.WriteLine("Processus de copie de fichiers terminé.");
    }
}

// Classe qui suit le modèle Singleton pour gérer la copie de fichiers
public sealed class FileCopier
{
    private static FileCopier instance;
    private static readonly object lockObject = new object();

    // Propriété pour obtenir l'instance du Singleton
    public static FileCopier Instance
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new FileCopier();
                }
                return instance;
            }
        }
    }

    // Méthode pour effectuer la copie des fichiers
    public void CopyFiles(int ID, string sourcePath, string destinationPath)
    {
        // Lire le fichier d'arrêt
        string stopFilePath = "C:\\LOGJ\\stop.txt";
        while (!File.Exists(stopFilePath) || File.ReadAllText(stopFilePath) != "go")
        {
            Console.WriteLine("En attente...");
            Thread.Sleep(1000); // Attendre une seconde avant de vérifier à nouveau
        }

        // Logique spécifique à C#
        int fichier = 0;
        long totalFilesSize = 0;

        // Obtenir la liste des fichiers à copier
        FileInfo[] filesToCopy = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

        // Première partie
        foreach (var file in filesToCopy)
        {
            string relativePath = file.FullName.Substring(sourcePath.Length);
            string newFilePath = Path.Combine(destinationPath, relativePath);

            if (File.Exists(newFilePath))
            {
                FileInfo existingFile = new FileInfo(newFilePath);

                if (existingFile.LastWriteTime == file.LastWriteTime)
                {
                    continue; // Le fichier existe déjà et n'a pas changé, passer au suivant
                }
                else
                {
                    fichier++;
                    totalFilesSize += existingFile.Length;
                }
            }
        }

        // Initialiser la barre de progression
        int progress = 0;
        int totalFiles = filesToCopy.Length;

        // Vérifier si le dossier de destination existe
        if (!Directory.Exists(destinationPath))
        {
            // S'il n'existe pas, le créer
            Directory.CreateDirectory(destinationPath);
            //Console.WriteLine("Dossier créé à {0}", destinationPath);
        }
        else
        {
            //Console.WriteLine("Le dossier existe déjà à {0}", destinationPath);
        }

        // Copier les fichiers avec une barre de progression
        foreach (var file in filesToCopy)
        {
            progress++;
            double progressPercentage = (double)progress / totalFiles * 100;

            // État actuel
            var currentState = new
            {
                IdEtaTemp = ID,
                NomETR = "Save " + ID,
                SourceFilePath = sourcePath,
                TargetFilePath = destinationPath,
                State = "Active",
                TotalFilesToCopy = filesToCopy.Length,
                TotalFilesSize = totalFilesSize,
                NbFilesLeftToDo = filesToCopy.Length - progress,
                Progression = progressPercentage
            };

            // Convertir l'état actuel en JSON
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(currentState);

            // Afficher la barre de progression
            //Console.WriteLine($"Progression : {progress}/{totalFiles} - {progressPercentage}%");

            // Copier le fichier
            string newFilePath = Path.Combine(destinationPath, file.FullName.Substring(sourcePath.Length));
            File.Copy(file.FullName, newFilePath, true);

            // Écrire l'état actuel dans le fichier JSON
            File.WriteAllText("C:\\LOGJ\\state.json", jsonString);
        }

        // État final
        var finalState = new
        {
            IdEtaTemp = ID,
            NomETR = "Save " + ID,
            SourceFilePath = "",
            TargetFilePath = "",
            State = "End",
            TotalFilesToCopy = 0,
            TotalFilesSize = 0,
            NbFilesLeftToDo = 0,
            Progression = 0
        };

        // Convertir l'état final en JSON
        string finalJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(finalState);

        // Écrire l'état final dans le fichier JSON2 (en mode Append)
        File.AppendAllText("C:\\LOGJ\\state2.json", finalJsonString);
    }

    // Constructeur privé pour éviter une instanciatisation directe
    private FileCopier() { }
}
 