using System;
using PowershellShowcase;

using System.ComponentModel;
using System.Linq;
using System.Management.Automation;

public class Program
    {
    public static void Main()
    {

            Console.WriteLine("Le vrai Project");
            
            Console.WriteLine("Choississ ..../ Choice ...");
            string saves = Console.ReadLine();
            Console.WriteLine("Ou sont les fichiers ?/ Where is the files?");
            string sources = Console.ReadLine();
            Console.WriteLine("Ou mettre le fichiers? /Where you put the files?");
            string Cible = Console.ReadLine();
            Console.WriteLine("Choississ ... // ");
            string Type = Console.ReadLine();
            Systeme TravailNouvelle = new Systeme();
            TravailNouvelle.EnregistrerSauvegarde(TravailNouvelle.CreerSauvegarde(sources, Cible));
            
            
    }
    
    }


