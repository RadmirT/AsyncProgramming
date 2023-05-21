
int number = 13;

//long result = CalculateFactorial(number);
Task<long> resultTask = CalculateFactorialAsync(number);
resultTask.ContinueWith(task =>
{
    Console.WriteLine($"Результат - {task.Result}");    
}, TaskContinuationOptions.OnlyOnRanToCompletion);


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
