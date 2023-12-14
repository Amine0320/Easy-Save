using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;


namespace WpfApp2
{

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
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(metier);
                    writer.Close();
                }
            }
        }

        private void Button_OuvreLM(object sender, RoutedEventArgs e) 
        {
            string path = GlobalVariables.Dir + "logicielmetier.txt";
            Process.Start(path);

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
        private List<string> temporaryExtensions = new List<string>();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            // Supprimer le fichier existant s'il existe
            string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");

            // Obtenir la CheckBox actuelle qui a déclenché l'événement
            if (sender is CheckBox checkBox)
            {
                // Obtenir l'extension associée à cette CheckBox
                string extension = GetExtensionForCheckBox(checkBox);

                if (!string.IsNullOrEmpty(extension))
                {
                    // Ajouter l'extension temporairement à la liste
                    temporaryExtensions.Add(extension);
                }
                else
                {
                    Console.WriteLine("Extension non trouvée pour la CheckBox.");
                }
            }
        }

        // Obtenir l'extension associée à la CheckBox
       private string GetExtensionForCheckBox(CheckBox checkBox)
        {
            switch (checkBox.Name)
            {
                case "txt":
                    return ".txt";
                case "pdf":
                    return ".pdf";
                case "png":
                    return ".png";
                case "jpg":
                    return ".jpg";
                case "docx":
                    return ".docx";
                default:
                    return string.Empty;
            }
        }

        private void SavePrioritizedExtensions()
        {
            try
            {
                string filePath = Path.Combine(GlobalVariables.Dir, "ExtensionsPriori.txt");

                // Écrire la liste temporaire dans le fichier, écrasant son contenu existant
                File.WriteAllLines(filePath, temporaryExtensions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'enregistrement des extensions prioritaires : {ex.Message}");
            }
        }



        private void AjtExt_Click(object sender, RoutedEventArgs e)
        {
            // appeler SavePrioritizedExtensions ici pour sauvegarder les extensions temporaires dans le fichier
            SavePrioritizedExtensions();

            // Réinitialisez la liste temporaire pour la nouvelle exécution
            temporaryExtensions.Clear();
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

    }
}
