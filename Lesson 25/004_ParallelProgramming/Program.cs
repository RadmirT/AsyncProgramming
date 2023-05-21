Random random = new Random();
int breakIndex = random.Next(50, 80);
int[] data = new int[100];

Action<int, ParallelLoopState> loopBody = (i, state) =>
{
    Thread.Sleep(random.Next(1, 150));

    if (state.ShouldExitCurrentIteration == true)
    {
        if (state.LowestBreakIteration != null)
        {
            Console.WriteLine($"Log: Был использован метод Break(). Прерываю итерацию #{i}");
        }
        else if (state.IsStopped == true)
        {
            Console.WriteLine($"Log: Был использован метод Stop(). Прерываю итерацию #{i}");
        }
        else if (state.IsExceptional == true)
        {
            Console.WriteLine($"Log: Было выброшено исключение. Прерываю итерацию #{i}");
        }

        return;
    }

    if (i == breakIndex)
    {
        //state.Break();
        //state.Stop();
        throw new Exception($"Ошибка в параллельной итерации #{i}");
    }

    data[i] = i;
};

try
{
    ParallelLoopResult loopResult = Parallel.For(0, data.Length, loopBody);

    if (loopResult.IsCompleted == false)
    {
        string breakInfo = loopResult.LowestBreakIteration == null ? "" : $"Цикл прерван на {loopResult.LowestBreakIteration} итерации.";
        Console.WriteLine($"Параллельный цикл завершился преждевременно. {breakInfo}");
    }
    else
    {
        Console.WriteLine($"Параллельный цикл завершен успешно.");
    }
}
catch (AggregateException ex)
{
    Console.WriteLine();
    Console.WriteLine(new string('-', 80));
    Console.WriteLine($"Произошли ошибки при выполнение цикла:\n");
    foreach (var exception in ex.InnerExceptions)
    {
        Console.WriteLine($"Ошибка {exception.GetType()}");
        Console.WriteLine($"Сообщение: {exception.Message}");
        Console.WriteLine(new string('-', 80));
    }
}

Console.WriteLine($"\nРезультаты выполнения параллельного цикла: ");
foreach (var item in data)
{
    Console.Write($"{item} ");
}

Console.ReadKey();
