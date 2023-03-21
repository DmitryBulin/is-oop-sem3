namespace Shops.Entities;

public interface IBalance
{
    decimal CurrentBalance { get; }
    void Spend(decimal payout);
    void Earn(decimal income);
}
