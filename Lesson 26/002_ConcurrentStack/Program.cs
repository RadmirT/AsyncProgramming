using System.Collections.Concurrent;

StartTest(TestStack, "Stack");
StartTest(TestConcurrentStack, "ConcurrentStack");
Console.ReadKey();

void StartTest(Action action, string testName)
{
    try
    {
        Console.WriteLine($"\nНачало тестирования {testName}\n");
        action.Invoke();

    }
    catch (Exception e)
    {
        Console.WriteLine($"Ошибка: {e.GetType()}");
        Console.WriteLine($"Сообщение: {e.Message}");
        if (e is AggregateException aggregate)
        {
            Console.WriteLine(aggregate.InnerException.GetType());
            Console.WriteLine(aggregate.InnerException.Message);
        }
    }
    finally
    {
        Console.WriteLine($"Конец тестирования {testName}\n");
        Console.WriteLine(new string('-', 80));
    }
}

void TestConcurrentStack()
{
    var stack = new ConcurrentStack<int>();
    for (int i = 0; i < 10_000; i++)
    {
        stack.Push(i);
    }
    int totalSum = 0;
    Action part = () =>
    {
        int actionSum = 0;
        while (stack.TryPop(out int item))
        {
            actionSum += item;
        }

        Interlocked.Add(ref totalSum, actionSum);
    };
    Parallel.Invoke(part, part, part, part);
    Console.WriteLine($"Результат подсчета ConcurrentStack: {totalSum:N} из 49 995 000.00");

}

void TestStack()
{
    var stack = new Stack<int>();
    for (int i = 0; i < 10_000; i++)
    {
        stack.Push(i);
    }
    int totalSum = 0;
    Action part = () =>
    {
        int actionSum = 0;
        while (stack.TryPop(out int item))
        {
            actionSum += item;
        }

        Interlocked.Add(ref totalSum, actionSum);
    };
    Parallel.Invoke(part, part, part, part);
    Console.WriteLine($"Результат подсчета Stack: {totalSum:N} из 49 995 000.00");
}

