
Task task = new Task(new Action(ThreadOutput));
task.Start();
MainOutput();

var taskFactory = new TaskFactory();
taskFactory.StartNew(new Action(ThreadOutput));
MainOutput();

Task.Run(ThreadOutput);
MainOutput();

task = new Task(new Action(ThreadOutput));
task.RunSynchronously();
MainOutput();

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
