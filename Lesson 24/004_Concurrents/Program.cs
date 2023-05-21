using System.Collections.Concurrent;

using (BlockingCollection<int> collection = new BlockingCollection<int>())
{
    var producer = Task.Run(() =>
    {
        for (int i = 0; i < 25; i++)
        {
            collection.Add(i);
            Thread.Sleep(1000);
        }
        collection.CompleteAdding();
    });

    Console.WriteLine($"Спим..... Даем поработать задаче...");
    Thread.Sleep(3000);
    Console.WriteLine($"Проснулись!");

    Console.WriteLine($"\nЭлементы: ");
    foreach (var item in collection.GetConsumingEnumerable())
    {
        Console.Write($"{item} ");
    }
    Console.WriteLine($"\nКоллекция пустая (Count = {collection.Count}).");
    Console.WriteLine($"Работа с коллекцией завершена - {collection.IsCompleted}");
}

Console.WriteLine($"\n\n\nКонец работы!");
Console.ReadKey();
       
