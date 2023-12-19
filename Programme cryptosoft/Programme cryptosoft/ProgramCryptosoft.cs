using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace Programme_cryptosoft 
{
    /// <summary>
    /// Represents the main program for CryptoSoft, responsible for encrypting files using XOR encryption.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point for the CryptoSoft program.
        /// </summary>
        void Main()
        {
            // Generate XOR key in CryptoSoft
            byte[] cle = GenererCleXOR64Bits();

            // Use a named pipe to transmit the key
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("CryptoSoftPipe"))
            {
                pipeServer.WaitForConnection();

                // Write the XOR key to the named pipe
                using (StreamWriter sw = new StreamWriter(pipeServer))
                {
                    sw.Write(Convert.ToBase64String(cle));
                }

                // Read command line arguments
                string[] args = Environment.GetCommandLineArgs();

                // Retrieve source and target variables
                string Sources = args.Length > 2 ? args[1] : null;
                string Cible = args.Length > 3 ? args[3] : null;

                if (!string.IsNullOrEmpty(Sources) && !string.IsNullOrEmpty(Cible))
                {
                    // Retrieve extensions to encrypt
                    string selectedExtensionsInput = args.Length > 4 ? args[4] : string.Empty;
                    List<int> selectedExtensions = selectedExtensionsInput.Split(',')
                        .Select(part => int.Parse(part.Trim()))
                        .ToList();

                    // Use the key to encrypt with source, target, and extensions variables
                    int returnCode = ChiffrerDossier(Sources, Cible, cle, selectedExtensions);

                    // Send the return code via the named pipe
                    using (StreamWriter swReturnCode = new StreamWriter(pipeServer))
                    {
                        swReturnCode.Write(returnCode);
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts files in the specified source directory and saves them to the target directory.
        /// </summary>
        /// <param name="Sources">Source directory.</param>
        /// <param name="Cible">Target directory.</param>
        /// <param name="cle">XOR key for encryption.</param>
        /// <param name="selectedExtensions">List of selected extensions to encrypt.</param>
        /// <returns>Return code indicating success or failure.</returns>
        public int ChiffrerDossier(string Sources, string Cible, byte[] cle, List<int> selectedExtensions)
        {
            try
            {
                // Check if the source directory exists
                if (!Directory.Exists(Sources))
                {
                    Console.WriteLine("Sources directory does not exist.");
                    return -3; // Error code for invalid Sources directory
                }

                // Check if the target directory exists
                if (!Directory.Exists(Cible))
                {
                    Console.WriteLine("Target directory does not exist.");
                    return -4; // Error code for invalid Target directory
                }

                Console.WriteLine($"Sources Directory: {Sources}");
                Console.WriteLine($"Target Directory: {Cible}");

                string[] files = Directory.GetFiles(Sources);

                foreach (string filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = Path.GetExtension(filePath);

                    if (selectedExtensions.Contains(GetExtensionIndex(fileExtension)))
                    {
                        // Construct the full path for the new encrypted text file in the Target directory
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

                return 1; // Success code
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption failed. Exception: {ex.Message}");
                return -2; // General error code
            }
        }

        /// <summary>
        /// Gets the index corresponding to the given file extension.
        /// </summary>
        /// <param name="fileExtension">File extension.</param>
        /// <returns>Index corresponding to the file extension.</returns>
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
        /// <returns>Generated XOR key.</returns>
        static byte[] GenererCleXOR64Bits()
        {
            byte[] cleBytes = new byte[8];
            new Random().NextBytes(cleBytes);
            return cleBytes;
        }
    }
}
 