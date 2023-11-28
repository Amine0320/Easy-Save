
using PowershellShowcase;
using System.Management.Automation;

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
        Console.WriteLine("************************");
        Console.WriteLine("***Project Easy Save ***");
        Console.WriteLine("************************");
        string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
        //string newfichier = "TravailSauvegarde" + dateString1;
        /// First Execution Create file 
        //string ejecutar1 = @"New-Item" C:\Repository2\" + newfichier + " -Type Directory";
        //Console.WriteLine(ejecutar1);
        //string output2 = PowerShellHandler.Command(ejecutar1);
        //Console.WriteLine(output2);


        /// Execution of Backup
        string ejecutar = @"Copy-Item -Path " + NewSauvegarder.RepSource + " -Destination " + NewSauvegarder.RepCible + " -Force";
        string output = PowerShellHandler.Command(ejecutar);

        return NewSauvegarder;
    }

    public void EnregistrerSauvegarde(TravailSauvegarde NewSauvegarder)
    {
        return;

    }
}