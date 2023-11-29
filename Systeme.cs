
using PowershellShowcase;

public class Systeme
{
    public static List<int> SauvDejaCreee = new List<int>();

    private int IdSys = 0;

    public static bool VerifieDispo(HashSet<int> list)
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

    public TravailSauvegarde? CreerSauvegarde(int save, string sources, string cibles, string type)
    {
        TravailSauvegarde NewSauvegarder = new TravailSauvegarde();
        NewSauvegarder.RepSource = sources;
        NewSauvegarder.RepCible = cibles;
        //Console.WriteLine("************************");
        //Console.WriteLine("***Project Easy Save ***");
        //Console.WriteLine("************************");
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