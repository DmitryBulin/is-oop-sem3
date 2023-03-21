using Shops.Exceptions;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    public Product(string name)
    {
        Id = Guid.NewGuid();
        ChangeName(name);
    }

    public Guid Id { get; }
    public string Name { get; private set; } = string.Empty;

    public static bool ValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        return true;
    }

    public void ChangeName(string name)
    {
        if (!ValidName(name))
        {
            throw ProductException.InvalidNameException(Id, name);
        }

        Name = name;
    }

    public bool Equals(Product? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }
}
