internal class MessageListenter
{
    private static readonly LinkedList<Message> messagesList = new LinkedList<Message>();

    public static void AddMessage(Message message)
    {
        messagesList.AddLast(message);
    }

    public void Listen()
    {
        while (true)
        {
            if (messagesList.Count > 0)
            {
                Message message = messagesList.First.Value;

                if (message != null)
                {
                    messagesList.Remove(message);
                    DispatchMessage(message);
                }
            }

        }
    }

    private void DispatchMessage(Message message)
    {
        SendOrPostCallback callback = message.Callback;
        object state = message.State;

        try
        {
            callback.Invoke(state);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"{new string('-', 80)}");

            Console.WriteLine($"Ошибка - {ex.GetType()}");
            Console.WriteLine($"Сообщение:");
            Console.WriteLine($"{ex.Message}");

            Console.WriteLine($"{new string('-', 80)}");
        }
    }
}
