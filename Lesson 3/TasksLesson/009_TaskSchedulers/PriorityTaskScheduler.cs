internal class PriorityTaskScheduler : TaskScheduler
{
    private readonly LinkedList<Task> tasksList = new LinkedList<Task>();

    public bool Prioritize(Task task)
    {
        lock (tasksList)
        {
            var node = tasksList.Find(task);
            if (node != null)
            {
                tasksList.Remove(node);
                tasksList.AddFirst(node);
                return true;
            }
        }

        return false;
    }

    public bool Deprioritize(Task task)
    {
        lock (tasksList)
        {
            var node = tasksList.Find(task);
            if (node != null)
            {
                tasksList.Remove(node);
                tasksList.AddLast(node);
                return true;
            }
        }

        return false;
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        lock (tasksList)
        {
            return tasksList;
        }
    }

    protected override bool TryDequeue(Task task)
    {
        lock (tasksList)
        {
            return tasksList.Remove(task);
        }
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return base.TryExecuteTask(task);
    }

    protected override void QueueTask(Task task)
    {
        lock (tasksList)
        {
            tasksList.AddLast(task);
        }

        ThreadPool.QueueUserWorkItem(ProcessNextQueuedItem, null);
    }

    private void ProcessNextQueuedItem(object _)
    {
        Task task;

        lock (tasksList)
        {
            if (tasksList.Count > 0)
            {
                task = tasksList.First.Value;
                tasksList.RemoveFirst();
            }
            else
            {
                return;
            }
        }

        base.TryExecuteTask(task);
    }
}
