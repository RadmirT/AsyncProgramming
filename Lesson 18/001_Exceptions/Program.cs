var thread = new Thread(Method);

//try
//{
    thread.Start();
//ThreadPool.QueueUserWorkItem(Method);

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

while (true)
{
    Console.Write("*");
    Thread.Sleep(100);
}
void Method(object? _)
{
    //try
    //{
        throw new Exception("Исключение возникшее во вторичном потоке");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}
}
