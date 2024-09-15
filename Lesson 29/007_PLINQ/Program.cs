using System.Collections.Concurrent;

ConcurrentDictionary<int, int> dictionary = new ConcurrentDictionary<int, int>();

Console.SetWindowSize(100, 40);

var parallelQuery = Enumerable.Range(0, 10_000)
                        .AsParallel()
                        //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                        .Where(WhereFilter)
                        .Select((num, index) => num);

foreach (var item in parallelQuery)
{
    Console.Write($"{item} ");
}

Console.WriteLine($"\n\nРезультаты обработки потоков:");
foreach (var item in dictionary)
{
    Console.WriteLine($"Поток #{item.Key} обработал {item.Value} элементов.");
}


Console.WriteLine($"\n\nКонец работы метода Main");
Console.ReadKey();

bool WhereFilter(int number, int index)
{
    dictionary.AddOrUpdate(Thread.CurrentThread.ManagedThreadId, 1, (key, value) => ++value);
    return number % 2 == 0;
}

