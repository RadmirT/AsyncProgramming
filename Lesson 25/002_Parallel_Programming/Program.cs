using System.Diagnostics;

Console.WriteLine("У Вашего процессора {0} ядер.", Environment.ProcessorCount);

Stopwatch sw = new Stopwatch();

int[] values = [1, 2, 3, 4, 5];

const int array_size = 100_000_000;

double[] data = new double[array_size];

sw.Start();
// Параллельный вариант инициализации массива в цикле.
Parallel.For(0, data.Length, (i) =>
{
    // Имитация сложных вычислений...
    double element = values.Sum() * (i + 250) * 350 * (450 + 2 * i);
    data[i] = element;
});
sw.Stop();

Console.WriteLine($"Параллельно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}.");
sw.Reset();

double[] data2 = new double[array_size];
sw.Start();
// Последовательный вариант инициализации массива в цикле.
for (int i = 0; i < data2.Length; i++)
{
    // Имитация сложных вычислений...
    double element = values.Sum() * (i + 250) * 350 * (450 + 2 * i); 
    data2[i] = element;
}
sw.Stop();

Console.WriteLine($"Последовательно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}");

// Delay.
Console.ReadKey();
