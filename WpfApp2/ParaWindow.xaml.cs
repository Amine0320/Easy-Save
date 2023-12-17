using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Shapes;
using Path = System.IO.Path;


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
        private void AjtLimite(object sender, RoutedEventArgs e)
        {
            string path = GlobalVariables.Dir + "limite.txt";
            string limite = Limite.Text.ToString();
            if (limite.All(char.IsDigit))
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(limite);
                writer.Close();
            }
        }

        private void Button_OuvreLM(object sender, RoutedEventArgs e) 
        {
            string path = GlobalVariables.Dir + "logicielmetier.txt";
            Process.Start( "notepad.exe", path);

        }

        private void AjtExt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");
                string Extensions = "";
                if ((bool)txt.IsChecked)
                {
                    Extensions += ".txt\n";
                }
                if ((bool)pdf.IsChecked)
                {
                    Extensions += ".pdf\n";
                }
                if ((bool)png.IsChecked)
                {
                    Extensions += ".png\n";
                }
                if ((bool)jpg.IsChecked)
                {
                    Extensions += ".jpg\n";
                }
                if ((bool)docx.IsChecked)
                {
                    Extensions += ".docx\n";
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(Extensions);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'enregistrement des extensions prioritaires : {ex.Message}");
            }
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
        


        private void Button_England(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "Extensions to prioritize";
            MetierLabel.Content = "Work Apps";
            ButtonQuit.Content = "Quit";
            ButtonRetour.Content = "Return";
            AjtExt.Content = "Add";
            BoutonAjtMetier.Content = AjtExt.Content;
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "Extensions à prioritiser";
            MetierLabel.Content = "Logiciels Métiers";
            ButtonQuit.Content = "Quitter";
            ButtonRetour.Content = "Retour";
            AjtExt.Content = "Ajoute";
            BoutonAjtMetier.Content = AjtExt.Content;
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Arabe(object sender, RoutedEventArgs e)
        { }

        private void txt_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
