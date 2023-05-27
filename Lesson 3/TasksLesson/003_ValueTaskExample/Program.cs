CalculateAndShowAsync(-1).GetAwaiter().GetResult();
Console.ReadKey();

ValueTask CalculateAndShowAsync(int ceiling)
{
    if (ceiling < 0)
    {
        //return Task.CompletedTask; 
        return new ValueTask();
    }
    else
    {
        return new ValueTask(Task.Run(() =>
        {
            Calculator(ceiling);
        }));

        //return Task.Run(() =>
        //    {
        //        Calculator(ceiling);
        //    });
    }
}

void Calculator(int ceiling)
{
    int sum = 0;
    for (int i = 0; i < ceiling; i++)
        sum += i;
    Console.WriteLine($"Результат - {sum}. Найден в задаче #{Task.CurrentId}, в потоке #{Thread.CurrentThread.ManagedThreadId}");
}


