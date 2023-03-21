namespace Shops.Entities;

public class Person
{
    public Person()
    {
        Id = Guid.NewGuid();
        Balance = new BankAccount();
    }

    public Guid Id { get; }
    public IBalance Balance { get; }
}
