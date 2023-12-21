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
        private static Mutex mutex = new Mutex(true, "{1E8C4D6A-9C5C-4A0E-9D85-0E26F55D7E2F}");

        protected override void OnStartup(StartupEventArgs e)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show("L'application est déjà en cours d'exécution.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
