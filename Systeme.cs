using System;
using PowershellShowcase;

using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using EasySaveProSoft.Version1;
using System.Diagnostics;
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
            if (i > 5 | i < 1)
            {
                throw new Exception("La sauvegarde" + i.ToString() + "ne peut pas etre utilisée./ The save" + i.ToString() + "cannot be used");
            }
        }
        if (list.Count + SauvDejaCreee.Count >= 5)
        {
            throw new Exception("Tous les travaux de sauvegarde ont déjà été utilisés/ All the save spaces are used");
        }
        return true;
    }

    public TravailSauvegarde? CreerSauvegarde(int i,string saves, string sources, TypeSauv Type)
    {
        
        TravailSauvegarde NewSauvegarder = new TravailSauvegarde();
        NewSauvegarder.NomTDS = "Save" + i.ToString() ;
        NewSauvegarder.RepSource = saves;
        NewSauvegarder.RepCible = sources;
        NewSauvegarder.Type = Type;
        return NewSauvegarder;
    }

    public void EnregistrerSauvegarde(int i, TravailSauvegarde NewSauvegarder)
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
        etatTempsReel.SaveToJson(NewSauvegarder.RepSource, NewSauvegarder.RepCible,i);
        Console.WriteLine("***copie réussie ***");
        DateTime date2 = DateTime.Now;
        TimeSpan soustraction = date2 - date1;
        double Secondssoustraction = soustraction.TotalSeconds;
        LogJournalier log1 = new LogJournalier(1, "Journal "+i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2);
        string fichier = @"C:\LOGJ";
        string nombrefichier = TodayDateForString + ".json";
        string pathcomplete = Path.Combine(fichier, nombrefichier);
        string jsonString = JsonSerializer.Serialize(log1);
        using (StreamWriter sw = File.AppendText(pathcomplete))
        {
            sw.WriteLine(jsonString);
        }
            
        Console.WriteLine($"Json created in: {pathcomplete}");
        return;

    }
    public void EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSauvegarder)
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
        etatTempsReel.SaveToJsonDiff(NewSauvegarder.RepSource, NewSauvegarder.RepCible,i);
        Console.WriteLine("***copie réussie ***");
        DateTime date2 = DateTime.Now;
        TimeSpan soustraction = date2 - date1;
        double Secondssoustraction = soustraction.TotalSeconds;
        LogJournalier log1 = new LogJournalier(1, "Journal " + i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2);
        string fichier = @"C:\LOGJ";
        string nombrefichier = TodayDateForString + ".json";
        string pathcomplete = Path.Combine(fichier, nombrefichier);
        string jsonString = JsonSerializer.Serialize(log1.Consulter());
        using (StreamWriter sw = File.AppendText(pathcomplete))
        {
            // sw.WriteLine(jsonString);
        }

        Console.WriteLine($"Json created in: {pathcomplete}");
        return;

    }
    public void ActivePowershell()
    {
        string DossierPro = Directory.GetCurrentDirectory();
        string[] ListDossier = DossierPro.Split(@"\");
        string Dir = "";
        for (int k = 0; k < ListDossier.Length - 3; k++)
        {
            Dir += ListDossier[k] + @"\";
        }
        string scriptPath = Dir + "ExecuteActivate.ps1";
        // Ruta al archivo de script PowerShell (.ps1)
        

        // Configurar la información del proceso
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
            Verb = "runas", // Solicitar elevación de privilegios
            UseShellExecute = true,
        };

        try
        {
            // Iniciar el proceso
            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al iniciar el script: {ex.Message}");
        }

    }
}