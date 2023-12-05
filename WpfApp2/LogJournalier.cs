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
        // Recuperer les valeurs des attribus de class 
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
            string DossierPro = Directory.GetCurrentDirectory();
            string rutaScriptPowerShell = @"C:\CESIProject2\Csharp\ExecuteJorJournalier.ps1";
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

                // Cargar y ejecutar el script PowerShell
                using (System.IO.StreamWriter sw = proceso.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine($"& '{rutaScriptPowerShell}' -IdLogJourn " + this.IdLogJourn + " -NomLj " + this.NomLj + " -FileSource " + this.FileSource + " -FileTarget " + this.FileTarget + " -FileSize " + this.FileSize + " -FileTransferTime " + this.FileTransferTime + " -Time " + this.Time);
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
