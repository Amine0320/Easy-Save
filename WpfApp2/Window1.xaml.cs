using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Text.Json;
using System.Threading.Tasks;
using static WpfApp2.Window1;
using System.Numerics;
using System.Threading;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.Metadata;

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
            /*
            if (File.Exists(path))
            {
                Debut.IsReadOnly = true;
                Fin.IsReadOnly = true;
                Option1.Text = "";
                Option1.IsEnabled = false;
            }
            */
        }
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        DispatcherTimer timer = new DispatcherTimer();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = @"C:\LOGJ\quant.txt";
            int quan;
            
            /*if (Option1.Text.ToString() == "-")
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
            
                

                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    this.Close();
                    Fenetre2.Show();
                });
             */
            //cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() =>
            {
                Programproc program = new Programproc();
                Application.Current.Dispatcher.Invoke(() =>
                { 
                 program.EventMain(cancellationTokenSource, Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), Debut.Text.ToString() + Option1.Text.ToString() + Fin.Text.ToString(), TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
                });
            });
            bool ejer = cancellationTokenSource.IsCancellationRequested;

            Window2 Fenetre2 = new Window2();
            cancellationTokenSource.Cancel();
            ejer = cancellationTokenSource.IsCancellationRequested;
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
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Dt_tick;
            timer.Start();
            
        }
        private int incremet = 0;
        private double incremet2 = 0;
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
            string pathfichier = @"C:\LOGJ\state.json";
            if (File.Exists(pathfichier))
            {
                string contenidoJson = File.ReadAllText(pathfichier);

                statelog statelog1 = JsonSerializer.Deserialize<statelog>(contenidoJson);
                incremet2 = statelog1.Progression;
                pbConteo.Value = statelog1.Progression;
            }
            else
            {
                pbConteo.Value = 0;
            }
            int drro;
            string path = @"C:\LOGJ\quant.txt";
            string contenido;
            string trutj;
            Window1 Fenetre1 = new Window1();
            Window3 Fenetre3 = new Window3();
            if (GlobalVariables.Exist) { this.Close(); }
            if (pbConteo.Value == 100)
            {
                timer.Stop();
                
                Fenetre3.Show();
                this.Close();
                pbConteo.Value = pbConteo.Value + 10;

            }else
            {
                try
                {
                    if (cancellationTokenSource != null)
                {
                    
                        //cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    

                }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string rutaArchivo = @"C:\LOGJ\stop.txt";

            // Contenido que se escribirá en el archivo
            string contenido = "stop";

            try
            {
                // Crear un objeto StreamWriter y escribir en el archivo
                using (StreamWriter escritor = new StreamWriter(rutaArchivo))
                {
                    escritor.WriteLine(contenido);
                }

                //Console.WriteLine("Archivo creado exitosamente.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al crear el archivo: {ex.Message}");
            }
            Exitstop exitstop = new Exitstop();
            exitstop.Show();
            this.Close();
        }
        public class statelog
        {
            public string IdEtaTemp { get; set; }
            public int NbFilesLeftToDo { get; set; }
            public double Progression { get; set; }
            public string State { get; set; }
            public string NomETR { get; set; }
            public long TotalFilesSize { get; set; }
            public string SourceFilePath { get; set; }
            public long TotalFilesToCopy { get; set; }

            public string TargetFilePath { get; set; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string pathfichier = @"C:\LOGJ\stop.txt";

            // Contenido que se escribirá en el archivo
            string content = "stop";

            try
            {
                // Crear un objeto StreamWriter y escribir en el archivo
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

                //Console.WriteLine("Archivo creado exitosamente.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al crear el archivo: {ex.Message}");
            }
            //Exitstop exitstop = new Exitstop();
            //exitstop.Show();
            //this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string pathfichier = @"C:\LOGJ\stop.txt";

            // Contenido que se escribirá en el archivo
            string content = "go";

            try
            {
                // Crear un objeto StreamWriter y escribir en el archivo
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

                //Console.WriteLine("Archivo creado exitosamente.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al crear el archivo: {ex.Message}");
            }
        }
    }
}
