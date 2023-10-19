using Contracts;
using System.Diagnostics;

namespace _002_AsyncAndParallel.Models
{
    public class ServiceManager
    {
        private Stopwatch timer;
        private Queue<string> messages;

        public ServiceManager()
        {
            timer = Stopwatch.StartNew();
            messages = new Queue<string>();
        }

        public News News { get; set; }
        public MoviePoster MoviePoster { get; set; }

        public long ElapsedTime
        {
            get
            {
                return timer.ElapsedMilliseconds;
            }
        }

        public IEnumerable<string> Messages { get { return messages; } }

        public void AddMessage(string message)
        {
            messages.Enqueue($"{message} в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}