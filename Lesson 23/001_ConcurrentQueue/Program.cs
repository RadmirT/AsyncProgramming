using System.Collections.Concurrent;

var queue = new ConcurrentQueue<int>();
queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);
queue.Enqueue(4);
queue.Enqueue(5);

Enumerate(queue);

bool successPeek = queue.TryPeek(out int peekRes);
bool successDequeue = queue.TryDequeue(out int dequeueRes);

Console.WriteLine(successPeek ? $"TryPeek получил элемент: {peekRes}" : "Нет результата");
Console.WriteLine(successDequeue ? $"TryDequeue получил элемент: {dequeueRes}" : "Нет результата");

Enumerate(queue);

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
