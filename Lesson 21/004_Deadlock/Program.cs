Console.WriteLine("Программа начала свою работу.");

var syncRoot1 = new object();
var syncRoot2 = new object();

var t1 = Task.Run(() =>
{
    lock (syncRoot1)
    {
        Thread.Sleep(1000);
        lock (syncRoot2)
        {
            Console.WriteLine("Задача #1  выполнилась");
        }
    }
});

var t2 = Task.Run(() =>
{
    lock (syncRoot2)
    {
        Thread.Sleep(1000);
        lock (syncRoot1)
        {
            Console.WriteLine("Задача #2  выполнилась");
        }
    }
});

//var t1 = Task.Run(Solution);
//var t2 = Task.Run(Solution);

await Task.WhenAll(t1, t2);
Console.WriteLine("Программа завершена");
Console.ReadKey();


//void Solution()
//{
//    lock (syncRoot1)
//    {
//        Thread.Sleep(1000);
//        lock (syncRoot2)
//        {
//            Console.WriteLine($"Задача #{Task.CurrentId}");
//        }
//    }
//}