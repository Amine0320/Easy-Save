using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for Exitstop.xaml
    /// </summary>
    public partial class Exitstop : Window
    {
        // Timer for handling UI updates
        DispatcherTimer timer = new DispatcherTimer();

        // Constructor for the Exitstop class
        public Exitstop()
        {
            InitializeComponent();
        }

        // Event handler for the "Exit" button click
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();

            // Shutdown the application
            Application.Current.Shutdown();
        }

        // Event handler for the "English" button click
        private void Button_England(object sender, RoutedEventArgs e)
        {
            // Update content in English
            pbConteo2.Content = "You have stopped the recording. Click Quit to exit the application";
            ButtonQuit.Content = "Exit";
        }

        // Event handler for the "France" button click
        private void Button_France(object sender, RoutedEventArgs e)
        {
            // Update content in French
            pbConteo2.Content = "Vous avez arrêté l'enregistrement. Cliquez sur Quitter pour quitter l'application";
            ButtonQuit.Content = "Quitter";
        }

        // Event handler for the "Espagnol" button click
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            // Update content in Spanish
            pbConteo2.Content = "Has detenido la grabación. Haz clic en Salir para cerrar la aplicación";
            ButtonQuit.Content = "Salir";
        }

        // Event handler for the "Arab" button click
        private void Button_Arab(object sender, RoutedEventArgs e)
        {
            // Update content in Arabic
            pbConteo2.Content = "لقد قمت بإيقاف التسجيل. انقر على خروج لإغلاق التطبيق";
            ButtonQuit.Content = "خروج";
        }
    }
}
