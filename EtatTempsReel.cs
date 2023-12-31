﻿using System;
using System.Collections.Generic;
using System.Text; 
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace EasySaveProSoft.Version1
{
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

        public void SaveToJson(string Source, string Target, int iden)
        {
            //Console.WriteLine("***En proceso ***");
            string DossierPro = Directory.GetCurrentDirectory();
            string[] ListDossier = DossierPro.Split(@"\");
            string Dir = "";
            for (int k = 0; k < ListDossier.Length - 3; k++)
            {
                Dir += ListDossier[k] + @"\";
            }
            string rutaScriptPowerShell = Dir + "ExecuteLogTempReel.ps1";
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
                        sw.WriteLine($"& '{rutaScriptPowerShell}' -sourcePath "+ Source + " -destinationPath "+ Target+ " -ID "+ iden);
                    }
                }

                // Obtener la salida del proceso
                string resultado = proceso.StandardOutput.ReadToEnd();

                // Mostrar la salida (puedes hacer otras cosas con el resultado)
                //Console.WriteLine("Resultado:");
                Console.WriteLine(resultado);
            }
            return ;
        }
        public void SaveToJsonDiff(string Source, string Target, int iden)
        {
            //Console.WriteLine("***En proceso ***");
            string DossierPro = Directory.GetCurrentDirectory();
            string[] ListDossier = DossierPro.Split(@"\");
            string Dir = "";
            for (int k = 0; k < ListDossier.Length - 3; k++ ) 
            {
                Dir += ListDossier[k] + @"\";
            }
            string rutaScriptPowerShell = Dir + "ExecuteLogTempReelDiff.ps1";
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
                        sw.WriteLine($"& '{rutaScriptPowerShell}' -sourcePath " + Source + " -destinationPath " + Target + " -ID " + iden);
                    }
                }

                // Obtener la salida del proceso
                string resultado = proceso.StandardOutput.ReadToEnd();

                // Mostrar la salida (puedes hacer otras cosas con el resultado)
                //Console.WriteLine("Resultado:");
                Console.WriteLine(resultado);
            }
            return;
        }

    }
} 