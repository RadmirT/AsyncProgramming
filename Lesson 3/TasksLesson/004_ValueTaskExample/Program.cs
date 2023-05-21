int res = Sum(5, 3).Result;
Console.WriteLine(res);

Console.ReadKey();

ValueTask<int> Sum(int a, int b)
{
    if (a == 0)
    {
        //return Task.FromResult(b); 
        return new ValueTask<int>(b);
    }
    else if (b == 0)
    {
        //return Task.FromResult(a); 
        return new ValueTask<int>(a);
    }
   
    return new ValueTask<int>(Task.Run(() => a + b));
}
