bool flag = false;

Task<int> task1 = new Task<int>(new Func<int>(GetIntResult));
task1.Start();
Console.WriteLine($"Результат асинхронной операции (Int) #1 - {task1.Result}");
Thread.Sleep(1000);

TaskFactory taskFactory = new TaskFactory();
Task<bool> task2 = taskFactory.StartNew(new Func<bool>(GetBoolResult));
Console.WriteLine($"Результат асинхронной операции (Bool) #2 - {task2.Result}");

Thread.Sleep(1000);

Task<bool> task3 = Task.Run(new Func<bool>(GetBoolResult));
Console.WriteLine($"Результат асинхронной операции (Bool) #3 - {task3.Result}");

int GetIntResult() => 1;

bool GetBoolResult()
{
    if (flag == true)
    {
        flag = false;
        return true;
    }
    else
    {
        flag = true;
        return false;
    }
}
