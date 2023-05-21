Console.WriteLine($"Task Id метода Main : {Task.CurrentId ?? -1}");
Console.WriteLine($"Thread Id метода Main : {Thread.CurrentThread.ManagedThreadId}");
Console.WriteLine(new string('-', 80));

Task task = new Task(new Action(DoSomething), TaskCreationOptions.PreferFairness |
                                              TaskCreationOptions.LongRunning);

task.Start(); // Запуск задачи
Thread.Sleep(50); // Даем поработать методу DoSomething

for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"            Метод Main выполняется.");
    Thread.Sleep(80);
}

Console.ReadKey();

void DoSomething()
{
    Console.WriteLine($"Task Id метода DoSomething : {Task.CurrentId}");
    Console.WriteLine($"Thread Id метода DoSomething : {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine(new string('-', 80));

    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"            Задача выполняется.");
        Thread.Sleep(100);
    }

    Console.WriteLine($"Задача завершена в потоке : {Thread.CurrentThread.ManagedThreadId}.");
}
