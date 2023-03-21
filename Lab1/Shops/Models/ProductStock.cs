using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public record ProductStock
{
    public ProductStock(Product product, int quantity, decimal price = 0)
    {
        Product = product;
        ChangeQuantity(quantity);
        ChangePrice(price);
    }

    public ProductStock(ProductStock other)
    {
        Product = other.Product;
        ChangeQuantity(other.Quantity);
        ChangePrice(other.Price);
    }

    public Product Product { get; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalPrice => Price * Quantity;

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw ProductException.InvalidPriceException(Product.Id, newPrice);
        }

        Price = newPrice;
    }

    public void ChangeQuantity(int newQuantity)
    {
        if (newQuantity < 0)
        {
            throw ProductException.InvalidQuantityException(Product.Id, newQuantity);
        }

        Quantity = newQuantity;
    }

    public void IncreaseQuantity(int income)
    {
        if (income < 0)
        {
            throw ProductException.InvalidQuantityException(Product.Id, income);
        }

        ChangeQuantity(Quantity + income);
    }

    public void DecreaseQuantity(int deficit)
    {
        if (deficit < 0)
        {
            throw ProductException.InvalidQuantityException(Product.Id, deficit);
        }

        ChangeQuantity(Quantity - deficit);
    }
}
