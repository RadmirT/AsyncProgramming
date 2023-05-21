using System.Threading.Tasks;

Console.WriteLine("Программа начала свою работу");

try
{
    await OperationAsync();
}
catch (Exception ex)
{
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Исключение - {ex.GetType}");
    Console.WriteLine($"Сообщение - {ex.Message}");
    Console.ResetColor();

}

#region Обработка нескольких исколючений

// Обработка исключение с помощью свойства Exception
#region Способ 1 

//var tasks = OperationsAsync();
//try
//{
//    await tasks;
//}
//catch
//{
//    AggregateException exception = tasks.Exception;
//    HandleTaskExeption(exception);
//}

#endregion

// Обработка исключение с помощью продолжения (ContinueWith)
#region Способ 2
//var tasks = OperationsAsync();
//try
//{
//    await tasks.ContinueWith(t => { }, TaskContinuationOptions.ExecuteSynchronously);
//    tasks.Wait();
//}
//catch (AggregateException ex)
//{
//    HandleTaskExeption(ex);
//}

#endregion

// Обработка исключения внутри продолжения (ContinueWith)
#region Способ 3

//await OperationsAsync().ContinueWith(tasks =>
//    {
//        try
//        {
//            tasks.Wait();
//        }
//        catch (AggregateException ex)
//        {
//            HandleTaskExeption(ex);
//        }
//    }, TaskContinuationOptions.ExecuteSynchronously);


#endregion

#region Способ 4
// Обработка исключения внутри продолжения (ContinueWith) c параметром OnlyOnFaulted 
//await OperationsAsync().ContinueWith(t =>
//{
//    HandleTaskExeption(t.Exception);
//},
//    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

#endregion
#endregion

Console.WriteLine("Программа завершила свою работу");
Console.ReadKey();

async Task OperationAsync()
{
    Console.WriteLine("Начало работы метода OperationAsync");
    await Task.Run(() => throw new Exception("метод OperationAsync выбросил  исключение"));
    Console.WriteLine("Окончание работы метода OperationAsync");
}

Task OperationsAsync()
{
    Action<int> operaton = (taskNumber) =>
    {
        Thread.Sleep(taskNumber * 300);
        throw new Exception($"Задача {taskNumber} выбросила исключение");
    };

    var task1 = Task.Run(() => operaton(1));
    var task2 = Task.Run(() => operaton(2));
    var task3 = Task.Run(() => operaton(3));

    return Task.WhenAll(task1, task2, task3);
}

void HandleTaskExeption(AggregateException exception)
{
    foreach (var item in exception.InnerExceptions)
    {
        if (item is AggregateException aggregateException)
        {
            HandleTaskExeption(aggregateException);
        }
        else
        {
            Console.WriteLine($"Сообщение из исключения - {item.Message}");
        }
    }
}
