using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using PowershellShowcase;

using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;


namespace WpfApp2
{
    public class Systeme
    {
        private int IdSys = 0;
        public static List<int> SauvDejaCreee = new List<int>();
        public static bool VerifieDispo(List<int> list)
        {
            foreach (int i in list)
            {
                if (SauvDejaCreee.Contains(i))
                {
                    throw new Exception("La sauvegarde" + i.ToString() + "est déjà utilisée./ The save" + i.ToString() + "is already used");
                }
                if (i < 1)
                {
                    throw new Exception("La sauvegarde" + i.ToString() + "ne peut pas etre utilisée./ The save" + i.ToString() + "cannot be used");
                }
            }
            return true;
        }

        public TravailSauvegarde CreerSauvegarde(int i, string saves, string sources, TypeSauv Type)
        {

            TravailSauvegarde NewSauvegarder = new TravailSauvegarde();
            NewSauvegarder.NomTDS = "Save" + i.ToString();
            NewSauvegarder.RepSource = saves;
            NewSauvegarder.RepCible = sources;
            NewSauvegarder.Type = Type;
            return NewSauvegarder;
        }

        public void EnregistrerSauvegarde(int i, TravailSauvegarde NewSauvegarder, int log)
        {
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            Console.WriteLine("************************");
            Console.WriteLine("***Project Easy Save ***");
            Console.WriteLine("************************");
            string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            string Execute = @"Copy-Item -Path " + NewSauvegarder.RepSource + " -Destination " + NewSauvegarder.RepCible + " -Recurse -Force";
            string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
            //Console.WriteLine(ExecuteFileSize);
            string output2 = PowerShellHandler.Command(ExecuteFileSize);
            //Console.WriteLine("*******************************");
            long FileSize = long.Parse(output2);
            //string output = PowerShellHandler.Command(Execute);
            // Appeler la méthode ExtPriori de la classe ExtensionsPriori
            ExtensionsPriori.ExtPriori(NewSauvegarder.RepSource, NewSauvegarder.RepCible);
            Console.WriteLine("*** PAR PRIORI ***");
            etatTempsReel.SaveToJson(NewSauvegarder.RepSource, NewSauvegarder.RepCible, i);
            Console.WriteLine("***copie réussie ***");
            DateTime date2 = DateTime.Now;
            string timeCrypt = "";
            TimeSpan soustraction = date2 - date1;
            double Secondssoustraction = soustraction.TotalSeconds;
            LogJournalier log1 = new LogJournalier(1, "Journal " + i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2, timeCrypt);
            log1.timeCrypt = log1.ObtenuValeur();
            
            string fichier = @"C:\LOGJ";
            string logtype = ".json";
            if (log == 2)
            {
                logtype = ".xml";
            }
            string nombrefichier = TodayDateForString + logtype;
            string pathcomplete = Path.Combine(fichier, nombrefichier);
            string jsonString = JsonSerializer.Serialize(log1);
            using (StreamWriter sw = File.AppendText(pathcomplete))
            {
                sw.WriteLine(jsonString);
            }
           
            

            //Console.WriteLine($"Log created in: {pathcomplete}");
            return;

        }
        public void EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSauvegarder, int log)
        {
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            Console.WriteLine("************************");
            Console.WriteLine("***Project Easy Save ***");
            Console.WriteLine("************************");
            string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            string Execute = @"Copy-Item -Path " + NewSauvegarder.RepSource + " -Destination " + NewSauvegarder.RepCible + " -Recurse -Force";
            string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
            //Console.WriteLine(ExecuteFileSize);
            string output2 = PowerShellHandler.Command(ExecuteFileSize);

            long FileSize = long.Parse(output2);
            //string output = PowerShellHandler.Command(Execute);
            etatTempsReel.SaveToJsonDiff(NewSauvegarder.RepSource, NewSauvegarder.RepCible, i);
            Console.WriteLine("***copie réussie ***");
            DateTime date2 = DateTime.Now;
            string timeCrypt = "";
            TimeSpan soustraction = date2 - date1;
            double Secondssoustraction = soustraction.TotalSeconds;
            LogJournalier log1 = new LogJournalier(1, "Journal " + i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2, timeCrypt) ;
            log1.timeCrypt = log1.ObtenuValeur();
            string fichier = @"C:\LOGJ";
            string logtype = ".json";
            if (log == 2)
            {
                logtype = ".xml";
            }
            string nombrefichier = TodayDateForString + logtype;
            string pathcomplete = Path.Combine(fichier, nombrefichier);
            string jsonString = JsonSerializer.Serialize(log1);
            using (StreamWriter sw = File.AppendText(pathcomplete))
            {
                sw.WriteLine(jsonString);
            }


            // Appeler la méthode ExtPriori de la classe ExtensionsPriori
            ExtensionsPriori.ExtPriori(NewSauvegarder.RepSource, NewSauvegarder.RepCible);


            //Console.WriteLine($"Json created in: {pathcomplete}");
            return;

        }
    }
}
