using System.Collections.Concurrent;

var bag = new ConcurrentBag<int>();
bag.Add(1);
bag.Add(2);
bag.Add(3);
bag.Add(4);
bag.Add(5);

Enumerate(bag);

bool successPeek = bag.TryPeek(out int peekRes);
bool successTake = bag.TryTake(out int takeRes);

Console.WriteLine(successPeek ? $"TryPeek получил элемент: {peekRes}" : "Нет результата");
Console.WriteLine(successTake ? $"TryTake получил элемент: {takeRes}" : "Нет результата");

Enumerate(bag);
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
