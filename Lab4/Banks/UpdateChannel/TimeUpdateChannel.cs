namespace Banks.UpdateChannel;

public class TimeUpdateChannel : ITimeUpdateChannel
{
    private readonly List<Action<DateTime>> _subscribers = new List<Action<DateTime>>();

    public DateTime CurrentTime { get; private set; } = DateTime.MinValue;

    public void Notify(DateTime message)
    {
        CurrentTime = message;
        _subscribers.ForEach(subscriber => subscriber.Invoke(message));
    }

    public void Subscribe(Action<DateTime> action)
    {
        _subscribers.Add(action);
    }

    public void Unsubscribe(Action<DateTime> action)
    {
        _subscribers.Remove(action);
    }
}
