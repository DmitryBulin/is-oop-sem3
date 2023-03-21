using Banks.Exceptions;

namespace Banks.Transaction;

public class OneTimeReversableTransaction : ITransaction
{
    private Action _performFunc;
    private Action _revertFunc;
    private bool _performed = false;

    public OneTimeReversableTransaction(Action performFunc, Action revertFunc)
    {
        _performFunc = performFunc;
        _revertFunc = revertFunc;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public void Perform()
    {
        _performFunc.Invoke();
        _performFunc = () => throw TransactionException.PerformRepeat(Id);
        _performed = true;
    }

    public void Revert()
    {
        if (!_performed)
        {
            throw TransactionException.RevertBeforePerform(Id);
        }

        _revertFunc.Invoke();
        _revertFunc = () => throw TransactionException.RevertRepeat(Id);
    }

    public override string ToString() => $"{Id}";
}
