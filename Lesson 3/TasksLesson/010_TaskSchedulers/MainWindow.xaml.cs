using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flag = false;

        public MainWindow()
        {
            InitializeComponent();
            lblThreadId.Content += $"{Thread.CurrentThread.ManagedThreadId}";
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (flag == true)
            {
                return;
            }

            HandlerSetup();

            new Task(() =>
            {
                while (flag == true)
                {
                    ShowThreadPoolInfo();
                    Thread.Sleep(300);
                }
            }, TaskCreationOptions.LongRunning).Start();

            Worker();
        }

        private void HandlerSetup()
        {
            txtContinuations.Text = string.Empty;
            txtThreadPool.Text = string.Empty;
            flag = true;
        }

        private void ShowThreadPoolInfo()
        {
            ThreadPool.GetAvailableThreads(out int threads, out int completionPorts);
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPorts);

            string result = $"W[{threads}:{maxThreads}] IO[{completionPorts}:{maxCompletionPorts}] {Environment.NewLine}";

            Dispatcher.Invoke((() => txtThreadPool.Text += result));
        }

        private void Worker()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            //new SynchronizationContextTaskScheduler();

            Task<int>[] tasks = new Task<int>[20];

            new Task(() =>
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    int m = i;
                    tasks[i] = new Task<int>(() =>
                    {
                        Thread.Sleep(1000);
                        return m;
                    });
                    tasks[i].Start();

                    tasks[i].ContinueWith((t) =>
                    {
                        txtContinuations.Text += $"Результат - {t.Result} в потоке[{Thread.CurrentThread.ManagedThreadId}]";
                        txtContinuations.Text += Environment.NewLine;
                    }, scheduler);
                }

                Task.WaitAll(tasks);
                Thread.Sleep(2000);
                flag = false;
            }, TaskCreationOptions.LongRunning).Start();
        }

    }
}
