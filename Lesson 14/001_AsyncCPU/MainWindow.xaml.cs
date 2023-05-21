using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCPU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // Синхронный вариант вызова:
            int result = SumItems(2, 2, 2, 5, 4); // Такой вызов заблокирует выполнение на время работы метода.

            // Асинхронный вариант вызова:
            //Task<int> task = Task.Run(() => SumItems(2, 2, 2, 5, 4));

            // ЗДЕСЬ МОЖЕТ НАХОДИТСЯ КОД, КОТОРЫЙ В ЭТО ВРЕМЯ БУДЕТ ВЫПОЛНЯТЬСЯ ОСНОВНЫМ ПОТОКОМ

            // Получение результата асинхронной операции. Если результат не готов - его ожидание.
            //int result = await task;
            txtOutput.Text += $"Сумма последовательности = {result}{Environment.NewLine}";
        }

        private static int SumItems(params int[] items)
        {
            int sum = 0;

            for (int i = 0; i < items.Length; i++)
            {
                sum += items[i];
                Thread.Sleep(1000);
            }

            return sum;
        }
    }
}
