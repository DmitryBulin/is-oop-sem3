using Banks.Account;

namespace Banks.UpdateChannel;

internal class TermUpdateChannel : ITermUpdateChannel
{
    private readonly List<Action<IReadOnlyList<AccountTermsUpdatePair>>> _termsUpdateSubscribers = new List<Action<IReadOnlyList<AccountTermsUpdatePair>>>();
    private readonly List<Action<IReadOnlyList<IAccountTerms>>> _newTermsSubscribers = new List<Action<IReadOnlyList<IAccountTerms>>>();

    public void Notify(IReadOnlyList<AccountTermsUpdatePair> message)
    {
        _termsUpdateSubscribers.ForEach(subscriber => subscriber.Invoke(message));
    }

    public void Notify(IReadOnlyList<IAccountTerms> message)
    {
        _newTermsSubscribers.ForEach(subscriber => subscriber.Invoke(message));
    }

    public void Subscribe(Action<IReadOnlyList<AccountTermsUpdatePair>> action)
    {
        _termsUpdateSubscribers.Add(action);
    }

    public void Subscribe(Action<IReadOnlyList<IAccountTerms>> action)
    {
        _newTermsSubscribers.Add(action);
    }

    public void Unsubscribe(Action<IReadOnlyList<AccountTermsUpdatePair>> action)
    {
        _termsUpdateSubscribers.Remove(action);
    }

    public void Unsubscribe(Action<IReadOnlyList<IAccountTerms>> action)
    {
        _newTermsSubscribers.Remove(action);
    }
}