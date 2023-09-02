using System.Reflection;

Console.SetWindowSize(90, 40);

ShowData("Main выполнился до await");

var mainTask = new Task<Task>(MethodAsync);
mainTask.Start(new AwaitableTestTaskScheduler());
await await mainTask;
//var result = await mainTask;
//await result;

ShowData("Main выполнился после await");

Console.ReadKey();

async Task MethodAsync()
{
    ShowData("MethodAsync выполнился до await");

    var task = new Task(TestMethod);
    task.Start();

    await task.ConfigureAwait(false);
    ShowData("MethodAsync выполнился после await");
}

void TestMethod()
{
    ShowData("TestMethod выполнился");
}

void ShowData(string description)
{
    Console.WriteLine($"{description}");

    Console.WriteLine($"Имя потока: {Thread.CurrentThread.Name} ");
    Console.WriteLine($"Id потока: {Thread.CurrentThread.ManagedThreadId}. Поток из пула потоков: {Thread.CurrentThread.IsThreadPoolThread}");
    Console.WriteLine($"Id задачи: {Task.CurrentId}");
    Console.WriteLine($"Текущий планировщик задач: {typeof(TaskScheduler).GetProperty("InternalCurrent", BindingFlags.Static | BindingFlags.NonPublic).GetValue(typeof(TaskScheduler))}");

    Console.WriteLine(new string('-', 80));
    Console.WriteLine();
}
