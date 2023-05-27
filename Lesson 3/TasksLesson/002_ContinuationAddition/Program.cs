Task task = Task.Run(() => Method());
// Указание настроек выполнения продолжения :
//task.ContinueWith((t) => Continuation(t), TaskContinuationOptions.ExecuteSynchronously);
task.ContinueWith((t) => Continuation(t), TaskContinuationOptions.RunContinuationsAsynchronously);
void Method()
{
    Thread.Sleep(2000);
    Console.WriteLine($"Задача #{Task.CurrentId} выполнила метод в потоке {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine(new string('-', 80));
}

void Continuation(Task task)
{
    Console.WriteLine($"Id задачи продолжения - {Task.CurrentId}.");
    Console.WriteLine($"Продолжение выполнилось в потоке {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine();
}

