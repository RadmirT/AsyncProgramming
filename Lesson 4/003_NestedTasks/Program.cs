var parent = new Task<string>(() =>
{
    var task1 = new Task<int>(() => Addition(5000));
    var task2 = new Task<int>(() => Addition(10000));
    var task3 = new Task<int>(() => Addition(50000));
    task1.Start();
    task2.Start();
    task3.Start();
    task1.ContinueWith(t => Console.WriteLine($"Task 1 result = {t.Result} "));
    task2.ContinueWith(t => Console.WriteLine($"Task 2 result = {t.Result} "));
    task3.ContinueWith(t => Console.WriteLine($"Task 3 result = {t.Result} "));
    return "completed";
    //return (task1.Result + task2.Result + task3.Result).ToString();
});

parent.Start();
Console.WriteLine($"Parent task result = {parent.Result}");
Console.ReadKey();

int Addition(int length)
{
    var sum = 0;
    Thread.Sleep(1000);
    for (int i = 0; i < length; i++)
    {
        sum++;
    }
    return sum;
}
