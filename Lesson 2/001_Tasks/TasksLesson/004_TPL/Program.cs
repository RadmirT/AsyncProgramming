Task task = new Task(new Action(DoSomething));

Console.WriteLine($"{task.Status}");

task.Start();

Console.WriteLine($"{task.Status}");
Thread.Sleep(1000);

Console.WriteLine($"{task.Status}");
Thread.Sleep(2000);

Console.WriteLine($"{task.Status}");

void DoSomething()
{
    Thread.Sleep(2000);
}
