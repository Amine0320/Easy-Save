using System.Management.Automation;
using System.Text;
using System;

namespace PowershellShowcase
{
    /// <summary>
    /// A utility class for handling PowerShell commands.
    /// </summary>
    public static class PowerShellHandler
    {
        private static readonly PowerShell _ps = PowerShell.Create();

        /// <summary>
        /// Executes a PowerShell command or script and returns the output as a string.
        /// </summary>
        /// <param name="script">The PowerShell script or command to execute.</param>
        /// <returns>The output of the PowerShell script or command as a string.</returns>
        public static string Command(string script)
        {
            string errorMsg = string.Empty;

            // Add the script to the PowerShell session
            _ps.AddScript(script);

            // Make sure return values are outputted to the stream captured by C#
            _ps.AddCommand("Out-String");

            // Set up a data collection for PowerShell output
            PSDataCollection<PSObject> outputCollection = new();

            // Event handler for capturing PowerShell errors
            _ps.Streams.Error.DataAdded += (object sender, DataAddedEventArgs e) =>
            {
                errorMsg = ((PSDataCollection<ErrorRecord>)sender)[e.Index].ToString();
            };

            // Begin asynchronous invocation of PowerShell script or command
            IAsyncResult result = _ps.BeginInvoke<PSObject, PSObject>(null, outputCollection);

            // Wait for PowerShell command/script to finish executing
            _ps.EndInvoke(result);

            StringBuilder sb = new();

            // Append each output item to the StringBuilder
            foreach (var outputItem in outputCollection)
            {
                sb.AppendLine(outputItem.BaseObject.ToString());
            }

            // Clears the commands in the PowerShell runspace for the next use
            _ps.Commands.Clear();

            // If an error is encountered, return it
            if (!string.IsNullOrEmpty(errorMsg))
                return errorMsg;

            return sb.ToString().Trim();
        }
    }
}
 