int number = 13;

var result = CalculateFactorialAsync(number);
result.ContinueWith((t) => Console.WriteLine($"\nРезультат - {t.Result}.\n"));

while (true)
{
    Console.Write("*");
    Thread.Sleep(300);
}

Task<long> CalculateFactorialAsync(int number)
{
    return Task.Run(() => CalculateFactorial(number));
}

long CalculateFactorial(int number)
{
    Thread.Sleep(500);

    if (number == 1)
    {
        return number;
    }
    else
    {
        return CalculateFactorial(number - 1) * number;
    }
}

