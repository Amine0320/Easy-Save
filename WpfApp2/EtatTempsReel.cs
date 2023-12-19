using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp2
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

        
        
        public async void SaveToJson(string Source, string Target, int iden, CancellationTokenSource cancellationTokenSource)
        {
            //cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await Task.Run(() =>
            {
                string rutaScriptPowerShell = GlobalVariables.Dir + "ExecuteLogTempReel.ps1";
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
                    //cancellationTokenSource.Cancel();
                    // Obtener la salida del proceso
                    //string resultado = proceso.StandardOutput.ReadToEnd();

                    // Mostrar la salida (puedes hacer otras cosas con el resultado)
                    //Console.WriteLine("Resultado:");
                    //Console.WriteLine(resultado);
                }
            }, cancellationTokenSource.Token);
                
            } catch (OperationCanceledException)
            {
                throw;
            }
            
            return;
        }
        public async void SaveToJsonDiff(string Source, string Target, int iden, CancellationTokenSource cancellationTokenSource)
        {
            //Console.WriteLine("***En proceso ***");
            try {
                await Task.Run(() =>
                {
                    string rutaScriptPowerShell = GlobalVariables.Dir + "ExecuteLogTempReelDiff.ps1";
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
                        //string resultado = proceso.StandardOutput.ReadToEnd();

                        // Mostrar la salida (puedes hacer otras cosas con el resultado)
                        //Console.WriteLine("Resultado:");
                        //Console.WriteLine(resultado);
                    }
                }, cancellationTokenSource.Token);
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            
            
            return;
        }
        
    }

}
