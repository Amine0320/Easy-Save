using System;
using PowershellShowcase;

using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.IO;
using System.Text.Json;
public class Systeme
{
    private int IdSys = 0;

    public static bool VerifieDispo()
    {
        return false;
    }

    public TravailSauvegarde? CreerSauvegarde(string saves, string sources)
    {
        
        TravailSauvegarde NewSauvegarder = new TravailSauvegarde();
        NewSauvegarder.RepSource = saves;
        NewSauvegarder.RepCible = sources;
        
        return NewSauvegarder;
    }

    public void EnregistrerSauvegarde(TravailSauvegarde NewSauvegarder)
    {
        Console.WriteLine("************************");
        Console.WriteLine("***Project Easy Save ***");
        Console.WriteLine("************************");
        string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
        var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ///Console.WriteLine(dateString2);
        //string newfichier = "TravailSauvegarde" + dateString1;
        //DateTime now = DateTime.Now; 
        DateTime date1 = DateTime.Now;
        string TodayDateForString = date1.ToString("yyyy-MM-dd");
        //int hours = Fecha1.Hour;
        //int minutes = Fecha1.Minute;
        //int seconds = Fecha1.Second;  
        // Console.WriteLine(hours + minutes + seconds); 
        // Write the hours, minutes, and seconds to the console
        // Console.WriteLine($"Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}");
        //string InfoFecha = PowerShellHandler.Command(Fecha1);
        //Console.WriteLine("aqui toy");
        //Console.WriteLine(InfoFecha);
        /// First Execution Create file 
        //string Execute1 = @"New-Item" C:\Repository2\" + newfichier + " -Type Directory";
        //Console.WriteLine(Execute1);
        //string output2 = PowerShellHandler.Command(Execute1);
        //Console.WriteLine(output2);
        /// Console.WriteLine(NewSauvegarder.RepSource);
        ///Console.WriteLine(NewSauvegarder.RepCible);
        /// Execution of Backup
        string Execute = @"Copy-Item -Path " + NewSauvegarder.RepSource + " -Destination " + NewSauvegarder.RepCible + " -Recurse -Force";
        string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
        string output2 = PowerShellHandler.Command(ExecuteFileSize);
        ///Console.WriteLine(output2);
        Console.WriteLine("*******************************");
        int FileSize = int.Parse(output2) ; 
        ///Console.WriteLine(Execute);
        string output = PowerShellHandler.Command(Execute);
        Console.WriteLine("***copie réussie ***");
        DateTime date2 = DateTime.Now;
        TimeSpan soustraction = date2 - date1;
        double Secondssoustraction = soustraction.TotalSeconds;
        //int FileTransferTime = (int.Parse(InfoFecha2) - int.Parse(InfoFecha)); 
        //string SubsTime = @"New-TimeSpan -Start "+ InfoFecha + " -End "+ InfoFecha2;
        //Console.WriteLine(SubsTime);
        //string output3 = PowerShellHandler.Command(SubsTime);
        LogJournalier log1 = new LogJournalier(1, "Journal 1", NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2);
        //Console.WriteLine(log1.Consulter());
        string fichier = @"C:\LOGJ";
        string nombrefichier = TodayDateForString + ".json";
        string pathcomplete = Path.Combine(fichier, nombrefichier);
        string jsonString = JsonSerializer.Serialize(log1);
        File.WriteAllText(pathcomplete, jsonString);
        Console.WriteLine($"Json created in: {pathcomplete}");
        return;

    }
}