using System.Linq;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using WpfApp2;


// Classe qui suit le modèle Singleton pour gérer la copie de fichiers
public sealed class SaveDiff
{
    private static SaveDiff instance;
    private static readonly object lockObject = new object();

    // Propriété pour obtenir l'instance du Singleton
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

    // Méthode pour effectuer la copie des fichiers
    public long CopyFiles(int ID, string sourcePath, string destinationPath, SemaphoreSlim semaphore)
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

        //FileInfo[] filesToCopy = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories);

        // Première partie
        EtatTempsReel currentState = new EtatTempsReel();
        List<string> filesToCopy = Directory.GetFiles(sourcePath).ToList();
        foreach (var file in filesToCopy)
        {
            string fileName = Path.GetFileName(sourcePath);
            string targetFilePath = Path.Combine(destinationPath, fileName);


            if (File.Exists(targetFilePath))
            {
                DateTime sourceLastModified = File.GetLastWriteTime(file);
                DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);

                if (targetLastModified == sourceLastModified)
                {
                    filesToCopy.Remove(file); // Le fichier existe déjà et n'a pas changé, on ne le copie pas
                }
                else
                {
                    fichier++;
                    totalFilesSize += new FileInfo(file).Length;
                }
            }
            else 
            {
                fichier++;
                totalFilesSize += new FileInfo(file).Length;
            }
        }

        // Initialiser la barre de progression
        int progress = 0;
        int totalFiles = fichier;

        // Vérifier si le dossier de destination existe
        if (!Directory.Exists(destinationPath))
        {
            // S'il n'existe pas, le créer
            Directory.CreateDirectory(destinationPath);
            //Console.WriteLine("Dossier créé à {0}", destinationPath);
        }


        // Copier les fichiers avec une barre de progression
        foreach (var file in filesToCopy)
        {
            progress++;
            double progressPercentage = (double)progress / totalFiles * 100;

            // État actuel
            currentState.IdEtaTemp = ID;
            currentState.NomETR = "Save " + ID;
            currentState.SourceFilePath = sourcePath;
            currentState.TargetFilePath = destinationPath;
            currentState.State = "Active";
            currentState.TotalFilesToCopy = totalFiles;
            currentState.TotalFilesSize = (int)totalFilesSize;
            currentState.NbFilesLeftToDo = fichier - progress;
            currentState.Progression = (int)progressPercentage;

            // Convertir l'état actuel en JSON
            string jsonString = JsonConvert.SerializeObject(currentState, Formatting.Indented);

            string fileName = Path.GetFileName(file);
            string targetFilePath = Path.Combine(destinationPath, fileName);

            // Copier le fichier
            if (!File.Exists(targetFilePath))
            { File.Copy(file, targetFilePath); }

            // Écrire l'état actuel dans le fichier JSON
            File.WriteAllText("C:\\LOGJ\\state.json", jsonString);
        }

        // État final
        currentState.IdEtaTemp = ID;
        currentState.NomETR = "Save " + ID;
        currentState.SourceFilePath = "";
        currentState.TargetFilePath = "";
        currentState.State = "End";
        currentState.TotalFilesToCopy = 0;
        currentState.TotalFilesSize = 0;
        currentState.NbFilesLeftToDo = 0;
        currentState.Progression = 0;

        // Convertir l'état final en JSON
        string finalJsonString = JsonConvert.SerializeObject(currentState, Formatting.Indented);

        // Écrire l'état final dans le fichier JSON2 (en mode Append)
        File.AppendAllText("C:\\LOGJ\\state2.json", finalJsonString);

        return totalFilesSize;
    }

    // Constructeur privé pour éviter une instanciatisation directe
    private SaveDiff() { }
}
 