using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp2 
{
    /// <summary>
    /// Represents the real-time status of a backup operation.
    /// </summary>
    public class EtatTempsReel
    {
        // Declaration of attributes
        public int IdEtaTemp { get; set; }
        public string NomETR { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string State { get; set; }
        public int TotalFilesToCopy { get; set; }
        public int TotalFilesSize { get; set; }
        public int NbFilesLeftToDo { get; set; }
        public int Progression { get; set; }

        /// <summary>
        /// Saves the real-time status to a JSON file using a PowerShell script.
        /// </summary>
        /// <param name="Source">The source path.</param>
        /// <param name="Target">The target path.</param>
        /// <param name="iden">The identifier.</param>
        /// <param name="cancellationTokenSource">The cancellation token source.</param>
        public async void SaveToJson(string Source, string Target, int iden, CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Define the path to the PowerShell script
                    string rutaScriptPowerShell = GlobalVariables.Dir + "ExecuteLogTempReel.ps1";

                    // Configure the process start info for the PowerShell execution
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    // Start a new process for executing the PowerShell script
                    using (Process proceso = new Process { StartInfo = psi })
                    {
                        proceso.Start();

                        // Load and execute the PowerShell script
                        using (System.IO.StreamWriter sw = proceso.StandardInput)
                        {
                            if (sw.BaseStream.CanWrite)
                            {
                                sw.WriteLine($"& '{rutaScriptPowerShell}' -sourcePath " + Source + " -destinationPath " + Target + " -ID " + iden);
                            }
                        }
                    }
                }, cancellationTokenSource.Token);

            }
            catch (OperationCanceledException)
            {
                throw;
            }

            return;
        }

        /// <summary>
        /// Saves the real-time status for differential backup to a JSON file using a PowerShell script.
        /// </summary>
        /// <param name="Source">The source path.</param>
        /// <param name="Target">The target path.</param>
        /// <param name="iden">The identifier.</param>
        /// <param name="cancellationTokenSource">The cancellation token source.</param>
        public async void SaveToJsonDiff(string Source, string Target, int iden, CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Define the path to the PowerShell script for differential backup
                    string rutaScriptPowerShell = GlobalVariables.Dir + "ExecuteLogTempReelDiff.ps1";

                    // Configure the process start info for the PowerShell execution
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    // Start a new process for executing the PowerShell script
                    using (Process proceso = new Process { StartInfo = psi })
                    {
                        proceso.Start();

                        // Load and execute the PowerShell script
                        using (System.IO.StreamWriter sw = proceso.StandardInput)
                        {
                            if (sw.BaseStream.CanWrite)
                            {
                                sw.WriteLine($"& '{rutaScriptPowerShell}' -sourcePath " + Source + " -destinationPath " + Target + " -ID " + iden);
                            }
                        }
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