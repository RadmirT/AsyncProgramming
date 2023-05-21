using System;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncMethods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            txtClick.FontSize = 45;
            txtDelay.FontSize = 14;
        }

        private void BtnInc_Click(object sender, RoutedEventArgs e)
        {
            txtClick.Text = (++counter).ToString();
        }

        private async void BtnDelay_Click(object sender, RoutedEventArgs e)
        {
            txtDelay.Text = $"Задача будет завершена по истечении времени задержки.";

            btnDelay.IsEnabled = false;
            li.Visibility = Visibility.Visible;

            await Task.Delay(5000);

            btnDelay.IsEnabled = true;
            li.Visibility = Visibility.Hidden;

            txtDelay.Text += Environment.NewLine;
            txtDelay.Text += $"Задача завершена!";
        }
    }
}
