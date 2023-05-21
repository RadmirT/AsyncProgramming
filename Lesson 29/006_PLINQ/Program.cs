using System.Collections.Concurrent;

ConcurrentDictionary<int, int> dictionary = new ConcurrentDictionary<int, int>();

var query = from num in Enumerable.Range(0, 1000)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount * 2)
            where WhereFilter(num)
            select num;

foreach (var item in query)
{
    Console.Write($"{item} ");
}

Console.WriteLine($"\n\nРезультаты обработки потоков:");
foreach (var item in dictionary)
{
    Console.WriteLine($"Поток #{item.Key} обработал {item.Value} элементов.");
}

Console.WriteLine($"\n\nКонец работы программы.");
Console.ReadKey();

bool WhereFilter(int number)
{
    dictionary.AddOrUpdate(Environment.CurrentManagedThreadId, 1, (key, value) => ++value);
    return number % 2 == 0;
}
