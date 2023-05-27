int a = 2, b = 3;
Task<int> task = Task.Run<int>(() => Calc(a, b));

task.ContinueWith(Continuation);

// Другой вариант продолжения:
//task.ContinueWith((t) =>
//{
//    Console.WriteLine($"\nПродолжение task Id #{Task.CurrentId}. Thread Id #{Thread.CurrentThread.ManagedThreadId}.")
//    Console.WriteLine($"Результат асинхронной операции - {t.Result}");
//});

Console.ReadKey();

int Calc(int a, int b)
{
    Console.WriteLine($"Task Id #{Task.CurrentId}. Thread Id #{Thread.CurrentThread.ManagedThreadId}.");
    return a + b;
}

void Continuation(Task<int> t)
{
    Console.WriteLine($"\nПродолжение task Id #{Task.CurrentId}. Thread Id #{Thread.CurrentThread.ManagedThreadId}.");
    Console.WriteLine($"Результат асинхронной операции - {t.Result}");
}
