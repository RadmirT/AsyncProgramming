internal class TestSyncContext : SynchronizationContext
{
    public override void OperationCompleted()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Операция закончила выполняться");
        Console.ResetColor();
    }

    public override void OperationStarted()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Операция начала выполняться");
        Console.ResetColor();
    }

    public override void Post(SendOrPostCallback d, object? state)
    {
        try
        {
            d.Invoke(state);
        }
        catch (Exception e)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(e.GetType());
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
