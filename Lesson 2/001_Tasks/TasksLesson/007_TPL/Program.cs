Task task = new Task(OperationAsync, "Hello world");
Task continuation = task.ContinueWith(Continuation);

Console.WriteLine($"Статус продолжения - {continuation.Status}");

task.Start();

Console.ReadKey();


void OperationAsync(object arg)
{
    Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}.");
    Console.WriteLine($"Argument value - {arg.ToString()}");
    Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}.");
}

void Continuation(Task task)
{
    Console.Write($"\nПродолжение #{Task.CurrentId} сработало в потоке {Thread.CurrentThread.ManagedThreadId}. ");
    Console.WriteLine($"Параметр задачи - {task.AsyncState}");
    Console.WriteLine($"Сразу после выполнения задачи.");
}
    