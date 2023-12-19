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
    /// <summary>
    /// Represents the CryptoSoft program for encrypting files using XOR encryption.
    /// </summary>
    class ProgramCryptoSoft
    {
        /// <summary>
        /// Main entry point for the CryptoSoft program.
        /// </summary>
        public void Main()
        {
            // Generate XOR key in CryptoSoft
            byte[] key = GenerateXORKey();

            // Use a named pipe to transmit the key
            Task.Factory.StartNew(() =>
            {
                var pipeServer = new NamedPipeServerStream("CryptoSoftPipe");
                pipeServer.WaitForConnection();

                using (StreamWriter sw = new StreamWriter(pipeServer))
                {
                    sw.Write(Convert.ToBase64String(key));
                }

                // Read command line arguments
                string[] args = Environment.GetCommandLineArgs();

                // Get source and target variables
                string source = args.Length > 2 ? args[1] : null;
                string target = args.Length > 3 ? args[3] : null;

                if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(target))
                {
                    // Get extensions to encrypt
                    string selectedExtensionsInput = args.Length > 4 ? args[4] : string.Empty;
                    List<int> selectedExtensions = selectedExtensionsInput.Split(',')
                        .Select(part => int.Parse(part.Trim()))
                        .ToList();

                    // Use the key to encrypt with source, target, and extensions
                    int returnCode = EncryptFolder(source, target, key, selectedExtensions);

                    // Send the return code through the named pipe
                    using (StreamWriter swReturnCode = new StreamWriter(pipeServer))
                    {
                        swReturnCode.Write(returnCode);
                    }
                }
            });
        }

        /// <summary>
        /// Encrypts files in the specified source folder and saves them to the target folder.
        /// </summary>
        /// <param name="source">Source folder path.</param>
        /// <param name="target">Target folder path.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="selectedExtensions">List of selected extensions.</param>
        /// <returns>Return code indicating success or failure.</returns>
        public int EncryptFolder(string source, string target, byte[] key, List<int> selectedExtensions)
        {
            try
            {
                if (!Directory.Exists(source))
                {
                    Console.WriteLine("Source directory does not exist.");
                    return -3; // Error code for invalid source directory
                }

                if (!Directory.Exists(target))
                {
                    Console.WriteLine("Target directory does not exist.");
                    return -4; // Error code for invalid target directory
                }

                Console.WriteLine($"Source Directory: {source}");
                Console.WriteLine($"Target Directory: {target}");
                int extExist = 0;

                string[] files = Directory.GetFiles(source);

                Stopwatch encryptionStopwatch = new Stopwatch();
                encryptionStopwatch.Start();

                foreach (string filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = Path.GetExtension(filePath);

                    if (selectedExtensions.Contains(GetExtensionIndex(fileExtension)))
                    {
                        extExist++;
                        string encryptedFilePath = Path.Combine(target, "encrypted_" + fileName + ".txt");

                        using (FileStream fsSource = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        using (StreamWriter sw = new StreamWriter(encryptedFilePath))
                        {
                            int keyIndex = 0;

                            while (fsSource.Position < fsSource.Length)
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead = fsSource.Read(buffer, 0, buffer.Length);

                                // Apply XOR encryption directly
                                for (int j = 0; j < bytesRead; j++)
                                {
                                    buffer[j] = (byte)(buffer[j] ^ key[keyIndex]);
                                    keyIndex = (keyIndex + 1) % key.Length;
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
                TimeSpan timeCrypt = TimeSpan.FromSeconds(0);
                if (extExist != 0) { timeCrypt = encryptionStopwatch.Elapsed; }

                Console.WriteLine($"Total encryption time: {timeCrypt.TotalMilliseconds} milliseconds");

                // Record encryption time in the log
                RecordInLog(timeCrypt.TotalMilliseconds);

                return 1; // Success code
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption failed. Exception: {ex.Message}");
                return -2; // General error code
            }
        }

        /// <summary>
        /// Gets the index of the extension from the dictionary.
        /// </summary>
        /// <param name="fileExtension">File extension.</param>
        /// <returns>Extension index.</returns>
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

        /// <summary>
        /// Generates a 64-bit XOR key.
        /// </summary>
        /// <returns>XOR key.</returns>
        static byte[] GenerateXORKey()
        {
            byte[] keyBytes = new byte[8];
            new Random().NextBytes(keyBytes);
            return keyBytes;
        }

        /// <summary>
        /// Records the encryption time in the JSON log.
        /// </summary>
        /// <param name="timeCrypt">Encryption time.</param>
        public void RecordInLog(double timeCrypt)
        {
            try
            {
                // Specifies the JSON file path
                string filePath = @"C:\LOGJ\state3.json";
                File.Delete(filePath);

                // Creates a new object to save the information
                var logInfo = new
                {
                    timeCrypt = timeCrypt,
                    RecordingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    // Add other information you want to record
                };

                // Converts the object to a JSON string
                string logInfoJson = JsonSerializer.Serialize(logInfo);

                // Writes the JSON string to the file
                File.WriteAllText(filePath, logInfoJson);

                Console.WriteLine($"Encryption time recorded in JSON log: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error recording in the log. Exception: {ex.Message}");
            }
        }
    }
}
 