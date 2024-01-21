Task<int> task = Task.Run<int>(DoWork);

//try
//{
//    Console.WriteLine(task.Result);
//    Console.WriteLine("Код продолжил выполнение без ошибок");
//}
//catch (AggregateException ex)
//{
//    Console.WriteLine($"Исключение: {ex.GetType()}");
//    Console.WriteLine($"Сообщение внешнего исключения: {ex.Message}");
    
//    foreach ( var item in ex.InnerExceptions)
//    {
//        Console.WriteLine(new string('-', 80));
//        Console.WriteLine($"Вложенное исключение {item.GetType()}");
//        Console.WriteLine($"Сообщение вложенного исклюяения {item.Message}");
//    }
//}
Console.WriteLine(new string('-', 80));
Console.WriteLine("Нажмите любую клавишу для завершения.");
Console.ReadKey();

int DoWork()
{
    Console.WriteLine("Код метода DoWork до ошибки");
    throw new Exception("Ошибка в методе DoWork");
    Console.WriteLine("Код метода DoWork после ошибки");
    return -1;
}

