using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace _001_AsyncAwait_Decompiled
{
    internal class Program
    {
        public Program()
            : base()
        {
        }

        [SpecialName]
        private static void Main(string[] args)
        {
            Program.Main().GetAwaiter().GetResult();
        }

        [AsyncStateMachine(typeof(Program.MainStruct))]
        private static Task Main()
        {
            // Создание экземпляра конечного автомата
            Program.MainStruct stateMachine = default;
            // Заполнение полей конечного автомата
            stateMachine.builder = AsyncTaskMethodBuilder.Create();
            stateMachine.state = -1;
            // Запуск конечного автомата (внутри вызов метода MoveNext())
            stateMachine.builder.Start<Program.MainStruct>(ref stateMachine);
            return stateMachine.builder.Task;
        }

        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct MainStruct : IAsyncStateMachine
        {
            // Открытые поля конечного автомата для их заполнения при создании экземпляра.
            public int state;
            public AsyncTaskMethodBuilder builder;
            // Закрытые поля конечного автомата для сохранения значений локальных переменных метода при приостановке.
            private TaskAwaiter awaiter;

            /// <summary>
            /// Метод выполняет тело асинхронного метода. Изменяет состояние конечного автомата при шаге.
            /// </summary>
            void IAsyncStateMachine.MoveNext()
            {
                // Получение состояния конечного автомата из поля state.
                int num1 = this.state;
                try
                {
                    // Создание объекта ожидания завершения асинхронной задачи
                    TaskAwaiter awaiter;
                    int num2;
                    if (num1 != 0)
                    {
                        Console.WriteLine(string.Format("Метод Main начал свою работу в потоке {0}.", (object)Thread.CurrentThread.ManagedThreadId));
                        // Работа оператора await:
                        awaiter = Program.WriteCharAsync('#').GetAwaiter();
                        // Проверка: завершилась ли работа задачи.
                        if (!awaiter.IsCompleted)
                        {
                            // Перевод состояния конечного автомата в ожидающее.
                            // Любое значение отличное от "-1" и "-2" означает, что конечный автомат ожидает завершения 
                            // асинхронной операции.
                            this.state = num2 = 0;
                            this.awaiter = awaiter;
                            // Метод создаст и установит продолжение для асинхронной задачи. 
                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter, Program.MainStruct>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.awaiter;
                        this.awaiter = new TaskAwaiter();
                        // Значение "-1" для состояния также означает, что конечный автомат выполняется.
                        this.state = num2 = -1;
                    }
                    // Завершения ожидания асинхронной задачи.
                    awaiter.GetResult();
                    Program.WriteChar('*');
                    Console.WriteLine(string.Format("Метод Main закончил свою работу в потоке {0}.", (object)Thread.CurrentThread.ManagedThreadId));
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                    this.state = -2;
                    // Установка полученного исключения в задачу-марионетку
                    this.builder.SetException(ex);
                    return;
                }
                // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                this.state = -2;
                // Установка успешного выполнения задачи-марионетки
                this.builder.SetResult();
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.builder.SetStateMachine(stateMachine);
            }
        }

        [AsyncStateMachine(typeof(Program.WriteCharAsyncStruct))]
        private static Task WriteCharAsync(char symbol)
        {
            // Создание экземпляра конечного автомата
            Program.WriteCharAsyncStruct stateMachine = default;
            // Заполнение полей конечного автомата
            stateMachine.symbol = symbol;
            stateMachine.builder = AsyncTaskMethodBuilder.Create();
            stateMachine.state = -1;
            // Запуск конечного автомата (внутри вызов метода MoveNext())
            stateMachine.builder.Start<Program.WriteCharAsyncStruct>(ref stateMachine);
            return stateMachine.builder.Task;
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine(string.Format("Id потока - [{0}]. Id задачи - [{1}]", (object)Thread.CurrentThread.ManagedThreadId, (object)Task.CurrentId));
            Thread.Sleep(500);
            for (int index = 0; index < 80; ++index)
            {
                Console.Write(symbol);
                Thread.Sleep(50);
            }
        }


        [CompilerGenerated]
        [StructLayout(LayoutKind.Auto)]
        private struct WriteCharAsyncStruct : IAsyncStateMachine
        {
            // Открытые поля конечного автомата для их заполнения при создании экземпляра.
            public int state;
            public AsyncTaskMethodBuilder builder;
            public char symbol;
            // Закрытые поля конечного автомата для сохранения значений локальных переменных метода при приостановке.
            private TaskAwaiter awaiter;

            /// <summary>
            /// Метод выполняет тело асинхронного метода. Изменяет состояние конечного автомата при шаге.
            /// </summary>
            void IAsyncStateMachine.MoveNext()
            {
                // Получение состояния конечного автомата из поля state.
                int num1 = this.state;
                try
                {
                    // Создание объекта ожидания завершения асинхронной задачи
                    TaskAwaiter awaiter;
                    int num2;
                    if (num1 != 0)
                    {
                        // Создание экземпляра класса, который представляет лямбда-выражение: () => WriteChar(symbol).
                        Program.DisplayClass displayClass = new Program.DisplayClass();
                        displayClass.symbol = this.symbol;
                        Console.WriteLine(string.Format("Метод WriteCharAsync начал свою работу в потоке {0}.", (object)Thread.CurrentThread.ManagedThreadId));

                        // Работа оператора await:
                        awaiter = Task.Run(new Action(displayClass.WriteCharAsync)).GetAwaiter();
                        // Проверка: завершилась ли работа задачи.
                        if (!awaiter.IsCompleted)
                        {
                            // Перевод состояния конечного автомата в ожидающее.
                            // Любое значение отличное от "-1" и "-2" означает, что конечный автомат ожидает завершения 
                            // асинхронной операции.
                            this.state = num2 = 0;
                            this.awaiter = awaiter;
                            // Метод создаст и установит продолжение для асинхронной задачи. 
                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter, Program.WriteCharAsyncStruct>(ref awaiter, ref this);
                            // Возврат управления "вызывающему потоку".
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.awaiter;
                        this.awaiter = new TaskAwaiter();
                        // Значение "-1" для состояния также означает, что конечный автомат выполняется.
                        this.state = num2 = -1;
                    }
                    // Завершения ожидания асинхронной задачи.
                    awaiter.GetResult();
                    Console.WriteLine(string.Format("Метод WriteCharAsync закончил свою работу в потоке {0}.", (object)Thread.CurrentThread.ManagedThreadId));
                }
                catch (Exception ex)
                {
                    // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                    this.state = -2;
                    // Установка полученного исключения в задачу-марионетку
                    this.builder.SetException(ex);
                    return;
                }
                // Установка значения "-2" для состояния конечного автомата означает, что он завершил свою работу.
                this.state = -2;
                // Установка успешного выполнения задачи-марионетки
                this.builder.SetResult();
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.builder.SetStateMachine(stateMachine);
            }
        }

        /// <summary>
        /// Класс, который представляет лямбда-выражение () => WriteChar(symbol)
        /// </summary>
        [CompilerGenerated]
        private sealed class DisplayClass
        {
            /// <summary>
            /// Захваченный параметр в лямбда-выражение.
            /// </summary>
            public char symbol;

            public DisplayClass()
                    : base()
            {
            }

            /// <summary>
            /// Тело лямбда-выражения.
            /// </summary>
            internal void WriteCharAsync()
            {
                Program.WriteChar(this.symbol);
            }
        }
    }
}