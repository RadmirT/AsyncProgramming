using System.Collections.Concurrent;

using (BlockingCollection<int> collection = new BlockingCollection<int>(new ConcurrentStack<int>(), 5))
{
    Console.WriteLine($"Максимальный внутренний размер: {collection.BoundedCapacity}\n");

    Action<int, int> action = (startValue, endValue) =>
    {
        for (int i = startValue; i <= endValue; i++)
        {
            bool isSuccess = collection.TryAdd(i);
            ShowElementStatus(isSuccess, i);
        }
    };

    var producer1 = Task.Run(() => action.Invoke(1, 10));

    var producer2 = Task.Run(() => action.Invoke(11, 20));

    var consumer = Task.Run(() =>
    {
        List<int> elements = new List<int>();

        while (collection.IsCompleted == false)
        {
            if (collection.TryTake(out int item) == true)
            {
                elements.Add(item);
                Console.WriteLine($"Извлечен элемент [{item}]");
            }

            Thread.Sleep(500);
        }

        Console.WriteLine($"\n\nПолученые элементы ({elements.Count}) через TryTake:");
        foreach (var item in elements)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    });

    await Task.WhenAll(producer1, producer2);
    ShowPropertiesInfo(collection, "До CompleteAdding");

    collection.CompleteAdding();

    ShowPropertiesInfo(collection, "После CompleteAdding");
    await consumer;

    ShowPropertiesInfo(collection, "После работы Consumer");
}

Console.WriteLine($"Работа завершена!");
Console.ReadKey();


void ShowElementStatus(bool isSuccess, int value)
{
    if (isSuccess == true)
    {
        Console.WriteLine($"Добавлен элемент: {value}");
    }
    else
    {
        Console.WriteLine($"\t\t\tНе добавлен элемент: {value}");
    }
}

void ShowPropertiesInfo(BlockingCollection<int> bc, string when)
{
    Console.WriteLine($"\n{when}");
    Console.WriteLine($"Свойство IsCompleted: {bc.IsCompleted}");
    Console.WriteLine($"Свойство IsAddingCompleted: {bc.IsAddingCompleted}");
    Console.WriteLine(new string('-', 80));
}
