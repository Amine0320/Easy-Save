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
            string[] files = Directory.GetFiles(NewSauvegarder.RepSource);
            string stopPath = @"C:\LOGJ\stop.txt";
            
            while (File.ReadAllText(stopPath).Equals("go"))
            {
                //D'abord les extensions prioritaires
                foreach (string file in files.Where(file => ExtensionsPriori.ExtPriorite(file)))
                {
                    long fileSize = new FileInfo(file).Length;
                    if (fileSize > limite * 1024)
                    {
                        semaphore.Wait();
                        try
                        {
                            string destinationPath = Path.Combine(NewSauvegarder.RepCible, Path.GetFileName(file));
                            File.Copy(file, destinationPath, true);
                        }
                        finally { semaphore.Release(); }
                    }
                    else 
                    { 
                        string destinationPath = Path.Combine(NewSauvegarder.RepCible, Path.GetFileName(file));
                        File.Copy(file, destinationPath, true);
                    }
                }
            
                // Extensions non prioritaires
                
                foreach (string file in files.Where(file => !ExtensionsPriori.ExtPriorite(file)))
                {
                    long fileSize = new FileInfo(file).Length;
                    if (fileSize > limite * 1024)
                    {
                        semaphore.Wait();
                        try
                        {
                            string destinationPath = Path.Combine(NewSauvegarder.RepCible, Path.GetFileName(file));
                            File.Copy(file, destinationPath, true);
                        }
                        finally { semaphore.Release(); }
                    }
                    else
                    {
                        string destinationPath = Path.Combine(NewSauvegarder.RepCible, Path.GetFileName(file));
                        File.Copy(file, destinationPath, true);
                    }
                }
                
            }
            string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
            //Console.WriteLine(ExecuteFileSize);
            string output2 = PowerShellHandler.Command(ExecuteFileSize);
            //Console.WriteLine("*******************************");
            long FileSize = long.Parse(output2);
            etatTempsReel.SaveToJson(NewSauvegarder.RepSource, NewSauvegarder.RepCible, i, cancellationTokenSource);
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
            JsonLogger jsonLogger = JsonLogger.Instance;
            jsonLogger.Log(log1, pathcomplete);

            //Console.WriteLine($"Log created in: {pathcomplete}");
            return;

        }

        public void EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSauvegarder, int log, CancellationTokenSource cancellationTokenSource)
        {
            string filePath = GlobalVariables.Dir + "limite.txt";
            int limite = Convert.ToInt32(File.ReadAllText(filePath));
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            int FileModifie = 0;
            int FileSize = 0;
            int FileProcessed = 0;
            if (Directory.Exists(NewSauvegarder.RepSource) && Directory.Exists(NewSauvegarder.RepCible))
            {
                string[] sourceFiles = Directory.GetFiles(NewSauvegarder.RepSource);
                int totalFiles = sourceFiles.Length;

                //Extensions priorite
                foreach (string sourceFilePath in sourceFiles.Where(file => ExtensionsPriori.ExtPriorite(file)))
                {
                    string fileName = Path.GetFileName(sourceFilePath);
                    string targetFilePath = Path.Combine(NewSauvegarder.RepCible, fileName);

                    if (File.Exists(targetFilePath))
                    {
                        DateTime sourceLastModified = File.GetLastWriteTime(sourceFilePath);
                        DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);
                        string stopPath = @"C:\LOGJ\stop.txt";
                        while (File.ReadAllText(stopPath).Equals("go"))
                        {

                            if (sourceLastModified < targetLastModified)
                            {
                                long fileSize = new FileInfo(sourceFilePath).Length;
                                if (fileSize > limite * 1024)
                                {
                                    semaphore.Wait();
                                    try
                                    {
                                        File.Copy(sourceFilePath, targetFilePath, true);
                                        FileSize += (int)fileSize;
                                        FileModifie++;
                                    }
                                    finally { semaphore.Release(); }
                                }
                                else
                                {
                                    File.Copy(sourceFilePath, targetFilePath, true);
                                    FileSize += (int)fileSize;
                                    FileModifie++;
                                }
                            }
                        }
                    }
                    else
                    {
                        long fileSize = new FileInfo(sourceFilePath).Length;
                        if (fileSize > limite * 1024)
                        {
                            semaphore.Wait();
                            try
                            {
                                File.Copy(sourceFilePath, targetFilePath);
                                FileSize += (int)fileSize;
                                FileModifie++;
                            }
                            finally { semaphore.Release(); }
                        }
                        else
                        {
                            File.Copy(sourceFilePath, targetFilePath);
                            FileSize += (int)fileSize;
                            FileModifie++;
                        }
                    }
                    int FilesRestant = totalFiles - FileProcessed - FileModifie;
                    FileProcessed++;

                }
                foreach (string sourceFilePath in sourceFiles.Where(file => !ExtensionsPriori.ExtPriorite(file)))
                {
                    string fileName = Path.GetFileName(sourceFilePath);
                    string targetFilePath = Path.Combine(NewSauvegarder.RepCible, fileName);

                    if (File.Exists(targetFilePath))
                    {
                        DateTime sourceLastModified = File.GetLastWriteTime(sourceFilePath);
                        DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);
                        string stopPath = @"C:\LOGJ\stop.txt";
                        while (File.ReadAllText(stopPath).Equals("go"))
                        {

                            if (sourceLastModified < targetLastModified)
                            {
                                long fileSize = new FileInfo(sourceFilePath).Length;
                                if (fileSize > limite * 1024)
                                {
                                    semaphore.Wait();
                                    try
                                    {
                                        File.Copy(sourceFilePath, targetFilePath, true);
                                        FileSize += (int)fileSize;
                                        FileModifie++;
                                    }
                                    finally { semaphore.Release(); }
                                }
                                else
                                {
                                    File.Copy(sourceFilePath, targetFilePath, true);
                                    FileSize += (int)fileSize;
                                    FileModifie++;
                                }
                            }
                        }
                    }
                    else
                    {
                        long fileSize = new FileInfo(sourceFilePath).Length;
                        if (fileSize > limite * 1024)
                        {
                            semaphore.Wait();
                            try
                            {
                                File.Copy(sourceFilePath, targetFilePath);
                                FileSize += (int)fileSize;
                                FileModifie++;
                            }
                            finally { semaphore.Release(); }
                        }
                        else
                        {
                            File.Copy(sourceFilePath, targetFilePath);
                            FileSize += (int)fileSize;
                            FileModifie++;
                        }
                    }
                    int FilesRestant = totalFiles - FileProcessed - FileModifie;
                    FileProcessed++;

                }
            }
            //string output = PowerShellHandler.Command(Execute);
            etatTempsReel.SaveToJsonDiff(NewSauvegarder.RepSource, NewSauvegarder.RepCible, i, cancellationTokenSource);
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

                //Console.WriteLine($"Json created in: {pathcomplete}");
            return;
        }
    }
}
