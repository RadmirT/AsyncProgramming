            string phrase = "The quick brown fox jumps over the lazy dog";
            string[] words = phrase.ToLower().Split(' ');

            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cts.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Func<List<char>> localInit = () => new List<char>();

            Func<int, ParallelLoopState, List<char>, List<char>> loopBody = (index, loopState, localList) =>
            {
                if (loopState.ShouldExitCurrentIteration == true)
                    return localList;

                for (int j = 0; j < words[index].Length; j++)
                    if (localList.Contains(words[index][j]) == false)
                        localList.Add(words[index][j]);

                return localList;
            };

            List<char> alphabet = new List<char>();
            Action<List<char>> localFinally = (localList) =>
            {
                lock (alphabet)
                {
                    foreach (var item in localList)
                    {
                        if (alphabet.Contains(item) == false)
                            alphabet.Add(item);
                    }
                }
            };

            try
            {
                Parallel.For(0, words.Length, parallelOptions, localInit, loopBody, localFinally);

                const int englishLetterCount = 26;
                Console.WriteLine($"Количество букв в английском алфавите - {englishLetterCount}.");
                Console.WriteLine($"Наша коллекция нашла {alphabet.Count} уникальных символов.");

                string result = alphabet.Count == englishLetterCount ? "Фраза является панграммой. " : "Фраза не является панграммой. ";

                Console.WriteLine($"{result}Найденные буквы:\n");
                foreach (var letter in alphabet.OrderBy(x => x))
                {
                    Console.Write($"{letter} ");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"{ex.GetType()}");
                Console.WriteLine($"{ex.Message}");
            }

            Console.ReadKey();
