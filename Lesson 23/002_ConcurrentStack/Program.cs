using System.Collections.Concurrent;

var stack = new ConcurrentStack<int>();
stack.Push(1);
stack.Push(2);
stack.Push(3);
stack.Push(4);
stack.Push(5);

Enumerate(stack);
Console.ReadKey();

bool successPeek = stack.TryPeek(out int peekRes);
bool successPop = stack.TryPop(out int popRes);

Console.WriteLine(successPeek ? $"TryPeek получил элемент: {peekRes}" : "Нет результата");
Console.WriteLine(successPop ? $"TryPop получил элемент: {popRes}" : "Нет результата");

Enumerate(stack);
Console.ReadKey();

#region ConcurrentStack

stack.PushRange([ 6, 7, 8 ]);
stack.PushRange( [ 9, 10, 11, 12 ], 0, 2);
Enumerate(stack);
Console.ReadKey();

int[] items = new int[5];
int popAmount = stack.TryPopRange(items);

Enumerate(stack);
Console.ReadKey();

Console.WriteLine($"Значение переменной popAmount: {popAmount}");
Enumerate(items);
Console.ReadKey();

items = new int[stack.Count];
popAmount = stack.TryPopRange(items, 0, items.Length);

Enumerate(stack);
Console.ReadKey();

Console.WriteLine($"Значение переменной popAmount: {popAmount}");
Enumerate(items);

#endregion

Console.ReadKey();

void Enumerate<T>(IEnumerable<T> collection)
{
    Console.WriteLine();
    Console.WriteLine($"Количество элементов: {collection.Count()}");
    Console.WriteLine($"Элементы:");

    foreach (var item in collection)
    {
        Console.Write($"{item} ");
    }
    Console.WriteLine();
    Console.WriteLine(new string('-', 80));
}
