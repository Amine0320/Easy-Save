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
using System.Windows.Forms; 
 

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary> 
    public partial class Window1 : Window
    {
        Window4 window = new Window4(); 
        private int incremet = 0; 
        DispatcherTimer timer = new DispatcherTimer();
        public Window1()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Window4 Fenetre = new Window4();


            string path = @"C:\LOGJ\quant.txt";
            if (File.Exists(path))
            {
                Debut.IsReadOnly = true;
                Fin.IsReadOnly = true;
                Option1.Text = "";
                Option1.IsEnabled = false;
            }
        }
        // Liaison entre commence et le Window4 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window4 Fenetre = new Window4();
            Window1 Fenetre1 = new Window1();
            Fenetre.Show();

            // Utiliser une boucle while pour attendre que OuiClicked devienne vrai
            if (Fenetre.OuiClicked)
            {
                // Mise en veille pour éviter de bloquer le thread
                System.Threading.Thread.Sleep(100);
                Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Background);
                string path = @"C:\LOGJ\quant.txt";
                Programproc program = new Programproc();
                program.EventMainAsync(Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), Debut.Text.ToString() + Option1.Text.ToString() + Fin.Text.ToString(), TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
                Fenetre.Close();
                // Ouvrir la deuxième fenêtre 
                //Window2 Fenetre2 = new Window2();
                //Fenetre2.Show();   

            } 

        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        } 
        public  void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window4 Fenetre = new Window4();
            if (Fenetre.NonClicked)
            { 
                System.Threading.Thread.Sleep(100);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Dt_tick;
                timer.Start();
                if (pbConteo.Value == 100)
                {
                    timer.Stop();
                }
            } 
        }  
        public  void InitializeTimer()
        { 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Dt_tick;
            timer.Start(); 
            // Additional initialization logic if needed
        } 
        public void Dt_tick(object sender, EventArgs e)
        {
            // Your logic for the timer tick event 
            Window4 Fenetre = new Window4();

          
                incremet = incremet + 25; 
                pbConteo.Value = incremet;
                int drro;
                string path = @"C:\LOGJ\quant.txt";
                string contenido;
                string trutj;
                Window1 Fenetre1 = new Window1();
                Window3 Fenetre3 = new Window3();
                if (GlobalVariables.Exist) { this.Close(); }
                if (pbConteo.Value == 100)
                {
                    using (StreamReader reader = new StreamReader(path))
                    {

                        contenido = reader.ReadLine();
                        Fenetre3.Show();
                    }
                    this.Close();
                }  
                    // Logic of Directory colors check 
                    incremet++;
                if (Directory.Exists(Source.Text.ToString()))
                {
                    LabelSource.Background = new SolidColorBrush(Colors.Green);
                } 
                else
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

            // public void Window_Loaded(object sender, RoutedEventArgs e)
            //{
            //      DispatcherTimer timer = new DispatcherTimer();
            //    timer.Interval = TimeSpan.FromSeconds(1);
            //  timer.Tick += Dt_tick;
            // timer.Start();
            // if (pbConteo.Value == 100) 
            // {
            //    timer.Stop(); 
            // }  
            // } 
            // private int incremet = 0; 
            //private void Dt_tick(object sender, EventArgs e)
            //{
            //Window 2 integration here 
            //  incremet = incremet + 25;
            //pbConteo.Value = incremet;
            //int drro;
            //string path = @"C:\LOGJ\quant.txt";
            //string contenido;
            //string trutj;
            //Window1 Fenetre1 = new Window1();
            /*Window3 Fenetre3 = new Window3();
            if (GlobalVariables.Exist) { this.Close(); }
            if (pbConteo.Value == 100)
            {
                using (StreamReader reader = new StreamReader(path))
                {

                    contenido = reader.ReadLine();
                    Fenetre3.Show();
                }
                this.Close();
            } 

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
           */


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
        private void Button_Arab(object sender, RoutedEventArgs e)
        {

            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "المصدر";
            CibleLabel.Content = "هدف";
            TypeLogLabel.Content = "نوع اللوق "; 
            TypeSauvLabel.Content = "نوع الحفظ";
            NombLabel.Content = "عدد مرات الحفظ";
            ExtLabel.Content = "نوع البسط";
            ButtonCommence.Content = "بدا";
            ButtonQuit.Content = "خروج";
            //Fenetre.Show() 
        }
       public  int GetExtension(string ext)  
        {
            if (ext.Equals("Aucune")) { return 0; }
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
