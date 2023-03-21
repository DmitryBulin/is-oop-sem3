namespace Shops.Exceptions;

public class ShopException : ShopsException
{
    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException InvalidNameException(string name)
    {
        return new ShopException($"Failed to set shop name to {name}");
    }

    public static ShopException ShopAbcentException(Guid shopId)
    {
        return new ShopException($"Failed to get shop {shopId}");
    }

    public static ShopException NotEnoughProductException(Guid shopId, Guid productId)
    {
        return new ShopException($"Not enough quantity for product {productId} in shop {shopId}");
    }

    public static ShopException NotEnoughProductsException(Guid shopId)
    {
        return new ShopException($"Not enough stock in shop {shopId}");
    }
}
