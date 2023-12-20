using System.Text;
using System.IO;
using Programme_cryptosoft;
using System.Diagnostics;
using System.IO.Pipes;

namespace WpfApp2
{
    public class Programproc
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        public async void EventMain(CancellationTokenSource cancellationTokenSource, string @Sources2,string @Cible2, string Type2, int saves2, string TypeLog, int ext)
        {

            
            string rutaArchivo = @"C:\LOGJ\state.json";
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    // Borrar el archivo
                    File.Delete(rutaArchivo);
                }
            }
            
            catch (Exception ex)
            {}
            
            Systeme TravailNouvelle = new Systeme(); 
            string Sources = Sources2;
            string Cible = Cible2;
            string Type = Type2;
            int log = 2;
            if (TypeLog.Equals("JSON")) { log = 1; }
            TypeSauv sauvType = Convertir(Type);
            string selectedExtensionsInput = ext.ToString();
            List<int> selectedExtensions = selectedExtensionsInput.Split(',')
            .Select(part => int.Parse(part.Trim()))
            .ToList();

            if (ext != 0)
            {
                // Lancer le programme CryptoSoft avec les arguments nécessaires
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "Programme cryptosoft.exe",
                    Arguments = $"{Sources} {Cible} {selectedExtensionsInput}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                ProgramCryptoSoft cryptoSoft = new ProgramCryptoSoft();
                var pipeClient = new NamedPipeClientStream("CryptoSoftPipe");
                cryptoSoft.Main();
                try
                {
                    pipeClient.Connect();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to CryptoSoftPipe: {ex.Message}");
                    return; // Arrêter le processus si la connexion échoue
                }

                try
                {
                    using (StreamReader sr = new StreamReader(pipeClient))
                    using (StreamWriter sw = new StreamWriter(pipeClient, Encoding.UTF8))
                    {



                        // Lire la clé du tube nommé
                        string cleString = sr.ReadLine();
                        byte[] cle = Convert.FromBase64String(cleString);
                        Task.Delay(10000).Wait();
                        // Appeler la fonction de chiffrement dans le programme CryptoSoft
                        //  ProgramCryptoSoft cryptoSoft = new ProgramCryptoSoft();

                        int returnCode = cryptoSoft.ChiffrerDossier(Sources, Cible, cle, selectedExtensions);
                        Console.WriteLine(returnCode == 1 ? "Encryption successful." : $"Encryption failed. Error code: {returnCode}");


                    }
                }
                catch (IOException ex)
                {
                    // Handle the exception
                }
            }
                    
                    
                    if (sauvType == TypeSauv.Complete)
                    {
                        TravailNouvelle.EnregistrerSauvegarde(saves2, TravailNouvelle.CreerSauvegarde(saves2, Sources, Cible, sauvType), log, cancellationTokenSource);
                    }
                    else { TravailNouvelle.EnregistrerSauvegardeDiff(saves2, TravailNouvelle.CreerSauvegarde(saves2, Sources, Cible, sauvType), log,cancellationTokenSource); }
                    Systeme.SauvDejaCreee.Add(saves2);
                    

            
        }


        static TypeSauv Convertir(string Type)
        {
            if (Type.Equals("1"))
            {
                return TypeSauv.Complete;
            }
            else if (Type.Equals("2"))
            {
                return TypeSauv.Differentielle;
            }
            else { throw new Exception("N'est pas une option"); }
        }
    }

    public class GlobalVariables
    {   
        Systeme systeme = new Systeme();
        private static string GetDir() 
            {            
                string DossierPro = Directory.GetCurrentDirectory();
                string[] ListDossier = DossierPro.Split(@"\");
                string Dir = "";
                for (int k = 0; k < ListDossier.Length - 3; k++ ) 
                {
                    Dir += ListDossier[k] + @"\";
                }
                return Dir;
            }
        public static bool _Active { get; set; } = false;
        public static bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public static string Dir {get; set;} = GetDir();

        public static List<Task> tasks = new List<Task>();

        public static int number = 1;
    }
}
