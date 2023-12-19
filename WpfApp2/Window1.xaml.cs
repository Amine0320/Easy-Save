using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Text.Json;

namespace WpfApp2
{
    /// <summary>
    ///  /// Interaction logic for Window1.xaml 
    /// </summary>
    public partial class Window1 : Window
    {
        // Constructor 
        public Window1()
        {
            InitializeComponent();
        }
        // DispatcherTimer for real-time updates 
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        // DispatcherTimer for real-time updates 
        DispatcherTimer timer = new DispatcherTimer();
        // Save number from global variables 
        int saveNumber = GlobalVariables.number;

        // Button Click Event Handler 
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create instances of other windows 
            Window4 Fenetre = new Window4();
            Window1 Fenetre1 = new Window1();
            // Show Window4 
            Fenetre.Show();
            // Wait for 100 milliseconds 
            System.Threading.Thread.Sleep(100);
            // Invoke Dispatcher to handle background tasks 
            Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Background);

            //string path = @"C:\LOGJ\quant.txt";
            //Programproc program = new Programproc();
            //program.EventMainAsync(Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), Debut.Text.ToString() + Option1.Text.ToString() + Fin.Text.ToString(), TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
            // Ouvrir la deuxième fenêtre 
            //Window2 Fenetre2 = new Window2();
            //Fenetre2.Show();   


