Console.WriteLine("Программа начала свою работу.");
var cts = new CancellationTokenSource();
var task = Task.Run(() =>
{
    Thread.Sleep(1000);
    Console.WriteLine("Задача выполнилась");
    return 10;
});

var c1 = task.ContinueWith(t =>
{
    Console.WriteLine("Продолжение #1 выполнилось");
    Console.WriteLine($"Результат {t.Result}");
    return t.Result * 2;
}, cts.Token);

var c2 = c1.ContinueWith(t =>
{
    Console.WriteLine("Продолжение #2 выполнилось");
    try
    {
        Console.WriteLine($"Результат {t.Result}");
    }
    catch (AggregateException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\tОшибка: {ex.InnerException.GetType()}");
        Console.WriteLine($"\tСообщение об ошибке: {ex.InnerException.Message}");
        Console.ResetColor();
    }
});

cts.Cancel();
Thread.Sleep(3000);

Console.WriteLine(new string('-', 80));
Console.WriteLine($"Статус задачи : {task.Status}");
Console.WriteLine($"Статус продолжения #1: {c1.Status}");
Console.WriteLine($"Статус продолжения #2: {c2.Status}");
Console.WriteLine(new string('-', 80));

Console.WriteLine("Программа завершена");
Console.ReadKey();
