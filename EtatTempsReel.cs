using System.Management.Automation;
using System.Text;

namespace PowershellShowcase;

public class EtatTempsReel
{
    private int IdEtatTemp;
    public string NomETR;
    public string SourceFilePath;
    public string TargetFilePath;
    public string State;
    public int TotalFilesToCopy;
    public int TotalFilesSize;
    public int NbFilesLeftToDo;
    public int Progression;
}