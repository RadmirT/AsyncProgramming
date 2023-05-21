Console.SetWindowSize(80, 35);
IEnumerable<int> range = ParallelEnumerable.Range(1, 100_000_000);

var query = from num in range
                        .AsParallel()
            where (num & (num - 1)) == 0
            select num;

query.ForAll((item) =>
{
    Console.WriteLine($"{item:N0}");
});

Console.WriteLine($"\n\nКонец работы метода Main.");
Console.ReadKey();
