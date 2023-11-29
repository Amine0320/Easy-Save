
using PowershellShowcase;

public class MethodesEnregistre
{
    public static int EnregistrerSauvegardeDiff(int i, TravailSauvegarde NewSave)
    {
        int FileSize = 0;
        if (Directory.Exists(NewSave.RepSource) && Directory.Exists(NewSave.RepCible))
        {
            string[] sourceFiles = Directory.GetFiles(NewSave.RepSource);

            foreach (string sourceFilePath in sourceFiles)
            {
                string fileName = Path.GetFileName(sourceFilePath);
                string targetFilePath = Path.Combine(NewSave.RepCible, fileName);

                if (File.Exists(targetFilePath))
                {
                    DateTime sourceLastModified = File.GetLastWriteTime(sourceFilePath);
                    DateTime targetLastModified = File.GetLastWriteTime(targetFilePath);

                    if (sourceLastModified > targetLastModified)
                    {
                        File.Copy(sourceFilePath, targetFilePath, true);
                        Console.WriteLine($"Le fichier {fileName} a été mis à jour dans le répertoire cible.");
                        Console.WriteLine($"File {fileName} was updated in target repertory.");
                        FileInfo fileInfo = new FileInfo(sourceFilePath);
                        FileSize += (int)fileInfo.Length;
                    }
                }
                else
                {
                    File.Copy(sourceFilePath, targetFilePath);
                    Console.WriteLine($"Le fichier {fileName} a été ajouté au répertoire cible.");
                    Console.WriteLine($"File {fileName} was added in target repertory.");
                    FileInfo fileInfo = new FileInfo(sourceFilePath);
                    FileSize += (int)fileInfo.Length;
                }
            }

            Console.WriteLine($"Opération terminée pour la sauvegarde " + i.ToString() + ".");
            Console.WriteLine($"Save " + i.ToString() + "operation is finished.");
        }
        else
        {
            Console.WriteLine($"Le répertoire source ou le répertoire cible n'existe pas pour la sauvegarde " + i.ToString() + ".");
            Console.WriteLine($"The target or source repertory doesn't exist for Save " + i.ToString() + ".");
        }
        return FileSize;
    }

    public static int EnregistrerSauvegardeComp(int i, TravailSauvegarde NewSauvegarder)
    {
        string Execute = @"Copy-Item -Path " + NewSauvegarder.RepSource + " -Destination " + NewSauvegarder.RepCible + " -Recurse -Force";
        string ExecuteFileSize = @"(Get-ChildItem -Path " + NewSauvegarder.RepSource + " -Recurse | Measure-Object -Property Length -Sum).Sum";
        string output2 = PowerShellHandler.Command(ExecuteFileSize);
        int FileSize = int.Parse(output2);
        string output = PowerShellHandler.Command(Execute);
        return FileSize;
        
    }
}


public class Systeme
{
    public static List<int> SauvDejaCreee = new List<int>();

    private int IdSys = 0;

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

    public TravailSauvegarde? CreerSauvegarde(int i, string saves, string sources, TypeSauv Type)
    {

        TravailSauvegarde NewSauvegarder = new TravailSauvegarde();
        NewSauvegarder.NomTDS = "Save" + i.ToString();
        NewSauvegarder.RepSource = saves;
        NewSauvegarder.RepCible = sources;
        NewSauvegarder.Type = Type;
        return NewSauvegarder;
    }


    public void EnregistrerSauvegarde(TravailSauvegarde NewSauvegarder)
    {
        int FileSize = 0;
        string dateString1 = DateTime.Now.ToString("yyyyMMdd_HHmm");
        var dateString2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DateTime date1 = DateTime.Now;
        string TodayDateForString = date1.ToString("yyyy-MM-dd");            
        string str = NewSauvegarder.NomTDS.Split('e')[1];
        int i = int.Parse(str);
        if (NewSauvegarder.Type == TypeSauv.Complete) 
        {
            FileSize = MethodesEnregistre.EnregistrerSauvegardeComp(i, NewSauvegarder);
        }
        else 
        { 
            FileSize = MethodesEnregistre.EnregistrerSauvegardeDiff(i, NewSauvegarder); 
        }
        DateTime date2 = DateTime.Now;
        TimeSpan soustraction = date2 - date1;
        double Secondssoustraction = soustraction.TotalSeconds;
        LogJournalier log1 = new LogJournalier(i.ToString(), "Journal " + i.ToString(), NewSauvegarder.RepSource, NewSauvegarder.RepCible, FileSize, Secondssoustraction, dateString2);
        string fichier = @"C:\LOGJ";
        string nombrefichier = TodayDateForString + ".json";
        string pathcomplete = Path.Combine(fichier, nombrefichier);
        string jsonString = JsonSerializer.Serialize(log1);
        using (StreamWriter sw = File.AppendText(pathcomplete))
        {
            sw.WriteLine(jsonString);
        }

        Console.WriteLine($"Json created in: {pathcomplete}");

    }
}
