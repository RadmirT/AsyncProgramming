CancellationTokenSource parentCts1 = new CancellationTokenSource();
CancellationTokenSource parentCts2 = new CancellationTokenSource();
CancellationTokenSource parentCts3 = new CancellationTokenSource();

CancellationTokenSource linkedCts4 = CancellationTokenSource.CreateLinkedTokenSource(parentCts1.Token, parentCts2.Token);
CancellationTokenSource linkedCts5 = CancellationTokenSource.CreateLinkedTokenSource(parentCts3.Token, linkedCts4.Token);

CancellationToken parentToken1 = parentCts1.Token;
CancellationToken parentToken2 = parentCts2.Token;
CancellationToken parentToken3 = parentCts3.Token;

CancellationToken linkedToken4 = linkedCts4.Token;
CancellationToken linkedToken5 = linkedCts5.Token;

var t1 = Task.Run(() => Do(1, parentToken1), parentToken1);
var t2 = Task.Run(() => Do(2, parentToken2), parentToken2);
var t3 = Task.Run(() => Do(3, parentToken3), parentToken3);
var t4 = Task.Run(() => Do(4, linkedToken4), linkedToken4);
var t5 = Task.Run(() => Do(5, linkedToken5), linkedToken5);

#region Регистрация обработки отмены
parentToken1.Register(() => Canceled(1));
parentToken2.Register(() => Canceled(2));
parentToken3.Register(() => Canceled(3));
linkedToken4.Register(() => Canceled(4));
linkedToken5.Register(() => Canceled(5));
#endregion

parentCts1.CancelAfter(1500);
//parentCts2.CancelAfter(1500);
//parentCts3.CancelAfter(1500);
//linkedCts4.CancelAfter(1500);
//linkedCts5.CancelAfter(1500);

Console.ReadKey();

void Do(int taskId, CancellationToken cancellationToken)
{
    Console.WriteLine($"Задача {taskId} начала свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(1000);
    int sum = 0;
    for(int i =  0; i < 150; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Thread.Sleep(1);
        sum += i;
    }

    Console.WriteLine($"Задача {taskId} зарершила свою работу в потоке {Thread.CurrentThread.ManagedThreadId}. Результат {sum}");
}

void Canceled(int taskId)
{
    Console.WriteLine($"--- Задача {taskId}  в потоке {Thread.CurrentThread.ManagedThreadId} была отменена ----");
}
