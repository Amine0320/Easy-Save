using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PowershellShowcase
{
    public class LogJournalier
    {
        public int IdLogJourn { get; set; }
        public string NomLj { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long FileSize { get; set; }
        public double FileTransferTime { get; set; }
        public string Time { get; set; }
        public string timeCrypt { get; set; }
        
        public LogJournalier(int idLogJourn, string nomLj, string fileSource, string fileTarget, long fileSize, double fileTransferTime, string time)
        {
            this.IdLogJourn = idLogJourn;
            this.NomLj = nomLj;
            this.FileSource = fileSource;
            this.FileTarget = fileTarget;
            this.FileSize = fileSize;
            this.FileTransferTime = fileTransferTime;
            this.Time = time;
            this.timeCrypt = ObtenuValeur();
        }

        public void Modifier(int idLogJourn, string nomLj, string fileSource, string fileTarget, long fileSize, double fileTransferTime, string time)
        {
            this.IdLogJourn = idLogJourn;
            this.NomLj = nomLj;
            this.FileSource = fileSource;
            this.FileTarget = fileTarget;
            this.FileSize = fileSize;
            this.FileTransferTime = fileTransferTime;
            this.Time = time;
            //this.timeCrypt = timeCrypt;
        }




        public string Consulter(int outputFormat)
        {
            string DossierPro = Directory.GetCurrentDirectory();
            string scriptPowerShellPath = DossierPro + "ExecuteJorJournalier.ps1";
            //@"C:\CESIProject2\Csharp\ExecuteJorJournalier.ps1";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            //      EnregistrerTempsCryptage(this.timeCrypt);
            
            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine($"& '{scriptPowerShellPath}' -IdLogJourn {this.IdLogJourn} -NomLj {this.NomLj} -FileSource {this.FileSource} -FileTarget {this.FileTarget} -FileSize {this.FileSize} -FileTransferTime {this.FileTransferTime} -Time {this.Time} -timeCrypt {timeCrypt}");

                    }
                }

                string result = process.StandardOutput.ReadToEnd();

                string serializedResult = outputFormat switch
                {
                    1 => SerializeToJson(result),
                    2 => SerializeToXml(result),
                    _ => throw new ArgumentException("Choix invalide. Choisissez 1 pour JSON ou 2 pour XML./Invalid output format choice. Please choose 1 for JSON or 2 for XML."),
                };

                return serializedResult;
            }
        }

        private string SerializeToJson(string result)
        {
            return System.Text.Json.JsonSerializer.Serialize(result);
        }

        private string SerializeToXml(string result)
        {
            var serializer = new XmlSerializer(typeof(string));
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(xmlWriter, result);
                }
                return stringWriter.ToString();
            }
        }
        public string ObtenuValeur()
        {
            // Spécifiez le chemin du fichier JSON
            string cheminFichierJson = @"C:\LOGJ\state3.json";
            
            string timetre="";
            try
            {
                // Lire le contenu du fichier JSON
                string contenuJson = File.ReadAllText(cheminFichierJson);

                // Analyser le JSON en tant qu'objet (JObject)
                JObject jsonObject = JObject.Parse(contenuJson);

                // Extraire les deux propriétés
                JProperty timeCryptProperty = jsonObject.Property("timeCrypt");
                JProperty dateEnregistrementProperty = jsonObject.Property("DateEnregistrement");
                timetre = timeCryptProperty.Value.ToString();
                // Vérifier si les propriétés existent
                if (timeCryptProperty != null && dateEnregistrementProperty != null)
                {
                    // Traitez les deux propriétés comme nécessaire
                    Console.WriteLine($"timeCrypt : {timeCryptProperty.Value}");
                    Console.WriteLine($"DateEnregistrement : {dateEnregistrementProperty.Value}");
                    timetre= timeCryptProperty.Value.ToString();

                }
                else
                {
                    Console.WriteLine("Le fichier JSON ne contient pas les propriétés attendues.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur s'est produite : " + e.Message);
            }
            return timetre;
        }
        
    }
}