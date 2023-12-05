using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            InitializeComponent();

            Items = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Text = "Opción 1", ImagePath = "England.png" },
                new ComboBoxItem { Text = "Opción 2", ImagePath = "Espagne.png" },
                new ComboBoxItem { Text = "Opción 3", ImagePath = "Egypt.png" }
            };
        }
        public ObservableCollection<ComboBoxItem> Items { get; set; }
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string ImagePath { get; set; }
            public BitmapImage ImageSource => new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
        }
        private void MiBoton_Click(object sender, RoutedEventArgs e)
        {
            // Código a ejecutar cuando se hace clic en el botón
            MessageBox.Show("¡Haz hecho clic en el botón!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Fenetre = new Window1();
            this.Close();
            Fenetre.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            this.Close();
            //Fenetre.Show();
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
        private void Button_England(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "WELCOME TO OUR BACKUP SYSTEM! BY CESI INFO A3 COMPUTER SCIENCE";
            ButtonQuit.Content = "QUIT";
            ButtonSave.Content = "SAVE";
            //Fenetre.Show();
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "BIENVENUE A NOTRE SISTEME DE SAUVEGARDE! BY CESI INFO A3 INFORMATIQUE";
            ButtonQuit.Content = "SORTIR";
            ButtonSave.Content = "ENREGISTRER";
            //Fenetre.Show();
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "¡BIENVENIDO A NUESTRO SISTEMA DE RESPALDO! POR CESI INFO A3 INFORMATICA";
            ButtonQuit.Content = "SALIR";
            ButtonSave.Content = "GUARDAR";
            //Fenetre.Show();
        }
        private void Button_Arab(object sender, RoutedEventArgs e)
        {
            //Window1 Fenetre = new Window1();
            LabelSysteme.Content = "السنة الثالثة علوم الحاسوب في cesi";
            ButtonQuit.Content = "خروج";
            ButtonSave.Content = "حفظ";
            //Fenetre.Show();
        }
    }
}
