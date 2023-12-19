using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowershellShowcase;

namespace WpfApp2
{ 
    /// <summary>
    /// Represents a backup job configuration.
    /// </summary>
    public class TravailSauvegarde
    {
        private int IdTravailS = 0;  // Unique identifier for the backup job

        public string NomTDS;        // Name of the backup job
        public string RepSource;     // Source directory to be backed up
        public string RepCible;      // Target directory where the backup will be saved
        public TypeSauv Type;        // Type of backup (Complete or Differential)

        /// <summary>
        /// Static method for modifying backup job configuration. Placeholder method.
        /// </summary>
        static void Modifier()
        {
            // Placeholder method for modifying backup job configuration
            return;
        }

        /// <summary>
        /// Static method for consulting backup job information. Placeholder method.
        /// </summary>
        /// <returns>Null string (placeholder).</returns>
        static string Consulter()
        {
            string n1 = null;  // Placeholder variable
            return n1;         // Return null (placeholder)
        }
    }
}  