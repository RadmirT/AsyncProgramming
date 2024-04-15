using System.Collections.Concurrent;

IProducerConsumerCollection<int> collection = new ConcurrentQueue<int>();
//IProducerConsumerCollection<int> collection = new ConcurrentStack<int>();
//IProducerConsumerCollection<int> collection = new ConcurrentBag<int>();
collection.TryAdd(1);
collection.TryAdd(2);
collection.TryAdd(3);
collection.TryAdd(4);
collection.TryAdd(5);

Enumerate(collection);

bool successTake = collection.TryTake(out int takeRes);

Console.WriteLine(successTake ? $"TryTake получил элемент: {takeRes}" : "Нет результата");

Enumerate(collection);
Console.ReadKey();


void Enumerate<T>(IEnumerable<T> collection)
{
    Console.WriteLine();
    Console.WriteLine($"Количество элементов: {collection.Count()}");
    Console.WriteLine($"Элементы:");

    foreach (var item in collection)
    {
        Console.Write($"{item} ");
    }
    Console.WriteLine();
    Console.WriteLine(new string('-', 80));
}
