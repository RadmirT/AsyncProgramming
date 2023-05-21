using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AsyncProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter;
        public MainWindow()
        {
            InitializeComponent();
            counter = 0;
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            txtClick.Text = Convert.ToString(++counter);
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loadingIndicator.Visibility = Visibility.Visible;
                var result = await DownloadStringAsync("http://microsoft.com/");
                txtDownload.Text = result;
            }
            catch (Exception exception)
            {
                txtExceptions.Text = exception.Message;
            }
            finally
            {
                loadingIndicator.Visibility = Visibility.Hidden;
            }
            
            // var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            // loadingIndicator.Visibility = Visibility.Visible;
            // var downloadTask = DownloadStringAsync("http://microsoft.com/");
            // downloadTask.ContinueWith(_ => txtDownload.Text = downloadTask.Result, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, scheduler);
            // downloadTask.ContinueWith(task => txtExceptions.Text = task.Exception.Message, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, scheduler);
            // downloadTask.ContinueWith(_ => loadingIndicator.Visibility = Visibility.Hidden, scheduler);

        }

        private Task<string> DownloadStringAsync(string url)
        {
            return Task.Run(() => DownloadString(url));
        }

        private string DownloadString(string url)
        {
            Thread.Sleep(5000);

            HttpClient httpClient = new HttpClient();

            return httpClient.GetStringAsync(url).Result;
        }

    }
}
