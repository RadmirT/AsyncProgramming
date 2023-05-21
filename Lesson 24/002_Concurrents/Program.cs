using System.Collections.Concurrent;

using (BlockingCollection<int> collection = new BlockingCollection<int>(new ConcurrentStack<int>()))
{
    Console.WriteLine($"Максимальный внутренний размер: {collection.BoundedCapacity}\n");

    Action<int, int> action = (startValue, endValue) =>
    {
        for (int i = startValue; i <= endValue; i++)
        {
            collection.Add(i);
        }
    };

    var producer1 = Task.Run(() => action.Invoke(1, 25));

    var producer2 = Task.Run(() => action.Invoke(26, 50));

    var consumer = Task.Run(() =>
    {
        try
        {
            while (true)
                Console.Write($"{collection.Take()} ");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nСообщение ошибки: {ex.Message}");
        }
    });

    await Task.WhenAll(producer1, producer2);

    collection.CompleteAdding();
    await consumer;
}

Console.WriteLine($"\nРабота завершена!");
Console.ReadKey();

