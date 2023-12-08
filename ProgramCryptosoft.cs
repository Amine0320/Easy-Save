using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Programme_cryptosoft
{

     class ProgramCryptoSoft
    {
        public void Main()
        {
            // Générer la clé XOR dans CryptoSoft
            byte[] cle = GenererCleXOR64Bits();

            // Utiliser un tube nommé pour transmettre la clé
            Task.Factory.StartNew(() =>
            {
                var pipeServer = new NamedPipeServerStream("CryptoSoftPipe");
                pipeServer.WaitForConnection();

                using (StreamWriter sw = new StreamWriter(pipeServer))
                {
                    sw.Write(Convert.ToBase64String(cle));
                }

                // Lire les arguments de la ligne de commande
                string[] args = Environment.GetCommandLineArgs();

                // Récupérer les variables source et Cible
                string Sources = args.Length > 2 ? args[1] : null;
                string Cible = args.Length > 3 ? args[3] : null;

                if (!string.IsNullOrEmpty(Sources) && !string.IsNullOrEmpty(Cible))
                {
                    // Récupérer les extensions à chiffrer
                    string selectedExtensionsInput = args.Length > 4 ? args[4] : string.Empty;
                    List<int> selectedExtensions = selectedExtensionsInput.Split(',')
                        .Select(part => int.Parse(part.Trim()))
                        .ToList();

                    // Utiliser la clé pour chiffrer avec les variables Sources, Cible, et extensions
                    int returnCode = ChiffrerDossier(Sources, Cible, cle, selectedExtensions);

                    // Envoyer le code de retour via le tube nommé
                    using (StreamWriter swReturnCode = new StreamWriter(pipeServer))
                    {
                        swReturnCode.Write(returnCode);
                    }
                }
            });
        }

        public int ChiffrerDossier(string Sources, string Cible, byte[] cle, List<int> selectedExtensions)
        {
            try
            {
                if (!Directory.Exists(Sources))
                {
                    Console.WriteLine("Sourcess directory does not exist.");
                    return -3; // Code d'erreur pour répertoire Sourcess invalide
                }

                if (!Directory.Exists(Cible))
                {
                    Console.WriteLine("Cible directory does not exist.");
                    return -4; // Code d'erreur pour répertoire Cible invalide
                }

                Console.WriteLine($"Sourcess Directory: {Sources}");
                Console.WriteLine($"Cible Directory: {Cible}");

                string[] files = Directory.GetFiles(Sources);

                Stopwatch encryptionStopwatch = new Stopwatch();
                encryptionStopwatch.Start();

                foreach (string filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = Path.GetExtension(filePath);

                    if (selectedExtensions.Contains(GetExtensionIndex(fileExtension)))
                    {
                        string encryptedFilePath = Path.Combine(Cible, "encrypted_" + fileName + ".txt");

                        using (FileStream fsSources = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        using (StreamWriter sw = new StreamWriter(encryptedFilePath))
                        {
                            int keyIndex = 0;

                            while (fsSources.Position < fsSources.Length)
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead = fsSources.Read(buffer, 0, buffer.Length);

                                // Apply XOR encryption directly
                                for (int j = 0; j < bytesRead; j++)
                                {
                                    buffer[j] = (byte)(buffer[j] ^ cle[keyIndex]);
                                    keyIndex = (keyIndex + 1) % cle.Length;
                                }

                                // Convert the encrypted buffer to a Base64 string and write it to the text file
                                string encryptedBase64 = Convert.ToBase64String(buffer, 0, bytesRead);
                                sw.Write(encryptedBase64);
                            }
                        }

                        Console.WriteLine($"Encryption successful for {fileName}. Encrypted text file saved at: {encryptedFilePath}");
                    }
                }

                encryptionStopwatch.Stop();
                TimeSpan timeCrypt = encryptionStopwatch.Elapsed;

                Console.WriteLine($"Temps de cryptage total : {timeCrypt.TotalMilliseconds} millisecondes");

                // Enregistre le temps de cryptage dans le log
                EnregistrerDansLeJournal(timeCrypt.TotalMilliseconds);

                return 1; // Code de succès
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption failed. Exception: {ex.Message}");
                return -2; // Code d'erreur général
            }
        }

        public int GetExtensionIndex(string fileExtension)
        {
            Dictionary<int, string> extensions = new Dictionary<int, string>
        {
            { 1, ".txt" },
            { 2, ".jpg" },
            { 3, ".png" },
            { 4, ".pdf" },
            { 5, ".docx" },
            // Add more extensions as needed
        };

            return extensions.FirstOrDefault(x => x.Value.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)).Key;
        }

        static byte[] GenererCleXOR64Bits()
        {
            byte[] cleBytes = new byte[8];
            new Random().NextBytes(cleBytes);
            return cleBytes;
        }

        public void EnregistrerDansLeJournal(double timeCrypt)
        {
            try
            {
                // Spécifie le chemin du fichier JSON
                string rutaArchivo = @"C:\LOGJ\state3.json";
                File.Delete(rutaArchivo);
                // Crée un nouvel objet pour enregistrer les informations
                var logInfo = new
                {
                    timeCrypt = timeCrypt,
                    DateEnregistrement = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    // Ajoute d'autres informations que tu souhaites enregistrer
                };

                // Convertit l'objet en une chaîne JSON
                string logInfoJson = JsonSerializer.Serialize(logInfo);

                // Enregistre la chaîne JSON dans le fichier
                File.WriteAllText(rutaArchivo, logInfoJson);

                Console.WriteLine($"Temps de cryptage enregistré dans le journal JSON : {rutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'enregistrement dans le journal. Exception : {ex.Message}");
            }
        }
    }

}