            // Create and run a new task 
            Task task = Task.Run(() =>
            {
                // Create an instance of Programproc 
                Programproc program = new Programproc();
                // Invoke Dispatcher to execute the task on the UI thread 
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Call EventMain with parameters 
                    program.EventMain(cancellationTokenSource, Source.Text.ToString(), Cible.Text.ToString(), TypeSauv.Text.ToString(), saveNumber, TypeLog.Text.ToString(), GetExtension(Extension.Text.ToString()));
                });
            });
            // Add the task to the global list of tasks 
            GlobalVariables.tasks.Add(task); 
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
        // Button Click2 Event Handler 
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // Close the current window 
            this.Close();
            // Shutdown the application 
            Application.Current.Shutdown();
            
        }


        // Window Loaded Event Handler 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Configure and start the DispatcherTimer 
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Dt_tick;
            timer.Start();
            
        }
        private int incremet = 0;
        private double incremet2 = 0;
        // DispatcherTimer Tick Event Handler 
        private void Dt_tick(object sender, EventArgs e)
        {
            // Increment variables 
            incremet++;

            // Check if source directory exists and update label color 
            if (Directory.Exists(Source.Text.ToString()))
            {
                LabelSource.Background = new SolidColorBrush(Colors.Green); 
            }else
            {
                LabelSource.Background = new SolidColorBrush(Colors.Red);
            }
            // Check if target directory exists and update label color 
            if (Directory.Exists(Cible.Text.ToString()))
            {
                LabelCible.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                LabelCible.Background = new SolidColorBrush(Colors.Red);
            }
            // Path to State File 
            string pathfichier = @"C:\LOGJ\state.json";

            // Create an instance of EtatTempsReel class to store state information 
            EtatTempsReel statelog1 = new EtatTempsReel();

            // Check if the state file exists 
            if (File.Exists(pathfichier))
            {
                // Read the content of the state file 
                string contenidoJson = File.ReadAllText(pathfichier);

                // Deserialize JSON content into EtatTempsReel objects 
                foreach (string i in contenidoJson.Split('}',StringSplitOptions.None))
                {
                    if (!string.IsNullOrWhiteSpace(i))
                    {
                        // Deserialize each JSON fragment and update the state if the conditions are met 
                        statelog1 = JsonSerializer.Deserialize<EtatTempsReel>(i + "}");
                        if (statelog1.IdEtaTemp.Equals(saveNumber.ToString()) || statelog1.State.Equals("Active"))
                        {
                            incremet2 = statelog1.Progression;
                            pbConteo.Value = statelog1.Progression; 
                        } 
                    }
                }
            }
            else
            {
                // Set progress bar value to 0 if the state file doesn't exist 
                pbConteo.Value = 0;
            }

            // Variables used in the following block 
            int drro;
            //string path = @"C:\LOGJ\quant.txt";
            string contenido;
            string trutj;

            // Instances of Window1, Window3, and Window4 
            Window1 Fenetre1 = new Window1();
            Window3 Fenetre3 = new Window3();
            Window4 Fenetre4 = new Window4();

            // Check if the progress bar value is 100   
            if (pbConteo.Value == 100)
            {

                // Path to the second state file
                string endstate = @"C:\LOGJ\state2.json";

                // Check if the second state file exists 
                if (File.Exists(endstate))
                {
                    // Delete the original state file and replace it with the second state file 
                    File.Delete(pathfichier);
                    File.Move(endstate, pathfichier);
                }

                // Stop the timer
                timer.Stop();

                //Fenetre3.Show();
                //this.Close();

                // Increase progress bar value by 10 
                pbConteo.Value = pbConteo.Value + 10; 

            }
            else
            {
                // Check applications in the directory specified in the state file 
                LogMetier.CheckAppsInDirectory(statelog1.SourceFilePath);

                // Check if the process is active 
                if (GlobalVariables.Active)
                {

                    // Create a stop file to signal stopping the process 
                    using (StreamWriter escritor = new StreamWriter(@"C:\LOGJ\stop.exe"))
                    {
                        escritor.WriteLine("stop");
                    }
                }

            } 
        }
        // Button England Event Handler 
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
        // Button France Event Handler 
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
        // Button France Event Handler 
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

        // Button Arabic Event Handler 
        private void Button_Arabic(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            SourceLabel.Content = "المصدر";
            CibleLabel.Content = "الهدف";
            TypeLogLabel.Content = "نوع السجل";
            TypeSauvLabel.Content = "نوع الحفظ";
            ExtLabel.Content = "الامتدادات المراد تشفيرها";
            ButtonCommence.Content = "ابدأ";
            ButtonQuit.Content = "إنهاء"; 
            //Fenetre.Show();
        }  

        // GetExtention Code 
        public int GetExtension(string ext)
        {
            //Check if the option is Aucune (no extention given ) 
            if (ext.Equals("Aucune")) { return 0; }

            //Check if the option is .txt  (Extention Text given ) 
            else if (ext.Equals(".txt")) { return 1; }

            //Check if the option is .jpg (Extention  JPG given ) 
            else if (ext.Equals(".jpg")) { return 2; }

            //Check if the option is .png (Extention PNG given )  
            else if (ext.Equals(".png")) { return 3; }

            //Check if the option is .pdf (Extention PDF given ) 
            else if (ext.Equals(".pdf")) { return 4; }


            else { return 5; }
        }

        // Button Stop integrated in Window1 
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            // Define the file path for the stop command 
            string pathfichier = @"C:\LOGJ\stop.txt";
 
            // Define the content to be written (stop command)
            string content = "stop";

            try
            {
                // Write the stop command to the file 
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

            }
            catch (Exception ex) 
            { }
            // Display the Exitstop window 
            Exitstop exitstop = new Exitstop();
            exitstop.Show();

            // Close the current window 
            this.Close();
        }
        // Button Pause integrated in Window1 
        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            // Define the file path for the stop command 
            string pathfichier = @"C:\LOGJ\stop.txt";

            // Define the content to be written (stop command) 
            string content = "stop";

            try
            {
                // Write the stop command to the file 
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

            }
            catch (Exception ex)
            {}
        }
        // Button Play integrated in Window1 
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            // Define the file path for the stop command 
            string pathfichier = @"C:\LOGJ\stop.txt";

            // Define the content to be written (go command) 
            string content = "go";

            try
            {
                // Write the go command to the file 
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }
            }
            catch (Exception ex)
            {}
        }
    } 
}
