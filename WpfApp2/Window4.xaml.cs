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
        public bool OuiClicked { get; private set; }
        public bool NonClicked { get; private set; } 
        
        public Window4() 
        {
            InitializeComponent();

        }
        public ObservableCollection<ComboBoxItem> Items { get; set; }
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string ImagePath { get; set; }
            public BitmapImage ImageSource => new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
        } 
        // Button Oui 
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
           OuiClicked = true;
            GlobalVariables.number++;
           Window1 Fenetre = new Window1();  
           this.Close();
           Fenetre.Show();
           // If I clicked yes 
           // Mise en veille pour éviter de bloquer le thread
            

            // Ouvrir la deuxième fenêtre 
            //Window2 Fenetre2 = new Window2();
            //Fenetre2.Show() ;   

        } 
        // Button Non  
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {              
            Window1 Fenetre = new Window1();
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
            GlobalVariables.play = true;
            this.Close();
            Task.WaitAll(GlobalVariables.tasks.ToArray());
            Thread.Sleep(1500);
            Window3 Fenetre3 = new Window3(); 
            Fenetre3.Show(); 
        }  
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        } 

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        // language buttons 
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "Would you like to perform another backup?";
            ButtonQuit_Copy.Content = "No";
            ButtonSave.Content = "Yes";
            //Fenetre.Show();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "Souhaitez-vous effectuer une autre sauvegarde ? ";
            ButtonQuit_Copy.Content = "Non";
            ButtonSave.Content = "Oui";
            //Fenetre.Show();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "¿Desea realizar otra copia de seguridad?";
            ButtonQuit_Copy.Content = " No";
            ButtonSave.Content = "Sí";
            //Fenetre.Show();
        }
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
