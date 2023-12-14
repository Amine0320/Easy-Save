using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ParaWindow : Window
    {
        public ParaWindow()
        {
            InitializeComponent();

        }


        private void AjtMetier(object sender, RoutedEventArgs e)
        {
            string path = GlobalVariables.Dir + "logicielmetier.txt";
            string metier = LogMetier.Text.ToString();
            if (metier.Split(".").Last() == "exe")
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write(metier + "\n");
                    writer.Close();
                }
            }
        }

        private void Button_OuvreLM(object sender, RoutedEventArgs e) 
        {
            string path = GlobalVariables.Dir + "logicielmetier.txt";
            Process.Start( "notepad.exe", path);

        }

        private void Retour (object sender, RoutedEventArgs e)
        {
            MainWindow Fenetre = new MainWindow();
            this.Close();
            Fenetre.Show();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            Application.Current.Shutdown();
            
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {

        }

        private void txt_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
