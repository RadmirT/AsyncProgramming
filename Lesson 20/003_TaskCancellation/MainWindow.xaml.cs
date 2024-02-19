using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _003_TaskCancellation;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    CancellationTokenSource cts = new CancellationTokenSource();

    public MainWindow()
    {
        InitializeComponent();
    }


    private async void Button_Authorize(object sender, RoutedEventArgs e)
    {
        btnEnter.IsEnabled = false;
        string login = txtLogin.Text;
        string password = txtPassword.Text;
        try
        {
            bool isAuthorized = await Task.Run(() => Authorize(login, password, cts.Token), cts.Token);
            if (isAuthorized)
            {
                txtRes.Text += $"Добро прожаловать в систему, {login}!\n";
            }
        }
        catch (OperationCanceledException)
        {
            txtRes.Text += "Отмена авторизации\n";
        }
        catch (Exception ex)
        {
            txtRes.Text += $"Ошибка! {ex.Message}\n";
        }
        finally
        {
            btnEnter.IsEnabled = true;
        }
    }

    private bool Authorize(string login, string password, CancellationToken cancellationToken)
    {
        List<(string Login, string Password)> users = new List<(string, string)>() { ("user1", "qwerty"), ("user2", "asd"), ("user3", "1"), ("admin", "qhg57V}-.xdA*![v") };
        foreach (var user in users)
        {
            Thread.Sleep(1000);
            cancellationToken.ThrowIfCancellationRequested();
            if (user.Login != login)
            {
                continue;
            }
            if (user.Password == password)
            {
                return true;
            }
            throw new UnauthorizedAccessException("Неверный пароль");

        }
        throw new UnauthorizedAccessException("Неверный логин");

    }
    private void Button_Cancel(object sender, RoutedEventArgs e)
    {
        cts.Cancel();
        //txtRes.Text += $"Система готова к повторной отмене до пересоздания? - {!this.cts.IsCancellationRequested}\n";
        //cts = new CancellationTokenSource();
        //txtRes.Text += $"Система готова к повторной отмене до пересоздания? - {!this.cts.IsCancellationRequested}\n";
    }
}
