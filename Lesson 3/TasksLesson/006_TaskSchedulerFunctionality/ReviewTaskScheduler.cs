using System.Collections.ObjectModel;

internal class ReviewTaskScheduler : TaskScheduler
{
    private readonly LinkedList<Task> tasksList = new LinkedList<Task>();

    protected override IEnumerable<Task> GetScheduledTasks()
    {
            return tasksList.ToList();
    }

    /// <summary>
    /// Метод вызывается методом Start класса Task
    /// </summary>
    /// <param name="task"></param>
    protected override void QueueTask(Task task)
    {
        Console.WriteLine($"\t[QueueTask] Задача #{task.Id} поставлена в очередь..");
        tasksList.AddLast(task);
        ThreadPool.QueueUserWorkItem(this.ExecuteTasks, null);
        //this.ExecuteTasks(null);
    }

    /// <summary>
    /// Метод вызывается методами ожидания, к примеру Wait, WaitAll...
    /// </summary>
    /// <param name="task"></param>
    /// <param name="taskWasPreviouslyQueued"></param>
    /// <returns></returns>
    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        Console.WriteLine($"\t\t[TryExecuteTaskInline] Попытка выполнить задачу #{task.Id} синхронно..");

        lock (tasksList)
        {
            tasksList.Remove(task);
        }

        return base.TryExecuteTask(task);
    }

    /// <summary>
    /// Метод вызывается при отмене выполнения задачи
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    protected override bool TryDequeue(Task task)
    {
        Console.WriteLine($"\t\t[TryDequeue] Попытка удалить задачу {task.Id} из очереди..");
        bool result = false;

        lock (tasksList)
        {
            result = tasksList.Remove(task);
        }

        if (result == true)
        {
            Console.WriteLine($"\t\t[TryDequeue] Задача {task.Id} была удалена из очереди на выполнение..");
        }

        return result;
    }

    private void ExecuteTasks(object _)
    {
        while (true)
        {
            //Thread.Sleep(2000); // Убрать комментарий для проверки TryExecuteTaskInline
            //Thread.Sleep(500); // Убрать комментарий для проверки TryDequeueTesting
            Task? task = null;

            lock (tasksList)
            {
                if (tasksList.Count == 0)
                {
                    break;
                }

                task = this.tasksList.First.Value;
                tasksList.RemoveFirst();
             }

            if (task == null)
            {
                break;
            }

            base.TryExecuteTask(task);
        }
    }
}
