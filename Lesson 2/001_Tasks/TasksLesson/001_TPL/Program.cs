
Console.WriteLine("\nTask.Start");
Task task = new Task(new Action(ThreadOutput));

task.Start();
MainOutput();

Console.WriteLine("\nTaskFactory.StartNew");
var taskFactory = new TaskFactory();
taskFactory.StartNew(new Action(ThreadOutput));
MainOutput();

Console.WriteLine("\nTask.Run");
Task.Run(ThreadOutput);
MainOutput();

Console.WriteLine("\nTask.RunSynchronously");
task = new Task(new Action(ThreadOutput));
task.RunSynchronously();
MainOutput();
Console.ReadLine();

void ThreadOutput()
{
    for (int i = 0; i < 40; i++)
    {
        Console.Write('*');
        Thread.Sleep(75);
    }
}

void MainOutput()
{
    for (int i = 0; i < 40; i++)
    {
        Console.Write('!');
        Thread.Sleep(75);
    }
}
