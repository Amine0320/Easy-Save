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
        
        private int incremet =0;
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

    }
}
