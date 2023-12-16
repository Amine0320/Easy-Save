﻿using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string pathfichier = @"C:\LOGJ\stop.txt";

            string content = "stop";

            try
            {
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

            }
            catch (Exception ex)
            {}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Fenetre = new Window1();
            this.Close();
            Fenetre.Show();
        }
        private void Button_Parameters(object sender, RoutedEventArgs e)
        {
            ParaWindow Fenetre = new ParaWindow();
            this.Close();
            Fenetre.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            this.Close();
            Application.Current.Shutdown();
            //Fenetre.Show();
        }
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "WELCOME TO OUR BACKUP SYSTEM! BY CESI INFO A3 COMPUTER SCIENCE";
            ButtonQuit.Content = "Quit";
            ButtonSave.Content = "Save";
            Parameters.Content = "Settings";
            //Fenetre.Show();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "BIENVENUE A NOTRE SYSTEME DE SAUVEGARDE! BY CESI INFO A3 INFORMATIQUE";
            ButtonQuit.Content = "Quitter";
            Parameters.Content = "Paramètres";
            ButtonSave.Content = "Enregister";
            //Fenetre.Show();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "¡BIENVENIDO A NUESTRO SISTEMA DE RESPALDO! POR CESI INFO A3 INFORMATICA";
            ButtonQuit.Content = "Salir";
            ButtonSave.Content = "Guardar";
            Parameters.Content = "Parámetros";
            //Fenetre.Show();
        }
        private void Button_Arab(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content =  "CESI A3"+ "مرحبا بكم في نظام النسخ الاحتياطي! من فريق معلوماتية ";
            ButtonQuit.Content = "خروج";
            ButtonSave.Content = "نسخ";
            Parameters.Content = "إعدادات";
            //Fenetre.Show();
        }
    }
}
