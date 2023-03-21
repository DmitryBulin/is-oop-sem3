using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class ShopStock
{
    private readonly List<ProductStock> _products = new List<ProductStock>();

    public ProductStock? FindProduct(Product product)
    {
        return _products.Find(position => position.Product == product);
    }

    public void ChangePrice(Product product, decimal price)
    {
        ProductStock? position = FindProduct(product);

        if (position == null)
        {
            throw ProductException.AbsentException(product.Id);
        }

        position.ChangePrice(price);
    }

    public void RestockProducts(StockOrder products)
    {
        foreach (ProductStock newProduct in products.Stocks)
        {
            ProductStock? position = _products.Find(product => product.Product == newProduct.Product);
            if (position == null)
            {
                _products.Add(newProduct);
            }
            else
            {
                position.IncreaseQuantity(newProduct.Quantity);
            }
        }
    }

    public void BuyProducts(StockOrder products, out decimal totalIncome)
    {
        totalIncome = 0;
        foreach (ProductStock stock in products.Stocks)
        {
            ProductStock? positionInShop = _products.Find(product => product.Product == stock.Product);

            if (positionInShop == null)
            {
                throw ProductException.AbsentException(stock.Product.Id);
            }

            totalIncome += positionInShop.Price * stock.Quantity;

            positionInShop.DecreaseQuantity(stock.Quantity);

            if (positionInShop.Quantity == 0)
            {
                _products.Remove(positionInShop);
            }
        }
    }

    public bool HasEnoughStock(StockOrder products, out decimal totalCost)
    {
        totalCost = 0;

        foreach (ProductStock stock in products.Stocks)
        {
            ProductStock? positionInShop = _products.Find(product => product.Product == stock.Product && product.Quantity >= stock.Quantity);

            if (positionInShop is null)
            {
                return false;
            }

            totalCost += stock.Quantity * positionInShop.Price;
        }

        return true;
    }
}
