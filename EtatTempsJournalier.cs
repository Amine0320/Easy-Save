using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

public class EtatTempsReel
{
    // Declaration des attribus 
    public int IdEtaTemp { get; set; }
    public string NomETR { get; set; }
    public string SourceFilePath { get; set; }
    public string TargetFilePath { get; set; }
    public string State { get; set; }
    public int TotalFilesToCopy { get; set; }
    public int TotalFilesSize { get; set; }
    public int NbFilesLeftToDo { get; set; }
    public int Progression { get; set; }

    public EtatTempsReel(int id, string nom, string source, string target, string etat, int TFC, int TFS, int NL, int Progres) 
    {
        IdEtaTemp = id;
        NomETR = nom;
        SourceFilePath = source;
        TargetFilePath = target;
        State = etat;
        TotalFilesToCopy = NL;
        TotalFilesSize = Progres;

    }

    public string Consulter()
    {

        return $"Id du journal : {this.IdEtaTemp}\n" +
            $"Nom du journal : {this.NomETR}\n" +
            $"Fichier source : {this.SourceFilePath}\n" +
            $"Fichier cible : {this.TargetFilePath}\n" +
            $"Etat : {this.State}\n" +
            $"Nombre Total : {this.TotalFilesToCopy}\n" +
            $"Taille Totale : {this.TotalFilesSize}\n" +
            $"Fichiers Restants : {this.Progression}\n" +
            $"Progression : {this.Progression}";
    }
    public void SaveToJson(int i, string Source, string Target)
    {
        Console.WriteLine("***En proceso ***");
        string DossierPro = Directory.GetCurrentDirectory();
        string rutaScriptPowerShell = DossierPro + "ExecuteLogTempReel.ps1";
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            UseShellExecute = false
        };
        using (Process proceso = new Process { StartInfo = psi })
        {
            proceso.Start();

            // Cargar y ejecutar el script PowerShell
            using (System.IO.StreamWriter sw = proceso.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine($"& '{rutaScriptPowerShell}' -nbr" + i + " -sourcePath " + Source + " -destinationPath " + Target);
                }
            }

            // Obtener la salida del proceso
            string resultado = proceso.StandardOutput.ReadToEnd();

            // Mostrar la salida (puedes hacer otras cosas con el resultado)
            Console.WriteLine(resultado);
        }
    } 
}