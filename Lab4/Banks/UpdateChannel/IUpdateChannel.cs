namespace Banks.UpdateChannel;

public interface IUpdateChannel<T>
{
    void Notify(T message);
    void Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}