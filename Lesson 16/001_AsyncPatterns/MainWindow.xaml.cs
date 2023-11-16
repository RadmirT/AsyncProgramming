using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Net.Http;

namespace AsyncPatterns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string requestUrl = "https://learn.microsoft.com";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Apm_Click(object sender, RoutedEventArgs e)
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.BeginGetResponse((iAsyncResult) =>
            {
                var webResponse = webRequest.EndGetResponse(iAsyncResult);
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    Dispatcher.Invoke(() => txtAPMResult.Text = reader.ReadToEnd());
                }
            }, null);
        }

        private void Eap_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri(requestUrl));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            txtEAPResult.Text += e.Result;
        }

        private async void Tap_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(requestUrl);
            txtTAPResult.Text = result;
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtAPMResult.Clear();
            txtEAPResult.Clear();
            txtTAPResult.Clear();
        }
    }
}
