using System;
using System.Collections.Generic;
using System.IO;
using WpfApp2;

namespace WpfApp2
{
    public class ExtensionsPriori
    {
        public static void ExtPriori(string RepSource, string RepCible)
        {
            try
            {
                // Assurez-vous que le dossier cible existe
                Directory.CreateDirectory(RepCible);

                // Lire la liste des extensions prioritaires à partir du fichier
                string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
                List<string> prioritizedExtensions = new List<string>(File.ReadAllLines(filePath));

                // Obtenir tous les fichiers dans le répertoire et ses sous-répertoires
                string[] allFiles = Directory.GetFiles(RepSource, "*", SearchOption.AllDirectories);

                // Parcourir tous les fichiers
                foreach (string file in allFiles)
                {
                    // Obtenir l'extension du fichier
                    string fileExtension = Path.GetExtension(file);

                    // Vérifier si l'extension est dans la liste des extensions prioritaires
                    if (prioritizedExtensions.Contains(fileExtension))
                    {
                        // Copier le fichier directement vers le répertoire cible avec le nom de fichier original
                        string destinationFilePath = Path.Combine(RepCible, Path.GetFileName(file));
                        File.Copy(file, destinationFilePath, true);
                    }
                }

                Console.WriteLine("Extensions prioritaires copiées avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }
        }
        public static bool ExtPriorite(string file)
        {
            string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
            List<string> prioritizedExtensions = new List<string>(File.ReadAllLines(filePath));
            string fileExtension = Path.GetExtension(file);
            return prioritizedExtensions.Contains(fileExtension);
        }
    }

    
}
