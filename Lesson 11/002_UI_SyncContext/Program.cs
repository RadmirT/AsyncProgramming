SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());
Message message = new Message(ApplicationMain, null);
MessageListenter.AddMessage(message);

new MessageListenter().Listen();

Console.ReadKey();

async void ApplicationMain(object _)
{
    Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

    StubMethod1();

    await MethodAsync();
    StubMethod2();

    Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
}
async Task MethodAsync()
{
    Console.WriteLine(new string('-', 80));

    Console.WriteLine($"Код до оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
    var task =Task.Run(Method);
    Thread.Sleep(5000);
    await task.ConfigureAwait(false);
    //await Task.Run(Method).ConfigureAwait(false);
    Console.WriteLine($"Код после оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");

    Console.WriteLine(new string('-', 80));
}
void StubMethod1()
{
    Console.WriteLine($"Пример метода 1!!! Id потока: {Thread.CurrentThread.ManagedThreadId}");
}

void StubMethod2()
{
    Console.WriteLine($"Пример метода 2!!! Id потока: {Thread.CurrentThread.ManagedThreadId}");
}


void Method()
{
    Console.WriteLine($"Метод {nameof(Method)} был выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
}

