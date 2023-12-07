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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process procesus = new Process();
            procesus.StartInfo.FileName= @"C:\LOGJ\2023-11-30.json";
            procesus.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            SauvReu.Content = "Guardado Exitoso";
            VoirLog.Content = "Abrir Log";
            VoirEtat.Content = "Abrir Estado";
            ButtonSortir.Content = "Salir";
            //Fenetre.Show();
        }
    }
}
