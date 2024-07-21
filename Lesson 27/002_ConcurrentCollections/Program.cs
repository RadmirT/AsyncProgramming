using System.Collections.Concurrent;

ConcurrentDictionary<string, double> movieRating = new();
movieRating.TryAdd("Побег из Шоушенка", 9.3);
movieRating.TryAdd("Крестный отец", 9.2);
movieRating.TryAdd("Тёмный рыцарь", 9.0);
movieRating.TryAdd("Santa Barbara", 5.7);

movieRating.TryAdd("Матрица", 8.7);

Console.WriteLine($"Количество фильмов в коллекции - {movieRating.Count}");
EnumerateMovies(movieRating);

movieRating["Бойцовский клуб"]= 8.8;

double santaBarbaraValue = movieRating["Santa Barbara"];
bool isSuccess = movieRating.TryUpdate("Santa Barbara", santaBarbaraValue * 2, santaBarbaraValue);
if (isSuccess == true)
{
    Console.WriteLine($"У сериала Santa Barbara рейтинг - {santaBarbaraValue} \n");
}

isSuccess = movieRating.TryGetValue("Тёмный рыцарь", out double theDarkKnightValue);

if (isSuccess == true)
{
    Console.WriteLine($"У фильма Тёмный рыцарь - {theDarkKnightValue} \n");
}


isSuccess = movieRating.TryRemove("Santa Barbara", out double _);

Console.WriteLine($"Удаление успешно - {isSuccess}");
Console.WriteLine($"Количество фильмов в коллекции после удаления - {movieRating.Count}");
EnumerateMovies(movieRating);
Console.ReadKey();

void EnumerateMovies(ConcurrentDictionary<string, double> movieRating)
{
    Console.WriteLine("Список фильмов:");
    foreach (var movie in movieRating)
    {
        Console.WriteLine($"Фильм \"{movie.Key}\" имеет рейтинг - [{movie.Value}]");
    }

    Console.WriteLine(new string('-', 80));
}
