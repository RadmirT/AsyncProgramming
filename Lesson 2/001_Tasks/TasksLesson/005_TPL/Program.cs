Task[] tasks = new Task[] {
                new Task(DoSomething, 1000),
                new Task(DoSomething, 800),
                new Task(DoSomething, 2000),
                new Task(DoSomething, 1000),
                new Task(DoSomething, 3500),
            };

Console.WriteLine($"Метод Main выполняется..");
foreach (Task task in tasks)
    task.Start();

Console.WriteLine($"Метод Main ожидает..");

//foreach (Task task in tasks)
//    task.Wait();

//Task.WaitAll(tasks);
Task.WaitAny(tasks);
Console.WriteLine($"Ожидание окончено.");

Console.WriteLine($"Метод Main продолжает свою работу");

for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Main({i})");
}

Console.WriteLine("Метод Main завершен. Нажмите любую клавишу");
Console.ReadKey();

void DoSomething(object sleepTime)
{
    Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");

    Thread.Sleep((int)sleepTime);

    Console.WriteLine($"\t\tЗадача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
}
