namespace Banks.UpdateChannel;

public interface ITimeUpdateChannel : IUpdateChannel<DateTime>
{
    DateTime CurrentTime { get; }
}
