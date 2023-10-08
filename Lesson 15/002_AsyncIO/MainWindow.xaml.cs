using System;
using System.Windows;
using System.Net.Http;
using System.Threading;
using System.Net;

namespace AsyncIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            MonitorIOCP();
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {   
            li.Visibility = Visibility.Visible;
            string httpContent = await httpClient.GetStringAsync("http://microsoft.com/");

            li.Visibility = Visibility.Hidden;
            txtOutput.Text = httpContent;
        }


        private void MonitorIOCP()
        {
            int previousThreads = int.MinValue;
            int previousIOCP = int.MinValue;
            new Thread(() =>
            {
                while (true)
                {
                    ThreadPool.GetAvailableThreads(out int threads, out int IOCP);
                    ThreadPool.GetMaxThreads(out int maxThreads, out int maxIOCP);

                    if ((previousThreads != threads || previousIOCP != IOCP))
                    {
                        previousThreads = threads;
                        previousIOCP = IOCP;
                        if (threads != maxThreads || IOCP != maxIOCP)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                txtIOCP.Text += $"WT[{threads} из {maxThreads}] IOCP[{IOCP} из {maxIOCP}]\n";
                            });
                        };
                    }
                }
            })
            { IsBackground = true }.Start();
        }
    }
}
