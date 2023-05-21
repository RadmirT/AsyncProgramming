int a = 3, b = 2;

CalcParameters parmeters;
parmeters.a = a;
parmeters.b = b;

Task<int> task = new Task<int>(Calc1, parmeters);
task.Start();

Console.WriteLine($"Сумма чисел : {task.Result}");
Console.WriteLine(new string('-', 80));

Task<int> lambda = new Task<int>(() => Calc2(a, 5));
lambda.Start();

Console.WriteLine($"Сумма чисел : {lambda.Result}");
Console.WriteLine(new string('-', 80));

// Еще удобнее :
Task<int> taskRun = Task<int>.Run<int>(() =>
{
    int a1 = 5;
    int b1 = 5;
    return Calc2(a1, b1) + Calc2(a, b);
});

Console.WriteLine($"Сумма чисел : {taskRun.Result}");

Console.ReadKey();


int Calc1(object arg)
{
    CalcParameters box = (CalcParameters)arg;
    return box.a + box.b;
}

int Calc2(int a, int b)
{
    return a + b;
}

public struct CalcParameters
{
    public int a;
    public int b;
}
