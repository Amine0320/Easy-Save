using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Mutex to ensure single instance of the application
        private static Mutex mutex = new Mutex(true, "{1E8C4D6A-9C5C-4A0E-9D85-0E26F55D7E2F}");

        // Override the OnStartup event handler
        protected override void OnStartup(StartupEventArgs e)
        {
            // Check if the mutex can be acquired (no other instance running)
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // If mutex acquired, proceed with the base OnStartup method
                base.OnStartup(e);
            }
            else
            {
                // If mutex cannot be acquired, show an error message
                MessageBox.Show("L'application est déjà en cours d'exécution.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                // Shutdown the application
                Shutdown();
            }
        }
    }
}
