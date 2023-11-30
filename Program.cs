using System;
using System.ComponentModel;
using System.Linq;
using System.IO;


static void Main()
{
    Console.WriteLine("************************");
    Console.WriteLine("***Project Easy Save ***");
    Console.WriteLine("************************");
    Console.WriteLine("Choisissez les sauvegardes à effectuer (sous format '1-3' ou '1;3')") ;
    Console.WriteLine("Choose which saves to execute (using the template  '1-3' or '1; 3')") ;
    string saves = Console.ReadLine();
    List<int> listeDeSauvegardes = ListeDeSauv(saves);
    Systeme TravailNouvelle = new Systeme();
    if (!Systeme.VerifieDispo(listeDeSauvegardes)) 
        {
        Console.WriteLine("Pas Possible"); 
        }
    else
    {
        foreach (int i in listeDeSauvegardes)
        {
            Console.WriteLine("");
            Console.WriteLine("Entrez les paramètres de la sauvegarde " + i.ToString());
            Console.WriteLine("Enter the parametres of save " + i.ToString());
            Console.WriteLine("");
            Console.WriteLine("Où sont les fichiers à savegarder?");
            Console.WriteLine("Where are the files to save?");
            string Sources = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Où sauvegarder les fichiers?");
            Console.WriteLine("Where should the files be saved?");
            string Cible = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Choisis la méthode de sauvegarde :");
            Console.WriteLine("1.Complet");
            Console.WriteLine("2.Differentiel");
            Console.WriteLine("Choose the save method :");
            Console.WriteLine("1.Complete");
            Console.WriteLine("2.Differential");
            string Type = Console.ReadLine();
            TypeSauv sauvType = Convertir(Type);
            TravailNouvelle.EnregistrerSauvegarde(TravailNouvelle.CreerSauvegarde(i, Sources, Cible, sauvType));
            Systeme.SauvDejaCreee.Add(i);
        }
    }
}

static List<int> ListeDeSauv(string sauv)
{
    List<int> listeSauv = [];
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

Main();