Task<int> task = Task.Run<int>(new Func<int>(GetValue));
Task<int> c1 = task.ContinueWith<int>(Increment);
Task<int> c2 = c1.ContinueWith<int>(Increment);
Task<int> c3 = c2.ContinueWith<int>(Increment);
Task<int> c4 = c3.ContinueWith<int>(Increment);
Task<int> c5 = c4.ContinueWith<int>(Increment);
c5.ContinueWith(ShowRes);

//task.ContinueWith(Increment)
//    .ContinueWith(Increment)
//    .ContinueWith(Increment)
//    .ContinueWith(Increment)
//    .ContinueWith(Increment)
//    .ContinueWith(ShowRes);

Console.WriteLine("Метод Main завершил свою работу..");
Console.ReadKey();

int GetValue() => 10;

static int Increment(Task<int> t)
{
    Console.WriteLine($"Продолжение task Id #{Task.CurrentId}. Thread Id #{Thread.CurrentThread.ManagedThreadId}.");
    int result = t.Result + 1;
    return result;
}

static void ShowRes(Task<int> t)
{
    Console.WriteLine($"Продолжение task Id #{Task.CurrentId}. Thread Id #{Thread.CurrentThread.ManagedThreadId}.");
    Console.WriteLine($"Результат - {t.Result}");
}