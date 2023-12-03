using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

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

        public LogJournalier(int idLogJourn, string nomLj, string fileSource, string fileTarget, long fileSize, double fileTransferTime, string time)
        {
            this.IdLogJourn = idLogJourn;
            this.NomLj = nomLj;
            this.FileSource = fileSource;
            this.FileTarget = fileTarget;
            this.FileSize = fileSize;
            this.FileTransferTime = fileTransferTime;
            this.Time = time;
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
                UseShellExecute = false
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine($"& '{scriptPowerShellPath}' -IdLogJourn {this.IdLogJourn} -NomLj {this.NomLj} -FileSource {this.FileSource} -FileTarget {this.FileTarget} -FileSize {this.FileSize} -FileTransferTime {this.FileTransferTime} -Time {this.Time}");
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
            return JsonSerializer.Serialize(result);
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
    }
}

/* return $"Id du journal : {this.IdLogJourn}\r\n" +
        $"Nom du journal : {this.NomLj}\r\n" +
        $"Fichier source : {this.FileSource}\r\n" +
        $"Fichier cible : {this.FileTarget}\r\n" +
        $"Taille du fichier : {this.FileSize}\r\n" +
        $"Temps de transfert du fichier : {this.FileTransferTime}\r\n" +
        $"Heure : {this.Time}";
 }
/* public string Consulter()
{
  return $"Id du journal : {this.IdLogJourn}\n" +
         $"Nom du journal : {this.NomLj}\n" +
         $"Fichier source : {this.FileSource}\n" +
         $"Fichier cible : {this.FileTarget}\n" +
         $"Taille du fichier : {this.FileSize}\n" +
         $"Temps de transfert du fichier : {this.FileTransferTime}\n" +
         $"Heure : {this.Time}";
}*/  