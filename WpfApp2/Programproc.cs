using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowershellShowcase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.Services.Description;

namespace WpfApp2
{
    public class Programproc
    {
        public void EventMain(string @Sources2,string @Cible2, string Type2, string saves2, string TypeLog)
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

            string saves = saves2;
            List<int> listeDeSauvegardes = ListeDeSauv(saves);
            Systeme TravailNouvelle = new Systeme();
            if (Systeme.VerifieDispo(listeDeSauvegardes))
            {
                foreach (int i in listeDeSauvegardes)
                {

                    string Sources = Sources2;
                    string Cible = Cible2;
                    string Type = Type2;
                    int log = 2;
                    if (TypeLog.Equals("JSON")) { log = 1; }
                    TypeSauv sauvType = Convertir(Type);
                    GlobalVariables.Exist = LogMetier.CheckAppsInDirectory(Sources);
                    if (!GlobalVariables.Exist)
                    {
                        if (sauvType == TypeSauv.Complete)
                        {
                            TravailNouvelle.EnregistrerSauvegarde(i, TravailNouvelle.CreerSauvegarde(i, Sources, Cible, sauvType), log);
                        }
                        else { TravailNouvelle.EnregistrerSauvegardeDiff(i, TravailNouvelle.CreerSauvegarde(i, Sources, Cible, sauvType), log); }
                        Systeme.SauvDejaCreee.Add(i);
                    }
                }

            }
            string pathfichierActuelle = @"C:\LOGJ\state2.json";
            if (File.Exists(pathfichierActuelle))
            {
                File.Delete(rutaArchivo);
                File.Move(pathfichierActuelle, rutaArchivo);
            }


        }

        static List<int> ListeDeSauv(string sauv)
        {
            List<int> listeSauv = new List<int>();
            ArraySegment<char> arr = sauv.ToCharArray();
            int len = arr.Count;

            for (int i = 0; i < len; i++)
            {
                if (arr[i].Equals(';')) { }
                else if (arr[i].Equals('-'))
                {
                    for (int j = int.Parse(arr[i - 1].ToString()) + 1; j < int.Parse(arr[i + 1].ToString()); j++)
                    {
                        listeSauv.Add(j);
                    }
                }
                else { listeSauv.Add(int.Parse(arr[i].ToString())); }
            }
            return listeSauv;
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
        public static bool _Exist {get; set;} = false;
        public static bool Exist 
        {        
            get { return _Exist; }
            set { _Exist = value; }
        }
        public static string Dir {get; set;} = GetDir();
    }
}
