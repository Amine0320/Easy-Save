using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
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
        // Recuperer les valeurs des attribus de class 
        public LogJournalier(int idLogJourn, string nomLj, string fileSource, string fileTarget, long fileSize, double fileTransferTime, string time, string timeCrypt)
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
        //Methode Modifier qui existe dans le Travail de sauvergarde est sera herite par LogJournalier
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
        // Methode Consulter qui existe dans le Travail de sauvergarde est sera herite par LogJournalier
        // A verifier 

        public string Consulter()
        {
            //string rutaScriptPowerShell = GlobalVariables.Dir + "ExecuteJorJournalier.ps1";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };
            //Console.WriteLine("***Ejecutando ***");
            using (Process proceso = new Process { StartInfo = psi })
            {
                proceso.Start();
                int timeCrypt = 0;
                // Cargar y ejecutar el script PowerShell
                using (System.IO.StreamWriter sw = proceso.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        //sw.WriteLine($"& '{rutaScriptPowerShell}' -IdLogJourn " + this.IdLogJourn + " -NomLj " + this.NomLj + " -FileSource " + this.FileSource + " -FileTarget " + this.FileTarget + " -FileSize " + this.FileSize + " -FileTransferTime " + this.FileTransferTime + " -Time " + this.Time + " - timeCrypt " + timeCrypt);
                    }
                }

                // Obtener la salida del proceso
                string resultado = proceso.StandardOutput.ReadToEnd();

                // Mostrar la salida (puedes hacer otras cosas con el resultado)
                //Console.WriteLine("Resultado:");
                //Console.WriteLine(resultado);
            }
            return $"Id du journal : {this.IdLogJourn}\r\n" +
                   $"Nom du journal : {this.NomLj}\r\n" +
                   $"Fichier source : {this.FileSource}\r\n" +
                   $"Fichier cible : {this.FileTarget}\r\n" +
                   $"Taille du fichier : {this.FileSize}\r\n" +
                   $"Temps de transfert du fichier : {this.FileTransferTime}\r\n" +
                   $"Heure : {this.Time}";
        }
        public string ObtenuValeur()
        {
            // Spécifiez le chemin du fichier JSON
            string cheminFichierJson = @"C:\LOGJ\state3.json";

            string timetre = "";
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
                    timetre = timeCryptProperty.Value.ToString();

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

    }
}
