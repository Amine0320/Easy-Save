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
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Exitstop : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Exitstop()
        {
            InitializeComponent();
        }
        
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        private void Button_England(object sender, RoutedEventArgs e)
        {
           
        }
        private void Button_France(object sender, RoutedEventArgs e)
        {
         
        }
        private void Button_Espagnol(object sender, RoutedEventArgs e)
        {
         
        }
        private void Button_Arab(object sender, RoutedEventArgs e)
        {

        }

    }
}
