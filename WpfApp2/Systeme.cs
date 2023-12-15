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

        public void EnregistrerSauvegardeAsync(int i, TravailSauvegarde NewSauvegarder, int log)
        {
            // Create a new thread and start it with the EnregistrerSauvegarde method
            Thread thread = new Thread(() => EnregistrerSauvegarde(i, NewSauvegarder, log, cancellationTokenSource));
            thread.Start();
        }

        public void EnregistrerSauvegarde(int i, TravailSauvegarde NewSauvegarder, int log, CancellationTokenSource cancellationTokenSource)
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
            string jsonString = JsonSerializer.Serialize(log1);
            using (StreamWriter sw = File.AppendText(pathcomplete))
            {
                sw.WriteLine(jsonString);
            }

            //Console.WriteLine($"Log created in: {pathcomplete}");
            return;

        }
        public void EnregistrerSauvegardeDiffAsync(int i, TravailSauvegarde NewSauvegarder, int log)
        {
            // Create a new thread and start it with the EnregistrerSauvegardeDiff method
            Thread thread = new Thread(() => EnregistrerSauvegardeDiff(i, NewSauvegarder, log, cancellationTokenSource));
            thread.Start();
        }
        public void EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSauvegarder, int log, CancellationTokenSource cancellationTokenSource)
        {
            EtatTempsReel etatTempsReel = new EtatTempsReel();
            Console.WriteLine("************************");
            Console.WriteLine("***Project Easy Save ***");
            Console.WriteLine("************************");
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

                foreach (string sourceFilePath in sourceFiles)
                {
                    string fileName = Path.GetFileName(sourceFilePath);
                    string targetFilePath = Path.Combine(NewSauvegarder.RepCible, fileName);

                    if (File.Exists(targetFilePath))
                    {
                        DateTime sourceLastModified = File.GetLastWriteTime(sourceFilePath);
                        DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);

                        if (sourceLastModified < targetLastModified)
                        {
                            File.Copy(sourceFilePath, targetFilePath, true);
                            Console.WriteLine($"Le fichier {fileName} a été mis à jour dans le répertoire cible.");
                            Console.WriteLine($"File {fileName} was updated in target directory.");
                            FileInfo fileInfo = new FileInfo(sourceFilePath);
                            FileSize += (int)fileInfo.Length;
                            FileModifie++;
                        }
                    }
                    else
                    {
                        File.Copy(sourceFilePath, targetFilePath);
                        Console.WriteLine($"Le fichier {fileName} a été ajouté au répertoire cible.");
                        Console.WriteLine($"File {fileName} was added in target directory.");
                        FileInfo fileInfo = new FileInfo(sourceFilePath);
                        FileSize += (int)fileInfo.Length;
                        FileModifie++;
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
