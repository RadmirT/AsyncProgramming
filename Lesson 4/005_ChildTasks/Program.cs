var parent = new Task(() =>
{
    new Task(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Тask 1 completed");
    }, TaskCreationOptions.AttachedToParent).Start();

    new Task(() =>
    {
        Thread.Sleep(2000);
        Console.WriteLine("Тask 2 completed");
    }, TaskCreationOptions.AttachedToParent).Start();

    Thread.Sleep(200);
    Console.WriteLine("Parent task completed");
}, TaskCreationOptions);

parent.Start();
parent.Wait();
Console.WriteLine("Completed");
Console.ReadKey();
