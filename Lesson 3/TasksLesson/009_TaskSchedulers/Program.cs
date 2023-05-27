Console.SetWindowSize(100, 45);
Console.WriteLine($"Поток метода Main - {Thread.CurrentThread.ManagedThreadId}.");
Timer timer = new Timer(ShowThreadPoolInfo, null, 1000, 1000);

PriorityTaskScheduler scheduler = new PriorityTaskScheduler();

Task[] tasks = new Task[30];

for (int i = 0; i < 30; i++)
{
    tasks[i] = new Task(() =>
    {
        Thread.Sleep(3000);
        Console.WriteLine($"Выполнена задача {Task.CurrentId} в потоке {Thread.CurrentThread.ManagedThreadId}");
    });
    tasks[i].Start(scheduler);
}

Task lowPriorityTask = new Task(() =>
{
    Thread.Sleep(1000);
    Console.WriteLine($"НИЗКОПРИОРИТЕТНАЯ задача выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
});
lowPriorityTask.Start(scheduler);
scheduler.Deprioritize(lowPriorityTask);

Console.WriteLine("Высокоприоритетные задачи начались позже, но выполнятся первее.");

for (int i = 0; i < 15; i++)
{
    Task task = new Task(() =>
    {
        Thread.Sleep(3000);
        Console.WriteLine($"ПРИОРИТЕТНАЯ задача {Task.CurrentId} в потоке - {Thread.CurrentThread.ManagedThreadId}");
    });

    task.Start(scheduler);

    scheduler.Prioritize(task);
}

Task.WaitAll(tasks);
Thread.Sleep(2000);
timer.Dispose();

Console.ReadKey();

void ShowThreadPoolInfo(object _)
{
    ThreadPool.GetAvailableThreads(out int threads, out int completionPorts);
    ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPorts);
    Console.WriteLine($"\t\tWorker Threads - [{threads}:{maxThreads}]");
    Console.WriteLine($"\t\tCompletion Ports - [{completionPorts}:{maxCompletionPorts}]");
}
