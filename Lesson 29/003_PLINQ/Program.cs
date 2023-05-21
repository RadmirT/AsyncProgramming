Console.SetWindowSize(100, 40);

var parallelQuery = from num in Enumerable.Range(0, 100_000)
                        .AsParallel()
                    where num % 2 == 0
                    select num;

var query = from n in parallelQuery
                .AsSequential()
            orderby n
            select n;

foreach (var item in query)
{
    Console.Write($"{item} ");
}


Console.WriteLine($"\n\nКонец работы программы");
Console.ReadKey();
