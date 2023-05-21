using System.Text;

try
{
    string result = await APM_TAP1();
    Console.WriteLine($"File content: {result}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.GetType());
    Console.WriteLine(ex.Message);
}

Console.ReadKey();


Task<string> APM_TAP1()
{
    TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

    using (FileStream fs = new FileStream("test.txt",
        FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite,
        4096, true))
    {
        byte[] array = new byte[250];
        fs.BeginRead(array, 0, array.Length, (iAsyncResult) =>
        {
            try
            {
                int bytes = fs.EndRead(iAsyncResult);
                Console.WriteLine($"Bytes read - {bytes}");
                tcs.TrySetResult(Encoding.UTF8.GetString(array));
            }
            catch (OperationCanceledException ex)
            {
                tcs.TrySetCanceled(ex.CancellationToken);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

        }, null);

    }

    return tcs.Task;
}

Task<string> APM_TAP2()
{
    TaskFactory taskFactory = new TaskFactory();

    using (FileStream fs = new FileStream("test.txt",
                FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite,
                4096, true))
    {
        byte[] array = new byte[250];

        return taskFactory.FromAsync(fs.BeginRead, (iAsyncResult) =>
        {
            int bytes = fs.EndRead(iAsyncResult);
            Console.WriteLine($"Bytes read - {bytes}");

            return Encoding.UTF8.GetString(array);

        }, array, 0, array.Length, null);
    }
}
