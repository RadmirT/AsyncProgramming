
int number = 13;

long result = CalculateFactorial(number);

Console.WriteLine($"Результат - {result}");

while (true)
{
    Console.Write("*");
    Thread.Sleep(300);
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
