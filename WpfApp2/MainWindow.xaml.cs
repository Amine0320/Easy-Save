using System.Collections.ObjectModel;
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
        // Constructor 
        public MainWindow()
        {
            InitializeComponent();

            //Remove old state
            string rutaArchivo = @"C:\LOGJ\state.json";
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    // Borrar el archivo
                    File.Delete(rutaArchivo);
                }
            }

            catch (Exception ex)
            { }
        } 

        // Button click event to open Window1 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create instances of Window1 
            Window1 Fenetre = new Window1(); 

            //Close Main Window
            this.Close();

            //Show ParaWindow 
            Fenetre.Show();
        }

        // Button click event to open ParaWindow 
        private void Button_Parameters(object sender, RoutedEventArgs e)
        {
            // Create instances of ParaWindow 
            ParaWindow Fenetre = new ParaWindow();

            //Close Main Window 
            this.Close();

            //Show ParaWindow 
            Fenetre.Show();
        }

        // Button click 1  event to close the application 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();

            //Close Window
            this.Close();

            //Close the Application 
            Application.Current.Shutdown();
            //Fenetre.Show();
        }
        // Button click event to set the user interface to English 
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "WELCOME TO OUR BACKUP SYSTEM! BY CESI INFO A3 COMPUTER SCIENCE";
            ButtonQuit.Content = "Quit";
            ButtonSave.Content = "Save";
            Parameters.Content = "Settings";
            //Fenetre.Show();
        }
        // Button click event to set the user interface to Frensh 
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "BIENVENUE A NOTRE SYSTEME DE SAUVEGARDE! BY CESI INFO A3 INFORMATIQUE";
            ButtonQuit.Content = "Quitter";
            Parameters.Content = "Paramètres";
            ButtonSave.Content = "Enregister";
            //Fenetre.Show();
        }
        // Button click event to set the user interface to Espagnole
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "¡BIENVENIDO A NUESTRO SISTEMA DE RESPALDO! POR CESI INFO A3 INFORMATICA";
            ButtonQuit.Content = "Salir";
            ButtonSave.Content = "Guardar";
            Parameters.Content = "Parámetros";
            //Fenetre.Show();
        }
        // Button click event to set the user interface to Arabic 
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
