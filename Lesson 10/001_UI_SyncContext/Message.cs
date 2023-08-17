internal class Message
{
    public SendOrPostCallback Callback { get; }
    public object State { get; }

    public Message(SendOrPostCallback callback, object state)
    {
        Callback = callback;
        State = state;
    }
}

