using System;
//using Project2;
using System;
using System.ComponentModel;
using System.Linq;

static void Main()
{


    Console.WriteLine("Choississ ..../ Choice ...");
    string saves = Console.ReadLine();
    Console.WriteLine("Ou sont les fichiers ?/ Where is the files?");
    string sources = Console.ReadLine();
    Console.WriteLine("Ou mettre le fichiers? /Where you put the files?");
    string Cible = Console.ReadLine();
    Console.WriteLine("Choississ ... // ");
    string Type = Console.ReadLine();
    Systeme TravailNouvelle = new Systeme();
    TravailNouvelle.CreerSauvegarde(sources, Cible);

}