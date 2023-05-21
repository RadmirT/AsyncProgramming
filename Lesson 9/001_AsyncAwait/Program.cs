namespace AsyncAwait
{
    internal class Program
    {

        //private static async Task Main()
        private static void Main()
        {
            int x = 3, y = 5;

            Task<int> additionTask = AdditionAsync("[асинхронно]", x, y);

            int syncSum = Addition("[синхронно]", x, y);

            int asyncSum = 0;

            // Разные способы получения результата из асинхронной задачи:
            asyncSum = additionTask.Result;
            //asyncSum = additionTask.GetAwaiter().GetResult();
            //asyncSum = await additionTask;
            Console.WriteLine($"\nРезультат асинхронного выполнения: {asyncSum}.");
            Console.WriteLine($"Результат синхронного выполнения: {syncSum}.");

            Console.WriteLine($"Метод Main завершил свою работу");
            Console.ReadKey();
        }

        private static int Addition(string operationName, int x, int y)
        {
            Console.WriteLine($"Метод Addition вызван {operationName} в потоке: {Thread.CurrentThread.ManagedThreadId}");
            // Thread.Sleep - имитация нагруженной и тяжелой работы.
            Thread.Sleep(3000);
            return x + y;
        }

        private static async Task<int> AdditionAsync(string operationName, int x, int y)
        {


            // Первый способ

            int result = await Task.Run<int>(() => Addition(operationName, x, y));
            return result;

            // ---------------------------------------------- //

            // Второй способ

            //return await Task.Run<int>(() => Addition(operationName, x, y));

            // ---------------------------------------------- //

            // Ошибочный способ
            // return Task.Run<int>(() => Addition(operationName, x, y));
        }
    }
}
