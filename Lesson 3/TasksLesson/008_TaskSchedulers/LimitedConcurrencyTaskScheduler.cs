internal class LimitedConcurrencyTaskScheduler : TaskScheduler
{
    [ThreadStatic]
    private static bool currentThreadIsProcessingItems;

    private readonly LinkedList<Task> tasksList = new LinkedList<Task>();
    private readonly int concurrencyLevel;

    private int runningTasks = 0;

    public LimitedConcurrencyTaskScheduler(int concurrencyLevel)
    {
        this.concurrencyLevel = concurrencyLevel < 1 ? 1 : concurrencyLevel;
    }

    public sealed override int MaximumConcurrencyLevel => this.concurrencyLevel;

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        lock (tasksList)
        {
            return tasksList;
        }
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        if (currentThreadIsProcessingItems == false)
        {
            return false;
        }

        if (taskWasPreviouslyQueued == true)
        {
            this.TryDequeue(task);
        }

        return TryExecuteTask(task);
    }

    protected sealed override bool TryDequeue(Task task)
    {
        lock (tasksList)
        {
            return tasksList.Remove(task);
        }
    }

    protected override void QueueTask(Task task)
    {
        lock (tasksList)
        {
            tasksList.AddLast(task);

            if (runningTasks < concurrencyLevel)
            {
                ++runningTasks;
                NotifyThreadPoolOfPendingWork();
            }
        }
    }

    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            currentThreadIsProcessingItems = true;
            try
            {
                while (true)
                {
                    Task task;
                    lock (tasksList)
                    {
                        if (tasksList.Count == 0)
                        {
                            --runningTasks;
                            break;
                        }
                        task = tasksList.First.Value;
                        tasksList.RemoveFirst();
                    }

                    TryExecuteTask(task);
                }
            }
            finally { currentThreadIsProcessingItems = false; }
        }, null);
    }
}

