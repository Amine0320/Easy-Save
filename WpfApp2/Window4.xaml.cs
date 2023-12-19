using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class Window4: Window 
    {
        // Properties to track button clicks 
        public bool OuiClicked { get; private set; }
        public bool NonClicked { get; private set; }  
        
        //Constructor  
        public Window4() 
        {
            InitializeComponent();

        }
        //ObservableCollection for ComboBox items 
        public ObservableCollection<ComboBoxItem> Items { get; set; }
        public class ComboBoxItem
        {
            // Gets or sets the text associated with the ComboBoxItem 
            public string Text { get; set; }

            // Gets or sets the file path for the image associated with the ComboBoxItem 
            public string ImagePath { get; set; }

            // Create a BitmapImage from the specified ImagePath
            public BitmapImage ImageSource => new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
        }
        // Button Oui Click Event 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // Set OuiClicked to true 
           OuiClicked = true;

           // Increment the global variable "number"
           GlobalVariables.number++;

           // Open Window1
            Window1 Fenetre = new Window1();  
           this.Close();
           Fenetre.Show();
           // If I clicked yes 
           // Mise en veille pour éviter de bloquer le thread
            

            // Ouvrir la deuxième fenêtre 
            //Window2 Fenetre2 = new Window2();
            //Fenetre2.Show() ;   

        }
        // Button Non Click Event
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {              

            Window1 Fenetre = new Window1();

            // Set NonClicked to true 
            NonClicked = true;
            /*
            System.Threading.Thread.Sleep(100);
            Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Background);
            string path = @"C:\LOGJ\quant.txt";
            Programproc program = new Programproc();
            //program.EventMainAsync(Fenetre.Source.Text.ToString(), Fenetre.Cible.Text.ToString(), Fenetre.TypeSauv.Text.ToString(), Fenetre.Debut.Text.ToString() + Fenetre.Option1.Text.ToString() + Fenetre.Fin.Text.ToString(), Fenetre.TypeLog.Text.ToString(), Fenetre.GetExtension(Fenetre.Extension.Text.ToString()));
            this.Dispatcher.Invoke(() =>
            {
                program.EventMainAsync(
                    Fenetre.Source.Text.ToString(),
                    Fenetre.Cible.Text.ToString(),
                    Fenetre.TypeSauv.Text.ToString(),
                    Fenetre.Debut.Text.ToString() + Fenetre.Option1.Text.ToString() + Fenetre.Fin.Text.ToString(),
                    Fenetre.TypeLog.Text.ToString(),
                    Fenetre.GetExtension(Fenetre.Extension.Text.ToString())
                );
            });
            */
            string pathfichier = @"C:\LOGJ\stop.txt";

            string content = "go";

            try
            {
                // Writes the specified content to a file at the specified path 
                using (StreamWriter writer = new StreamWriter(pathfichier))
                {
                    writer.WriteLine(content);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, if any 
            }
            // Close the current window 
            this.Close();

            // Wait for all tasks to complete 
            Task.WaitAll(GlobalVariables.tasks.ToArray());

            // Pause for a brief period 
            Thread.Sleep(1500);

            // Open Window3 
            Window3 Fenetre3 = new Window3(); 
            Fenetre3.Show(); 
        }
        // ComboBox SelectionChanged Event 
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // ListBox SelectionChanged Event 
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
 
        // ComboBox SelectionChanged Event 
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        // language buttons 

        // Switch User Interface to English 
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "Would you like to perform another backup?";
            ButtonQuit_Copy.Content = "No";
            ButtonSave.Content = "Yes";
            //Fenetre.Show();
        }
        // Switch User Interface to French 
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "Souhaitez-vous effectuer une autre sauvegarde ? ";
            ButtonQuit_Copy.Content = "Non";
            ButtonSave.Content = "Oui";
            //Fenetre.Show();
        }
        // Switch User Interface to Espagnol
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "¿Desea realizar otra copia de seguridad?";
            ButtonQuit_Copy.Content = " No";
            ButtonSave.Content = "Sí";
            //Fenetre.Show();
        }
        // Switch User Interface to Arabic  
        private void Button_Arab(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "هل ترغب في إجراء نسخة احتياطية أخرى؟";
            ButtonQuit_Copy.Content = "لا";
            ButtonSave.Content = "نعم";
            //Fenetre.Show();
        }
    }
}
