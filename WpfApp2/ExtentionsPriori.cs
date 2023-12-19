using System;
using System.Collections.Generic;
using System.IO;
using WpfApp2;

namespace WpfApp2
{
    public class ExtensionsPriori
    {

        public static bool ExtPriorite(string file)
        {
            string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
            List<string> prioritizedExtensions = new List<string>(File.ReadAllLines(filePath));
            string fileExtension = Path.GetExtension(file);
            return prioritizedExtensions.Contains(fileExtension);
        }
    }

    
}
