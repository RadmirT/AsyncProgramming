Console.WriteLine("Программа начала свою работу.");
var cts = new CancellationTokenSource();
var task = Task.Run(() =>
{
    Thread.Sleep(1000);
    Console.WriteLine("Задача выполнилась");
    return 10;
}, cts.Token);

var c1 = task.ContinueWith(t =>
{
    Console.WriteLine("Продолжение выполнилось");
    if (t.Status != TaskStatus.Canceled  && t.Status != TaskStatus.Faulted)
    {
        Console.WriteLine($"Результат {t.Result}");
    }
});

cts.Cancel();
Thread.Sleep(3000);

Console.WriteLine(new string('-', 80));
Console.WriteLine($"Статус задачи : {task.Status}");
Console.WriteLine($"Статус продолжения: {c1.Status}");
Console.WriteLine(new string('-', 80));

Console.WriteLine("Программа завершена");
Console.ReadKey();
