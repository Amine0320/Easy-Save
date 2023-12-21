﻿using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Text.Json;

namespace Soutenance
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        DispatcherTimer timer = new DispatcherTimer();
        //int saveNumber = GlobalVariables.number;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Window4 Fenetre = new Window4();
            Window1 Fenetre1 = new Window1();
            //Fenetre.Show();
            System.Threading.Thread.Sleep(100);
            Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Background);
           
            //string path = @"C:\LOGJ\quant.txt";
                //Programproc program = new Programproc();
                //program.EventMainAsync(Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), Debut.Text.ToString() + Option1.Text.ToString() + Fin.Text.ToString(), TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
            // Ouvrir la deuxième fenêtre 
            //Window2 Fenetre2 = new Window2();
            //Fenetre2.Show();   
            /*
            Task task = Task.Run(() =>
            {
                Programproc program = new Programproc();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    program.EventMain(cancellationTokenSource, Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), saveNumber, TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
                });
            });*/
            //GlobalVariables.tasks.Add(task);





            /*
             * Version Bruno
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
            /*
             * Autre partie ver Bruno
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
            */
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
            //EtatTempsReel statelog1 = new EtatTempsReel();
            if (File.Exists(pathfichier))
            {
                string contenidoJson = File.ReadAllText(pathfichier);
                foreach (string i in contenidoJson.Split('}',StringSplitOptions.None))
                {
                    if (!string.IsNullOrWhiteSpace(i))
                    {
                        /*statelog1 = JsonSerializer.Deserialize<EtatTempsReel>(i + "}");
                        if (statelog1.IdEtaTemp.Equals(saveNumber.ToString()) || statelog1.State.Equals("Active"))
                        {
                            incremet2 = statelog1.Progression;
                            pbConteo.Value = statelog1.Progression;
                        }*/
                    }
                }
            }
            else
            {
                pbConteo.Value = 0;
            }
            int drro;
            //string path = @"C:\LOGJ\quant.txt";
            string contenido;
            string trutj;
            //Window1 Fenetre1 = new Window1();
            //Window3 Fenetre3 = new Window3();
            //Window4 Fenetre4 = new Window4(); 
            if (pbConteo.Value == 100)
            {
                string endstate = @"C:\LOGJ\state2.json";
                if (File.Exists(endstate))
                {
                    File.Delete(pathfichier);
                    File.Move(endstate, pathfichier);
                }
                timer.Stop();
    
                //Fenetre3.Show();
                //this.Close();
                pbConteo.Value = pbConteo.Value + 10;
                


            }
            else
            {
                /*LogMetier.CheckAppsInDirectory(statelog1.SourceFilePath);
                if (GlobalVariables.Active)
                {
                    using (StreamWriter escritor = new StreamWriter(@"C:\LOGJ\stop.exe"))
                    {
                        escritor.WriteLine("stop");
                    }
                }*/

            } 
        }
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "Source";
            CibleLabel.Content = "Destination";
            TypeLogLabel.Content = "LogType";
            TypeSauvLabel.Content = "LogSav";
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
            ExtLabel.Content = "Extensiones para cifrar";
            ButtonCommence.Content = "Comenzar";
            ButtonQuit.Content = "Salir";
            //Fenetre.Show();
        }
        public int GetExtension(string ext)
        {
            if (ext.Equals("Aucune")) { return 0; }
            else if (ext.Equals(".txt")) { return 1; }
            else if (ext.Equals(".jpg")) { return 2; }
            else if (ext.Equals(".png")) { return 3; }
            else if (ext.Equals(".pdf")) { return 4; }
            else { return 5; }
        }


        private void Button_Stop(object sender, RoutedEventArgs e)
        {
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
            { }
            //Exitstop exitstop = new Exitstop();
            //exitstop.Show();
            this.Close();
        }
        
        private void Button_Pause(object sender, RoutedEventArgs e)
        {
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

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            string pathfichier = @"C:\LOGJ\stop.txt";

            string content = "go";

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}