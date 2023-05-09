using AsyncProgramming;

Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

ThreadPoolWorker<int> threadPoolWorker = new ThreadPoolWorker<int>(SumNumber);
threadPoolWorker.Start(1000);

while (threadPoolWorker.Completed == false)
{
    Console.Write("*");
    Thread.Sleep(35);
}

Console.WriteLine();
Console.WriteLine($"Результат асинхронной операции = {threadPoolWorker.Result:N}");

int SumNumber(object arg)
{
    int number = (int)arg;
    int sum = 0;

    for (int i = 0; i < number; i++)
    {
        sum += i;
        Thread.Sleep(1);
    }

    return sum;
}
