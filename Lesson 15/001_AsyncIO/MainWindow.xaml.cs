using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Threading;

namespace AsyncIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            File.Create("test.txt").Dispose();
            MonitorIOCP();
        }

        private async void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("test.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                liRead.Visibility = Visibility.Visible;

                byte[] bytes = new byte[fs.Length];
                await fs.ReadAsync(bytes, 0, bytes.Length);
                string fileContent = Encoding.UTF8.GetString(bytes);

                liRead.Visibility = Visibility.Hidden;
                txtOutput.Text = fileContent;
            }
        }

        private async void BtnWrite_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("test.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                liWrite.Visibility = Visibility.Visible;

                string textBoxContext = txtOutput.Text;
                byte[] bytes = Encoding.UTF8.GetBytes(textBoxContext);
                await fs.WriteAsync(bytes, 0, bytes.Length);

                liWrite.Visibility = Visibility.Hidden;
                txtOutput.Clear();
            }
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

