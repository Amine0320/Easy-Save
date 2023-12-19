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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ParaWindow : Window
    {
        // Constructor 
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
        // Set File Size Limit 
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
        // Open App List with Notepad
        private void Button_OuvreLM(object sender, RoutedEventArgs e) 
        {
            string path = GlobalVariables.Dir + "logicielmetier.txt";
            Process.Start( "notepad.exe", path);

        }
        // Add Preferred Extensions 
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
                // Save the preferred extensions 
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(Extensions);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exception and print error message to the console 
                Console.WriteLine($"Erreur lors de l'enregistrement des extensions prioritaires : {ex.Message}");
            }
        }
        // Return to the Main Window 
        private void Retour (object sender, RoutedEventArgs e)
        {
            // Create an instance of the main window 
            MainWindow Fenetre = new MainWindow();
            // Close the current window 
            this.Close();
            // Show the main window 
            Fenetre.Show();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // Close the current window 
            this.Close();
            // Shutdown the application 
            Application.Current.Shutdown();
            
        } 
        // Switch User Interface to English 
        private void Button_England(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "Extensions to prioritize";
            MetierLabel.Content = "Work Apps";
            LimitLabel.Content = "File Size Limit (in Ko)";
            ButtonQuit.Content = "Quit";
            ButtonRetour.Content = "Return";
            AjtExt.Content = "Add";
            BoutonAjtMetier.Content = AjtExt.Content;
        }
        // Switch User Interface to French  
        private void Button_France(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "Extensions à prioritiser";
            MetierLabel.Content = "Logiciels Métiers";
            LimitLabel.Content = "Taille Limite des Fichiers (en Ko)";
            ButtonQuit.Content = "Quitter";
            ButtonRetour.Content = "Retour";
            AjtExt.Content = "Ajoute";
            BoutonAjtMetier.Content = AjtExt.Content;
        }
        // Switch User Interface to Espagnol 
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "Extensiones para priorizar";
            MetierLabel.Content = "Software de negocios";
            LimitLabel.Content = "Límite de tamaño de archivo (en KB)";
            ButtonQuit.Content = "Dejar";
            ButtonRetour.Content = "atrás";
            AjtExt.Content = "Agregar";
            BoutonAjtMetier.Content = AjtExt.Content;
        }
        // Switch User Interface to Arabic 
        private void Button_Arabe(object sender, RoutedEventArgs e)
        {
            ExtensionsLabel.Content = "ملحقات لتحديد الأولويات";
            MetierLabel.Content = "اللوق الشغالين ";
            LimitLabel.Content = "الحجم الاقصى للملفات لالكيلو اوكتي"; 
            ButtonQuit.Content = "خروج";
            ButtonRetour.Content = "عودة";
            AjtExt.Content = "ضف";
            BoutonAjtMetier.Content = AjtExt.Content; 
        }
        // Event handler for checkbox Checked event 
        private void txt_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
