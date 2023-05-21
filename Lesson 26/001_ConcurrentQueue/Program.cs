using System.Collections.Concurrent;

StartTest(TestQueue, "Queue");
StartTest(TestConcurrentQueue, "ConcurrentQueue");
Console.ReadKey();

void StartTest(Action action, string testName)
{
    try
    {
        Console.WriteLine($"\nНачало тестирования {testName}\n");
        action.Invoke();

    }
    catch (Exception e)
    {
        Console.WriteLine($"Ошибка: {e.GetType()}");
        Console.WriteLine($"Сообщение: {e.Message}");
        if (e is AggregateException aggregate)
        {
            Console.WriteLine(aggregate.InnerException.GetType());
            Console.WriteLine(aggregate.InnerException.Message);
        }
    }
    finally
    {
        Console.WriteLine($"Конец тестирования {testName}\n");
        Console.WriteLine(new string('-', 80));
    }
}

void TestQueue()
{
    var queue = new Queue<int>();
    for (int i = 0; i < 10_000; i++)
    {
        queue.Enqueue(i);
    }
    int totalSum = 0;
    Action part = () =>
    {
        int actionSum = 0;
        while (queue.Count > 0)
        {
            int item = queue.Dequeue();
            actionSum += item;
        }

        Interlocked.Add(ref totalSum, actionSum);
    };
    Parallel.Invoke(part, part, part, part);
    Console.WriteLine($"Результат подсчета Queue: {totalSum:N} из 49 995 000.00");
}

void TestConcurrentQueue()
{
    var queue = new ConcurrentQueue<int>();
    for (int i = 0; i < 10_000; i++)
    {
        queue.Enqueue(i);
    }
    int totalSum = 0;
    Action part = () =>
    {
        int actionSum = 0;
        while (queue.TryDequeue(out int item))
        {
            actionSum += item;
        }

        Interlocked.Add(ref totalSum, actionSum);
    };
    Parallel.Invoke(part, part, part, part);
    Console.WriteLine($"Результат подсчета ConcurrentQueue: {totalSum:N} из 49 995 000.00");

}
