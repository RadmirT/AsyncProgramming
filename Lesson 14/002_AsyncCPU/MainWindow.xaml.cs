using System;
using System.Windows;
using System.Diagnostics;
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
            txtOutput.Text += $"Суммирование всех чисел 3 500 000 000 должно равняться числу 6 124 999 998 250 000 000\n";
        }

        private void BtnStartSync_Click(object sender, RoutedEventArgs e)
        {
            btnStartSync.IsEnabled = false;

            // Синхронное выполнение:
            Stopwatch timer = Stopwatch.StartNew();
            ulong result = SumSequence(to: 3_500_000_000);
            txtOutput.Text += $"[Synchronous   ] Результат операции = {result:N0}. Результат готов за {timer.Elapsed.Seconds} секунд и {timer.Elapsed.Milliseconds} миллисекунд.\n";

            btnStartSync.IsEnabled = true;
        }

        private async void BtnStartAsync_Click(object sender, RoutedEventArgs e)
        {
            btnStartAsync.IsEnabled = false;

            // Асинхронное выполнение:
            Stopwatch timer = Stopwatch.StartNew();
            ulong result = await ParallelSumSequenceAsync(3_500_000_000);
            txtOutput.Text += $"[Async&Parallel] Результат операции = {result:N0}. Результат готов за {timer.Elapsed.Seconds} секунд и {timer.Elapsed.Milliseconds} миллисекунд.\n";

            btnStartAsync.IsEnabled = true;
        }


        private ulong SumSequence(ulong from = 0, ulong to = 0)
        {
            ulong res = 0;
            for (ulong i = from; i < to; i++)
            {
                res += i;
            }
            return res;
        }

        
        private async Task<ulong> ParallelSumSequenceAsync(ulong ceiling)
        {
            Task<ulong> computeTask1 = Task<ulong>.Run(() => SumSequence(0, ceiling / 2));
            Task<ulong> computeTask2 = Task<ulong>.Run(() => SumSequence(ceiling / 2, ceiling));

            ulong compute = await computeTask1 + await computeTask2;

            return compute;
        }

    }
}
