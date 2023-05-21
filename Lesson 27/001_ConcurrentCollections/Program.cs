Dictionary<string, double> movieRating = new()
            {
                { "Побег из Шоушенка", 9.3},
                { "Крестный отец", 9.2},
                { "Тёмный рыцарь", 9.0 },
                { "Santa Barbara", 5.7},
            };

movieRating.Add("Матрица", 8.7);

Console.WriteLine($"Количество фильмов в коллекции - {movieRating.Count}");
EnumerateMovies(movieRating);


movieRating["Бойцовский клуб"] = 8.8;
movieRating["Santa Barbara"] = movieRating["Santa Barbara"] * 2;

Console.WriteLine($"У сериала Santa Barbara рейтинг - {movieRating["Santa Barbara"]} \n");
Console.WriteLine($"У фильма Тёмный рыцарь рейтинг - {movieRating["Тёмный рыцарь"]} \n");

movieRating.Remove("Santa Barbara");
Console.WriteLine($"Количество фильмов в коллекции после удаления - {movieRating.Count}");

EnumerateMovies(movieRating);
Console.ReadKey();

void EnumerateMovies(Dictionary<string, double> movieRating)
{
    Console.WriteLine("Список фильмов:");
    foreach (var movie in movieRating)
    {
        Console.WriteLine($"Фильм \"{movie.Key}\" имеет рейтинг - [{movie.Value}]");
    }

    Console.WriteLine(new string('-', 80));
}
