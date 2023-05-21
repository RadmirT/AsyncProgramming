var parent = new Task(() =>
{
    new Task(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Nested task 1 completed");
    }).Start();

    new Task(() =>
    {
        Thread.Sleep(2000);
        Console.WriteLine("Nested task 2 completed");
    }).Start();

    Thread.Sleep(200);
    Console.WriteLine("Parent task completed");
});

parent.Start();
parent.Wait();
Console.WriteLine("Completed");
Console.ReadKey();
