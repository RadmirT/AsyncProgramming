Action action = async () =>
{
    Console.WriteLine("-------------------------------------------------------");
    Console.WriteLine($"#1 Код до await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
    await Task.Run(() => Console.WriteLine($"#1 Задача выполнилась"));
    //throw new Exception("Ошибка в лямбда выражении #1!");
    Console.WriteLine($"#1 Код после await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine("-------------------------------------------------------");
};

Func<Task> func = async () =>
{
    Console.WriteLine("-------------------------------------------------------");
    Console.WriteLine($"#2 Код до await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
    //await Task.Run(() => Console.WriteLine($"#2 Задача выполнилась"));
    //throw new Exception("Ошибка в лямбда выражении #2!");
    Console.WriteLine($"#2 Код после await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine("-------------------------------------------------------");
};
try
{
    action.Invoke();
    await func.Invoke();
}
catch (Exception e)
{
    Console.WriteLine($"Исключение было обработано.");
}
Console.ReadKey();
