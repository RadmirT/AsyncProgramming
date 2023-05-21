internal class ConsoleSynchronizationContext : SynchronizationContext
{
    public override void OperationStarted()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Операция начата");
        Console.ResetColor();
    }

    public override void OperationCompleted()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Операция завершена");
        Console.ResetColor();
    }


    public override void Post(SendOrPostCallback d, object state)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Post выполнился");
            Console.ResetColor();

            Message message = new Message(d, state);
            MessageListenter.AddMessage(message);
        }, null);
    }

    public override void Send(SendOrPostCallback d, object state)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Send выполнился");
        Console.ResetColor();

        Message message = new Message(d, state);
        MessageListenter.AddMessage(message);
    }
}
