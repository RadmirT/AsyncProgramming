Console.WriteLine("Программа начала свою работу.");
var cts = new CancellationTokenSource();

var task = Task.Run(() => 123);
//var task = Task.Run(() => 123, cts.Token);
//var task = Task.Run(() => { 
//    throw new Exception("Исключение в задаче");
//    return 123;
//});

//cts.Cancel();

task.ContinueWith(t =>
{
    Console.WriteLine($"Результат задачи = {t.Result}");
}, TaskContinuationOptions.OnlyOnRanToCompletion);

task.ContinueWith(t =>
{
    Console.WriteLine($"Задача была отменена");
}, TaskContinuationOptions.OnlyOnCanceled);

task.ContinueWith(t =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Задача была провалена");
    Console.WriteLine($"\tОшибка: {t.Exception.InnerException.GetType()}");
    Console.WriteLine($"\tСообщение об ошибке: {t.Exception.InnerException.Message}");
    Console.ResetColor();
}, TaskContinuationOptions.OnlyOnFaulted);

Console.WriteLine("Программа завершена"); 
Console.ReadKey();
