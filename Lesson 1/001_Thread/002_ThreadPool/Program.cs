Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");
Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

Report();
ThreadPool.QueueUserWorkItem(new WaitCallback(Task1));
Report();
ThreadPool.QueueUserWorkItem(new WaitCallback(Task2));
Report();

Console.ReadKey();
Report();

void Task1(object state)
{
    Console.WriteLine($"Метод Task1 начал выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(2000);
    Console.WriteLine($"Метод Task1 закончил выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
}

void Task2(object state)
{
    Console.WriteLine($"Метод Task2 начал выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(1000);
    Console.WriteLine($"Метод Task2 закончил выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
}

void Report()
{
    ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxPortThreads);
    ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);

    Console.WriteLine($"Рабочие потоки {workerThreads} из {maxWorkerThreads}");
    Console.WriteLine($"IO потоки {portThreads} из {maxPortThreads}");
    Console.WriteLine(new string('-', 80));
}
