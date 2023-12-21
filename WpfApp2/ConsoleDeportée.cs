using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
namespace WpfApp2
{
    public class Server
    {
        private TcpListener tcpListener;
        //Window1 mainWindow = new Window1();
        public void Start()
        {
            
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
            tcpListener.Start();


            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(HandleClientComm);
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[1024];
            int bytesRead = clientStream.Read(message, 0, 1024);
            string request = Encoding.UTF8.GetString(message, 0, bytesRead);
            /*
            if (request == "OpenWPFPage")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Dispatcher.Invoke(() =>
                {

                    
                    mainWindow.Show();
                    //mainWindow.Close();
                });
            }*/

            tcpClient.Close();
        }
    }
}