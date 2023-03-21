using Banks.Account;

namespace Banks.UpdateChannel;

public interface ITermUpdateChannel :
    IUpdateChannel<IReadOnlyList<AccountTermsUpdatePair>>,
    IUpdateChannel<IReadOnlyList<IAccountTerms>>
{
}