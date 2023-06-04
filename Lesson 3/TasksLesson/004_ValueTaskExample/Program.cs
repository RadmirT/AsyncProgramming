int res = Sum(5, 3).Result;
Console.WriteLine(res);

Console.ReadKey();

ValueTask<int> Sum(int a, int b)
{
    if (a == 0)
    {
        return new ValueTask<int>(b);
    }
    else if (b == 0)
    {
        return new ValueTask<int>(a);
    }
    else if (a == 0 && b == 0)
    {
        return new ValueTask<int>(0);
    }

    return new ValueTask<int>(Task.Run(() => { return a + b; }));
}
