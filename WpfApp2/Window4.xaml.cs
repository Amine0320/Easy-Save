using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window4.xaml 
    /// </summary>
    public partial class Window4: Window 
    {
        //Constructor  
        public Window4() 
        {
            InitializeComponent();

        }

        // Button Oui Click Event 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
           // Increment the global variable "number"
           GlobalVariables.number++;

           // Open Window1
            Window1 Fenetre = new Window1();  
           this.Close();
           Fenetre.Show(); 

        }
        // Button Non Click Event
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {              
            // Execute the saves
            GlobalVariables.Play = true;

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
