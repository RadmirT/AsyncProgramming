Random random = new Random();
int[] sequence = new int[500_000_000];

Parallel.For(0, sequence.Length, (i) =>
{   
    if (random.Next(0, 10000) == 50)
    {
        sequence[i] = i * (-1);
    }
    else
    {
        sequence[i] = i;
    }
});

var parallelQuery = from num in sequence.AsParallel()
                    where num < 0
                    select num;

foreach (var num in parallelQuery)
{
    Console.Write($"{num, 11} ");
}

Console.WriteLine($"\n\n\nКонец работы программы.");
Console.ReadKey();
