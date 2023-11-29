using System.IO;
using System.Management.Automation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowershellShowcase 
{
public class LogJournalier
{
    public int IdLogJourn { get; set; }
    public string NomLj { get; set; }
    public string FileSource { get; set; }
    public string FileTarget { get; set; }
    public int FileSize { get; set; }
    public double FileTransferTime { get; set; }
    public string Time { get; set; }

    // Recuperer les valeurs des attribus de class 
    public LogJournalier(int idLogJourn, string nomLj, string fileSource, string fileTarget, int fileSize, double fileTransferTime, string time)
    {
        this.IdLogJourn = idLogJourn;
        this.NomLj = nomLj;
        this.FileSource = fileSource;
        this.FileTarget = fileTarget;
        this.FileSize = fileSize;
        this.FileTransferTime = fileTransferTime;
        this.Time = time;
    }
    //Methode Modifier qui existe dans le Travail de sauvergarde est sera herite par LogJournalier
    public void Modifier(int idLogJourn, string nomLj, string fileSource, string fileTarget, int fileSize, double fileTransferTime, string time)
    {
        this.IdLogJourn = idLogJourn;
        this.NomLj = nomLj;
        this.FileSource = fileSource;
        this.FileTarget = fileTarget;
        this.FileSize = fileSize;
        this.FileTransferTime = fileTransferTime;
        this.Time = time;
    }
    // Methode Consulter qui existe dans le Travail de sauvergarde est sera herite par LogJournalier
    // A verifier 
    public string Consulter()
    {
            
            return $"Id du journal : {this.IdLogJourn}\n" +
                $"Nom du journal : {this.NomLj}\n" +
                $"Fichier source : {this.FileSource}\n" +
                $"Fichier cible : {this.FileTarget}\n" +
                $"Taille du fichier : {this.FileSize}\n" +
                $"Temps de transfert du fichier : {this.FileTransferTime}\n" +
                $"Heure : {this.Time}";
        }
} 
}