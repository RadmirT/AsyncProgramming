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
            loadingIndicator.Visibility = Visibility.Visible;
            try
            {
                var result = await Task.Run(() => DownloadString("http://microsoft.com"));
                txtDownload.Text = result;
            }
            catch (Exception exeption)
            {
                txtExceptions.Text = exeption.Message;
            }
            finally
            {
                loadingIndicator.Visibility = Visibility.Hidden;
            }

            DispatcherSynchonizationContex
        }

        private string DownloadString(string url)
        {
            Thread.Sleep(5000);

            HttpClient httpClient = new HttpClient();

            return httpClient.GetStringAsync(url).Result;
        }

    }
}
