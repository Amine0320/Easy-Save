﻿using PowershellShowcase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.Services.Description;


static void Main()
{
    string rutaArchivo = @"C:\LOGJ\state.json";
    try
    {
        if (File.Exists(rutaArchivo))
        {
            // Borrar el archivo
            File.Delete(rutaArchivo);
        }
        else
        {
            
        }
    }
    catch (Exception ex)
    {
        
    }
	Systeme TravailNouvelle = new Systeme();
	TravailNouvelle.ActivePowershell();
    Console.WriteLine("************************");
    Console.WriteLine("***Project Easy Save ***");
    Console.WriteLine("************************");
    Console.WriteLine("Choisissez les sauvegardes à effectuer (sous format '1-3' ou '1;3')");
    Console.WriteLine("Choose which saves to execute (using the template  '1-3' or '1; 3')");
    string saves = Console.ReadLine();
    List<int> listeDeSauvegardes = ListeDeSauv(saves);
    
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
            if (sauvType == TypeSauv.Complete)
            {
                TravailNouvelle.EnregistrerSauvegarde(i, TravailNouvelle.CreerSauvegarde(i, Sources, Cible, sauvType));
            } else { TravailNouvelle.EnregistrerSauvegardeDiff(i, TravailNouvelle.CreerSauvegarde(i, Sources, Cible, sauvType)); }
                Systeme.SauvDejaCreee.Add(i);
        }

    }
    string pathfichierActuelle = @"C:\LOGJ\state2.json";
    string Nouvnomficchier = @"C:\LOGJ\state.json";
    File.Delete(Nouvnomficchier);
    File.Move(pathfichierActuelle, Nouvnomficchier);
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

Main();