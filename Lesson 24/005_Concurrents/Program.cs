using System.Collections.Concurrent;

BlockingCollection<int>[] collections =
[
    new BlockingCollection<int>(10),
    new BlockingCollection<int>(3),
    new BlockingCollection<int>(2),
];

int counter = 0;

for (int i = 0; i < 20; i++)
{
    if (BlockingCollection<int>.TryAddToAny(collections, i) == -1)
    {
        counter++;
    }
}

Console.WriteLine($"Количество невыполненных добавлений равно {counter}");

counter = 0;
while (BlockingCollection<int>.TryTakeFromAny(collections, out int item) != -1)
{
    counter++;
}

Console.WriteLine($"Количество извлеченных элементов = {counter}");

Console.ReadKey();
