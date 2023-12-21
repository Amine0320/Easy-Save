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
using System.Diagnostics;
using System.Threading;
//using EasySaveProSoft.Version1;

namespace WpfApp2
{
    public class Systeme
    {
        JsonLogger jsonLogger = JsonLogger.Instance;
        CompleteSave completeSave = CompleteSave.Instance;

        static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private int IdSys = 0;
        public static List<int> SauvDejaCreee = new List<int>();
        public static bool VerifieDispo(List<int> list)
        {
            foreach (int i in list)
            {
                if (SauvDejaCreee.Contains(i))
                {
                    //throw new Exception("La sauvegarde" + i.ToString() + "est déjà utilisée./ The save" + i.ToString() + "is already used");
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
            NewSauvegarder.IdTravailS = i;
            NewSauvegarder.NomTDS = "Save" + i.ToString();
            NewSauvegarder.RepSource = saves;
            NewSauvegarder.RepCible = sources;
            NewSauvegarder.Type = Type;
            return NewSauvegarder;
        }           
        
        

        public void EnregistrerSauvegarde(int i, TravailSauvegarde NewSauvegarder, int log, CancellationTokenSource cancellationTokenSource)
        {
            string filePath = GlobalVariables.Dir + "limite.txt";
            int limite =  Convert.ToInt32(File.ReadAllText(filePath));
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            completeSave.CopyFiles(NewSauvegarder.IdTravailS, NewSauvegarder.RepSource, NewSauvegarder.RepCible, semaphore);
            string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
            //Console.WriteLine(ExecuteFileSize);
            string output2 = PowerShellHandler.Command(ExecuteFileSize);
            //Console.WriteLine("*******************************");
            long FileSize = long.Parse(output2);
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

            jsonLogger.Log(log1, pathcomplete);

            //Console.WriteLine($"Log created in: {pathcomplete}");
            return;

        }

        public void EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSauvegarder, int log, CancellationTokenSource cancellationTokenSource)
        {
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            //string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            SaveDiff saveDiff = SaveDiff.Instance;
            //long FileSize = 
            saveDiff.CopyFiles(NewSauvegarder.IdTravailS, NewSauvegarder.RepSource, NewSauvegarder.RepCible, semaphore);
            DateTime date2 = DateTime.Now;
            string timeCrypt = "";
            TimeSpan soustraction = date2 - date1;
            double Secondssoustraction = soustraction.TotalSeconds;
            LogJournalier log1 = new LogJournalier(1, "Journal " + i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, GlobalVariables.FileSize, Secondssoustraction, dateString2, timeCrypt);
            log1.timeCrypt = log1.ObtenuValeur();
            string fichier = @"C:\LOGJ";
            string logtype = ".json";
            if (log == 2)
            {
                logtype = ".xml";
            }
            string nombrefichier = TodayDateForString + logtype;
            string pathcomplete = Path.Combine(fichier, nombrefichier);
            jsonLogger.Log(log1, pathcomplete);
            
            return;
        }
    }
}
