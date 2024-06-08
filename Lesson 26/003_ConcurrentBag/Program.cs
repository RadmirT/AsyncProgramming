using System.Collections.Concurrent;

await ConcurrentBag_UseTheOtherThreads();
//await ConcurrentBag_UseTheSameThreads();

async Task ConcurrentBag_UseTheOtherThreads()
{
    ConcurrentBag<string> bag = new ConcurrentBag<string>();

    Action action = () =>
    {
        for (int i = 0; i < 10; i++)
        {
            bag.Add($"({Thread.CurrentThread.ManagedThreadId} - {i})");
        }
    };

    var thread1 = new Thread(_=>action());
    var thread2 = new Thread(_ => action());

    thread1.Start();
    thread2.Start();
    thread1.Join();
    thread2.Join();

    Console.WriteLine($"Количество элементов в коллекции {bag.Count}. Элементы:");
    foreach (var item in bag)
    {
        Console.Write($"{item} ");
    }

    Console.WriteLine();

    action = () =>
    {
        while (bag.TryTake(out string? item))
        {
            Thread.Sleep(1);
            Console.WriteLine($"Элемент {item} взят в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    };
    Thread.Sleep(500);
    var t1 = Task.Run(action);
    var t2 = Task.Run(action);
    await Task.WhenAll(t1, t2);
}

static async Task ConcurrentBag_UseTheSameThreads()
{
    ConcurrentBag<string> bag = new ConcurrentBag<string>();

    Action action = () =>
    {
        for (int i = 0; i < 20; i++)
        {
            bag.Add($"({Thread.CurrentThread.ManagedThreadId} - {i})");
        }
    };

    var t1 = Task.Run(action);
    var t2 = Task.Run(action);
    Action<Task> continuationAction = (t) =>
    {
        while (bag.TryTake(out string? item))
        {
            Thread.Sleep(1);
            Console.WriteLine($"Элемент {item} взят в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    };

    var c1 = t1.ContinueWith(continuationAction, TaskContinuationOptions.ExecuteSynchronously);
    var c2 = t2.ContinueWith(continuationAction, TaskContinuationOptions.ExecuteSynchronously);

    await Task.WhenAll(c1, c2);
}
