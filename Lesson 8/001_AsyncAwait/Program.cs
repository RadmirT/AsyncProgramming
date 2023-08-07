namespace _001_AsyncAwait
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            await WriteCharAsync('#'); // Запуск метода асинхронно
            WriteChar('*'); // Запуск метода синхронно

            Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
            Console.ReadKey();
        }

        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

            await Task.Run(() => WriteChar(symbol));

            Console.WriteLine($"Метод WriteCharAsync закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Id потока - [{Thread.CurrentThread.ManagedThreadId}]. Id задачи - [{Task.CurrentId}]");
            Thread.Sleep(500);

            for (int i = 0; i < 80; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
}