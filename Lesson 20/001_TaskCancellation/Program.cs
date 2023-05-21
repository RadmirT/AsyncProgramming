Console.WriteLine("Основной поток запушен");

CancellationTokenSource cts = new CancellationTokenSource();

Task task = Task.Run(() =>MyTask(cts.Token), cts.Token);

Thread.Sleep(2000);

try
{
    cts.Cancel();
    task.Wait();
}
catch(AggregateException ex)
{
    Console.WriteLine(new string('-', 80));
    Console.ForegroundColor = ConsoleColor.Red;

    if (task.IsCanceled)
    {
        Console.WriteLine("Задача отменена");
    }

    if (task.IsFaulted)
    {
        Console.WriteLine("Задача провалена");
    }

    Console.WriteLine(ex.InnerException.Message);
    Console.ResetColor();

    Console.WriteLine(new string('-', 80));

}
finally
{ 
    task.Dispose();
    cts.Dispose();
}

Console.WriteLine("Основной поток завершен");
Console.ReadKey();


void MyTask(CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();
    Console.WriteLine("MyTask запущен");
    for(int count = 0; count < 10; count++)
    {
        if(cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Получен запрос на отмему");
            cancellationToken.ThrowIfCancellationRequested();
            //throw new OperationCanceledException(cancellationToken);
        }
        // cancellationToken.ThrowIfCancellationRequested();
        Thread.Sleep(500);
        Console.WriteLine($"count = {count}");
    }
    Console.WriteLine("MyTask завершен");
}
