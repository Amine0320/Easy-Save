using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string path = @"C:\LOGJ\quant.txt";
            if (File.Exists(path))
            {
                Debut.IsReadOnly = true;
                Fin.IsReadOnly = true;
                Option1.Text = "";
                Option1.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = @"C:\LOGJ\quant.txt";
            int quan;
            if (Option1.Text.ToString()=="-")
            {
                 quan = int.Parse(Fin.Text) - int.Parse(Debut.Text) + 1;
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(quan);
                    writer.Close();
                }
                } 
            else if (Option1.Text.ToString() == ";")
            {
                if (Fin.Text.Equals(""))
                {
                    quan = 1;
                }
                else 
                {
                    quan = 2;
                }
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(quan);
                    writer.Close();
                }
            }
            


            Programproc program = new Programproc();
            program.EventMain(Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), Debut.Text.ToString() + Option1.Text.ToString() + Fin.Text.ToString(), TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString())) ;
            Window2 Fenetre2 = new Window2();
            this.Close();
            Fenetre2.Show();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            Application.Current.Shutdown();
            
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Dt_tick;
            timer.Start();
        }
        private int incremet = 0;
        private void Dt_tick(object sender, EventArgs e)
        {
            incremet ++;
            if (Directory.Exists(Source.Text.ToString()))
            {
                LabelSource.Background = new SolidColorBrush(Colors.Green); 
            }else
            {
                LabelSource.Background = new SolidColorBrush(Colors.Red);
            }
            if (Directory.Exists(Cible.Text.ToString()))
            {
                LabelCible.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                LabelCible.Background = new SolidColorBrush(Colors.Red);
            }
        }
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "Source";
            CibleLabel.Content = "Destination";
            TypeLogLabel.Content = "LogType";
            TypeSauvLabel.Content = "LogSav";
            NombLabel.Content = "Number of Backups";
            ExtLabel.Content = "Extensions to encrypt";
            ButtonCommence.Content = "Start";
            ButtonQuit.Content = "Quit";
            //Fenetre.Show();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "Source";
            CibleLabel.Content = "Cible";
            TypeLogLabel.Content = "TypeLog";
            TypeSauvLabel.Content = "TypeSauv";
            NombLabel.Content = "Nombres de Sauvegardes";
            ExtLabel.Content = "Extensions à chiffrer";
            ButtonCommence.Content = "Commence";
            ButtonQuit.Content = "Quitter";
            //Fenetre.Show();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "Origen";
            CibleLabel.Content = "Destino";
            TypeLogLabel.Content = "TipoLog";
            TypeSauvLabel.Content = "TipoGuar";
            NombLabel.Content = "Cantidad de Salvaguardado";
            ExtLabel.Content = "Extensiones para cifrar";
            ButtonCommence.Content = "Comenzar";
            ButtonQuit.Content = "Salir";
            //Fenetre.Show();
        }
        private int GetExtension(string ext)
        {
            else if (ext.Equals(".txt")) { return 1; }
            else if (ext.Equals(".jpg")) { return 2; }
            else if (ext.Equals(".png")) { return 3; }
            else if (ext.Equals(".pdf")) { return 4; }
            else { return 5; }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
