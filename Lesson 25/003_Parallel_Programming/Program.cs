using System.Diagnostics;

List<Element> elements = new List<Element>();

// Инициализация коллекции в 10 000 000 элементов.
for (int i = 0; i < 10_000_000; i++)
    elements.Add(new Element() { Value = i });

Stopwatch timer = new Stopwatch();
timer.Start();

// Параллельное преобразование.
Parallel.ForEach(elements, (element) =>
{
    element.Value = TransformData(element.Value);
});

timer.Stop();

Console.WriteLine($"Параллельный цикл ForEach     : {timer.ElapsedMilliseconds:N}");

timer.Reset();
timer.Start();

foreach (Element element in elements)
{
    element.Value = TransformData(element.Value); // Последовательное преобразование.
}

timer.Stop();
Console.WriteLine($"Последовательный цикл foreach : {timer.ElapsedMilliseconds:N}");

Console.ReadKey();

int TransformData(int value)
{
    return (int)Math.Sqrt(Math.Pow(value, 2) * 255) / 3;
}

class Element
{
    public int Value { get; set; }
}
