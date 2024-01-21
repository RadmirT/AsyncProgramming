var parent = new Task(() =>
{
    new Task(() =>
    {
        Thread.Sleep(500);
        Console.WriteLine("Вложенная задача #1 завершила свою работу");
    }).Start();

    new Task(() =>
    {
        Thread.Sleep(600);
        Console.WriteLine("Вложенная задача #2 завершила свою работу");
    }).Start();

    new Task(() =>
    {
        Thread.Sleep(700);
        throw new Exception("Ошибка в вложенной задаче #3");
    }).Start();

    new Task(() =>
    {
        Thread.Sleep(800);
        Console.WriteLine("Вложенная задача #4 завершила свою работу");
    }).Start();

    new Task(() =>
    {
        new Task(() => throw new Exception("Ошибка в вложенной задаче #5.1 второго уровня вложенности"), TaskCreationOptions.AttachedToParent).Start();
        Thread.Sleep(900);
        throw new Exception("Ошибка в вложенной задаче #5");
    }).Start();

    new Task(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Вложенная задача #6 завершила свою работу");
    }).Start();
});

parent.Start();

try
{
    parent.Wait();
}
catch (AggregateException ex)
{
    HandleTaskExeption(ex);
}
Console.WriteLine(new string('*', 80));
Console.WriteLine($"Статус родительской задачи {parent.Status}");
Console.WriteLine("Нажмите любую клавишу для завершения.");
Console.ReadKey();

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
