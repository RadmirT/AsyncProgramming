using System.Collections.Concurrent;

await StartTest(TestDictionary, "Dictionary");

await StartTest(TestConcurrentDictionary, "ConcurrentDictionary");

Console.ReadKey();

async Task StartTest(Func<Task> test, string testName)
{
    try
    {
        Console.WriteLine($"\nНачало тестирования {testName}\n");
        await test.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nОшибка {ex.GetType()}");
        Console.WriteLine($"Сообщение: {ex.Message}");

        if (ex is AggregateException aggregate)
        {
            Console.WriteLine(aggregate.InnerException.GetType());
            Console.WriteLine(aggregate.InnerException.Message);
        }
    }
    finally
    {
        Console.WriteLine($"\nКонец тестирования {testName}");
        Console.WriteLine(new string('-', 80));
    }
}


async Task TestDictionary()
{
    int itemsCount = 200;

    // capacity - указывает начальное количество элементов для словаря.
    Dictionary<int, int> dictionary = new Dictionary<int, int>(capacity: itemsCount);

    // Инициализация словаря
    Action addAction = () =>
    {
        for (int i = 0; i < itemsCount; i++)
        {
            dictionary.Add(i, i + 1);
        }
    };

    Parallel.Invoke(addAction, addAction);
    //addAction.Invoke();

    // Просмотр значений элементов.
    Action<int> viewAction = (int key) =>
    {
        // Первый способ просмотреть значение элемента:
        Console.WriteLine($"По ключу #{key} значение = {dictionary[key]}");

        // Второй способ просмотреть значение элемента:
        //if (dictionary.TryGetValue(key, out int value))
        //{
        //    Console.WriteLine($"По ключу #{key} значение = {value}");
        //}
    };

    // Изменение значений элементов.
    Action<int> updateAction = (int key) =>
    {
        // Первый способ изменения значения элемента:
        dictionary[key] = dictionary[key] * dictionary[key];

        // Второй способ изменения значения элемента:
        //if (dictionary.TryGetValue(key, out int value))
        //{
        //    dictionary[key] = value * value;
        //}
    };


    // Просмотр значений по ключам : 15, 30, 100.
    Parallel.Invoke(() => viewAction.Invoke(15), () => viewAction.Invoke(30), () => viewAction.Invoke(100));
    Console.WriteLine();

    // Инициация процесса удаления.
    Task t1 = Task.Run(() => Parallel.For(0, itemsCount, (i) =>
    {
        dictionary.Remove(i);
    }));

    Task t2 = Task.Run(() =>
    {
        // Изменение значений элементов по ключам : 15, 30, 100.
        Parallel.Invoke(() => updateAction.Invoke(15), () => updateAction.Invoke(30), () => updateAction.Invoke(100));
        // Просмотр измененных значений по ключам : 15, 30, 100.
        Parallel.Invoke(() => viewAction.Invoke(15), () => viewAction.Invoke(30), () => viewAction.Invoke(100));
    });

    await Task.WhenAll(t1, t2);
}

async Task TestConcurrentDictionary()
{
    // Мы знаем сколько элементов будет добавлено в ConcurrentDictionary. 
    // Установка стартового размера для ConcurrentDictionary будет гарантировать,
    // что при инициализации словаря не потребуется его увеличение. 
    int itemsCount = 200;

    // concurrencyLevel - ожидаемое количество потоков, которые будут работать с коллекцией, initialCapacity - начальный размер коллекции
    ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>(Environment.ProcessorCount, itemsCount);

    // Инициализация словаря
    Action addAction = () =>
    {
        for (int i = 0; i < itemsCount; i++)
        {
            // Первый способ добавить элемент:
            cd.TryAdd(i, i + 1);

            // Второй способ добавить элемент:
            //cd[i] = i + 1;

            // Третий способ добавить элемент:
            //cd.AddOrUpdate(i, i + 1, (k, v) => { return v; });
        }
    };

    Parallel.Invoke(addAction, addAction);

    // Просмотр значений элементов.
    Action<int> viewAction = (int key) =>
    {
        // Первый способ просмотреть значение элемента:
        //Console.WriteLine($"По ключу #{key} значение = {dictionary[key]}");

        // Второй способ просмотреть значение элемента:
        if (cd.TryGetValue(key, out int value))
        {
            Console.WriteLine($"По ключу #{key} значение = {value}");
        }

        // Третий способ просмотреть значение элемента:
        // Console.WriteLine($"По ключу #{key} значение = {cd.GetOrAdd(key, 0)}");
    };

    // Изменение значений элементов.
    Action<int> updateAction = (int key) =>
    {
        // Первый способ изменения значения элемента:
        //cd[key] = cd[key] * cd[key];

        // Второй способ изменения значения элемента:
        //if (cd.TryGetValue(key, out int value))
        //{
        //    cd.TryUpdate(key, value * value, value);
        //}

        // Третий способ изменения значение элемента:
        cd.AddOrUpdate(key, 0, (k, v) => { return v * v; });
    };

    // Просмотр значений по ключам : 15, 30, 100.
    Parallel.Invoke(() => viewAction.Invoke(15), () => viewAction.Invoke(30), () => viewAction.Invoke(100));
    Console.WriteLine();

    // Инициация процесса удаления.
    Task t1 = Task.Run(() => Parallel.For(0, itemsCount, (i) =>
    {
        cd.TryRemove(i, out _);
    }));

    Task t2 = Task.Run(() =>
    {
        // Изменение значений элементов по ключам : 15, 30, 100.
        Parallel.Invoke(() => updateAction.Invoke(15), () => updateAction.Invoke(30), () => updateAction.Invoke(100));
        // Просмотр измененных значений по ключам : 15, 30, 100.
        Parallel.Invoke(() => viewAction.Invoke(15), () => viewAction.Invoke(30), () => viewAction.Invoke(100));
    });

    await Task.WhenAll(t1,t2);
}
