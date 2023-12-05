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
    public partial class Window2 : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Window2()
        {
            InitializeComponent();


            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Dt_tick;
            timer.Start();
            
            if (pbConteo.Value == 100)
            {
                timer.Stop();
            }
            else
            {
                
            }
            //
        }
        private int incremet =0;
        private void Dt_tick(object sender, EventArgs e)
        {
            incremet= incremet + 25;
            pbConteo.Value=incremet;
            int drro;
            string path = @"C:\LOGJ\quant.txt";
            string contenido;
            string trutj;
            Window1 Fenetre1 = new Window1();
            Window3 Fenetre3 = new Window3();
            if (pbConteo.Value == 100)
            {
                using (StreamReader reader = new StreamReader(path))
                {

                    contenido = reader.ReadLine();
                }
                if (contenido == "1")
                {
                    drro = int.Parse(contenido) - 1;

                    Fenetre3.Show();
                    File.Delete(path);
                    timer.Stop();

                }
                else if (contenido != "1")
                {
                    drro = int.Parse(contenido) - 1;

                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        // Escribir contenido en el archivo
                        trutj = drro.ToString();
                        writer.WriteLine(trutj);
                    }

                    Fenetre1.Show();
                    timer.Stop();
                }
                this.Close();
                
                
            }
            
            
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
