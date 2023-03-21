namespace Shops.Exceptions;

public class ProductException : ShopsException
{
    private ProductException(string message)
        : base(message)
    {
    }

    public static ProductException AbsentException(Guid productId)
    {
        return new ProductException($"Failed to get product {productId}");
    }

    public static ProductException InvalidPriceException(Guid productId, decimal price)
    {
        return new ProductException($"Failed to set price {price} to product {productId}");
    }

    public static ProductException InvalidQuantityException(Guid productId, int quantity)
    {
        return new ProductException($"Failed to set quantity {quantity} for product {productId}");
    }

    public static ProductException InvalidNameException(Guid productId, string name)
    {
        return new ProductException($"Tried to set name of product {productId} to {name}");
    }
}
