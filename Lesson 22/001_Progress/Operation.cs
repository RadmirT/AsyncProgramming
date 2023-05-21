namespace _001_Progress;
internal class Operation
{
    public async Task<int> SumNumbersAsync(int[] numbers, CancellationToken token = default, IProgress<int>? progress = default) 
        => await Task.Run(()=>this.SumNumbers(numbers, token, progress), token);

    private int SumNumbers(int[] numbers, CancellationToken token, IProgress<int>? progress)
    {
        token.ThrowIfCancellationRequested();
        var result = 0;

        for( var i = 0;  i < numbers.Length; i++)
        {
            Thread.Sleep(1000);
            result += numbers[i];
            
            token.ThrowIfCancellationRequested();
            progress?.Report((i+1)*100/numbers.Length);
        }
        return result;
    }
}
