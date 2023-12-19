using System;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        //Constructor 
        public Window3()
        {
            InitializeComponent();
        }

        // Open the log file based on the current date 
        private void Button_Log(object sender, RoutedEventArgs e)
        {
            // Get the current date 
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");

            // Create the file path using the current date 
            string path = @"C:/LOGJ/" + TodayDateForString;

            // Open the log file if it exists in either JSON or XML format 
            if (File.Exists(path + ".json")) 
            { Process.Start("notepad.exe", path + ".json"); }
            if (File.Exists(path + ".xml"))
            { Process.Start("notepad.exe", path + ".xml"); }
            
        }

        // Open the status file
        private void Button_Etat(object sender, RoutedEventArgs e)
        {
            //Path to State File 
            string path = @"C:/LOGJ/state.json";

            // Open the status file if it exists 
            if (File.Exists(path))
            { Process.Start("notepad.exe", path); }
        }

        // Exit the application, delete temporary state file
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Close Window 3 
            this.Close();
            //Delete State File 
            File.Delete("C:/LOGJ/state3.json");
            //Close Application 
            Application.Current.Shutdown();
        }

        // Switch User Interface to English 
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "End of backups";
            VoirLog.Content = "Open Log";
            VoirEtat.Content = "Open Status";
            ButtonSortir.Content = "Exit";
  
            //Fenetre.Show();
        }
        // Switch User Interface to Frensh 
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "Fin des sauvegardes";
            VoirLog.Content = "Ouvrir Log";
            VoirEtat.Content = "Ouvrir Etat";
            ButtonSortir.Content = "Quitter";
            //Fenetre.Show();
        }
        // Switch User Interface to Espagnol 
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "Fin de las copias de seguridad";
            VoirLog.Content = "Abrir Log";
            VoirEtat.Content = "Abrir Estado";
            ButtonSortir.Content = "Salir";
            //Fenetre.Show();
        }
        // Switch User Interface to Arabic 
        private void Button_Arabic(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "نهاية عمليات النسخ الاحتياطي";
            VoirLog.Content = "فتح السجل";
            VoirEtat.Content = "فتح الحالة";
            ButtonSortir.Content = "خروج"; 
            //Fenetre.Show();
        } 
    }
}
