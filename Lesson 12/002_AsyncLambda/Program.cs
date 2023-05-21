Func<Task> func = async () =>
{
    Console.WriteLine($"Labmda start");

    await Task.Run(() => Console.WriteLine("Task"));

    Console.WriteLine($"Labmda end");
};

await func();

Console.ReadKey();
