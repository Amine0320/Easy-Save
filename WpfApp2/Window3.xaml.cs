using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Log(object sender, RoutedEventArgs e)
        {
            DateTime date1 = DateTime.Now;
            string TodayDateForString = date1.ToString("yyyy-MM-dd");
            string path = @"C:/LOGJ/" + TodayDateForString;
            if (File.Exists(path + ".json"))
            { Process.Start("notepad.exe", path + ".json"); }
            if (File.Exists(path + ".xml"))
            { Process.Start("notepad.exe", path + ".xml"); }

        }

        private void Button_Etat(object sender, RoutedEventArgs e)
        {
            string path = @"C:/LOGJ/state.json";
            if (File.Exists(path))
            { Process.Start("notepad.exe", path); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            File.Delete("C:/LOGJ/state3.json");
            Application.Current.Shutdown();
        }
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "End of backups";
            VoirLog.Content = "Open Log";
            VoirEtat.Content = "Open Status";
            ButtonSortir.Content = "Exit";
  
            //Fenetre.Show();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "Fin des sauvegardes";
            VoirLog.Content = "Ouvrir Log";
            VoirEtat.Content = "Ouvrir Etat";
            ButtonSortir.Content = "Quitter";
            //Fenetre.Show();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SauvReu.Content = "Fin de las copias de seguridad";
            VoirLog.Content = "Abrir Log";
            VoirEtat.Content = "Abrir Estado";
            ButtonSortir.Content = "Salir";
            //Fenetre.Show();
        }
    }
}
