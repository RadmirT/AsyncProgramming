using System.Windows;

namespace _001_Progress;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IProgress<int> progress;
    private CancellationTokenSource cts = new CancellationTokenSource();

    public MainWindow()
    {
        this.InitializeComponent();
        this.progress = new Progress<int>(this.ProgressBarUpdate);
    }
    private void ProgressBarUpdate(int value)
    {
        this.pb.Value = value;
    }

    private async void btnStart_Click(object sender, RoutedEventArgs e)
    {
        this.pb.Value = 0;
        this.btnStart.IsEnabled = false;
        var operation = new Operation();
        try
        {
            this.txtRes.Text += $"==========================================================================\n";
            this.txtRes.Text += $"Операция начата\n";
            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            var sum = await operation.SumNumbersAsync(numbers, this.cts.Token, this.progress);
            this.txtRes.Text += $"Операция завершена. Сумма всех чисел {string.Join(',', numbers)} равна {sum}";
            this.txtRes.Text += $"\n==========================================================================\n";
        }
        catch (OperationCanceledException)
        {
            this.txtRes.Text += $"Операция отменена!!!";
            this.txtRes.Text += $"\n==========================================================================\n";
            this.cts = new CancellationTokenSource();

        }
        catch (Exception ex)
        {
            this.txtRes.Text += $"Возникла ошибка: {ex.GetType()}\n";
            this.txtRes.Text += $"\t-- сообщение об ошибке: {ex.Message}\n";
            this.txtRes.Text += $"\n==========================================================================\n";
        }
        finally
        {
            this.btnStart.IsEnabled = true;
        }
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.cts.Cancel();
    }
}